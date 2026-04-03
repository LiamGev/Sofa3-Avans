using Domain.Interfaces;

namespace Domain.Entities
{
    public class BacklogItem
    {
        private readonly List<TaskActivity> _activities = new();
        private readonly List<Message> _messages = new();
        private readonly List<INotificationObserver> _observers = new();
        private readonly List<string> _linkedCommits = new();
        private readonly List<string> _linkedBranches = new();

        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Type { get; private set; }

        // Op basis van de opdracht: maximaal één developer op backlog-item niveau.
        public string? AssignedDeveloper { get; private set; }

        // Deze property maakt expliciet dat “done” in workflow niet hetzelfde hoeft te zijn
        // als functioneel afgesloten voor discussies.
        public bool IsCompleted { get; private set; }

        // Zodra het item echt is afgerond mag de discussie niet meer wijzigen.
        public bool IsDiscussionLocked => IsCompleted;

        public IReadOnlyCollection<TaskActivity> Activities => _activities.AsReadOnly();
        public IReadOnlyCollection<Message> Messages => _messages.AsReadOnly();
        public IReadOnlyCollection<INotificationObserver> Observers => _observers.AsReadOnly();
        public IReadOnlyCollection<string> LinkedCommits => _linkedCommits.AsReadOnly();
        public IReadOnlyCollection<string> LinkedBranches => _linkedBranches.AsReadOnly();

        public IBacklogItemState CurrentState { get; private set; }

        public BacklogItem(string title, string description, string type, IBacklogItemState initialState)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be empty.");

            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Description cannot be empty.");

            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentException("Type cannot be empty.");

            Id = Guid.NewGuid();
            Title = title;
            Description = description;
            Type = type;
            CurrentState = initialState ?? throw new ArgumentNullException(nameof(initialState));
            IsCompleted = false;
        }

        public void AssignDeveloper(string developerName)
        {
            if (string.IsNullOrWhiteSpace(developerName))
                throw new ArgumentException("Developer name cannot be empty.");

            if (!string.IsNullOrWhiteSpace(AssignedDeveloper) &&
                !string.Equals(AssignedDeveloper, developerName, StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("A backlog item can have at most one assigned developer.");
            }

            AssignedDeveloper = developerName;
        }

        public void AddActivity(string title)
        {
            if (IsCompleted)
                throw new InvalidOperationException("Cannot add activities to a completed backlog item.");

            _activities.Add(new TaskActivity(title));
        }

        public void CompleteActivity(Guid activityId)
        {
            var activity = _activities.FirstOrDefault(a => a.Id == activityId)
                ?? throw new InvalidOperationException("Activity not found.");

            activity.MarkAsCompleted();
        }

        public bool AreAllActivitiesCompleted()
        {
            return _activities.All(a => a.IsCompleted);
        }

        // Start nieuwe discussie-thread
        public Guid AddDiscussionThread(string author, string content)
        {
            if (IsDiscussionLocked)
                throw new InvalidOperationException("Cannot start a new discussion thread for a completed backlog item.");

            var message = new Message(author, content);
            _messages.Add(message);

            NotifyObservers($"New discussion thread added to backlog item '{Title}'.");
            return message.Id;
        }

        // Reply op bestaande thread of bericht
        public Guid AddReply(Guid parentMessageId, string author, string content)
        {
            if (IsDiscussionLocked)
                throw new InvalidOperationException("Cannot reply to a discussion on a completed backlog item.");

            var parent = _messages.FirstOrDefault(m => m.Id == parentMessageId)
                ?? throw new InvalidOperationException("Parent message not found.");

            var reply = new Message(author, content, parent.Id);
            _messages.Add(reply);

            NotifyObservers($"New reply added to discussion for backlog item '{Title}'.");
            return reply.Id;
        }

        // Bestaande methode behouden zodat je service layer niet breekt.
        public void AddMessage(string author, string content)
        {
            AddDiscussionThread(author, content);
        }

        public void LinkCommit(string commitHash)
        {
            if (string.IsNullOrWhiteSpace(commitHash))
                throw new ArgumentException("Commit hash cannot be empty.");

            if (!_linkedCommits.Contains(commitHash))
                _linkedCommits.Add(commitHash);
        }

        public void LinkBranch(string branchName)
        {
            if (string.IsNullOrWhiteSpace(branchName))
                throw new ArgumentException("Branch name cannot be empty.");

            if (!_linkedBranches.Contains(branchName))
                _linkedBranches.Add(branchName);
        }

        public void AttachObserver(INotificationObserver observer)
        {
            if (observer == null)
                throw new ArgumentNullException(nameof(observer));

            _observers.Add(observer);
        }

        public void NotifyObservers(string message)
        {
            foreach (var observer in _observers)
            {
                observer.Update(message);
            }
        }

        public void SetState(IBacklogItemState newState)
        {
            CurrentState = newState ?? throw new ArgumentNullException(nameof(newState));
            NotifyObservers($"Backlog item '{Title}' changed state to {CurrentState.Name}.");
        }

        // Alleen wanneer het item definitief afgerond is mag de discussie op slot.
        public void MarkAsCompleted()
        {
            if (CurrentState.Name != "Done")
                throw new InvalidOperationException("A backlog item can only be completed when it is in Done state.");

            IsCompleted = true;
            NotifyObservers($"Backlog item '{Title}' has been completed and discussions are now locked.");
        }

        public void Reopen()
        {
            IsCompleted = false;
            SetState(new Domain.States.ToDoState());
            NotifyObservers($"Backlog item '{Title}' was reopened.");
        }

        public void StartWork() => CurrentState.Start(this);
        public void MoveToReadyForTesting() => CurrentState.MoveToReadyForTesting(this);
        public void StartTesting() => CurrentState.StartTesting(this);
        public void ApproveTesting() => CurrentState.ApproveTesting(this);
        public void RejectTesting() => CurrentState.RejectTesting(this);
        public void ApproveDone() => CurrentState.ApproveDone(this);
        public void RejectDone() => CurrentState.RejectDone(this);
    }
}