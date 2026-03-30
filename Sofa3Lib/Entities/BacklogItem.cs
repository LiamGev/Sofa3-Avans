using Domain.Interfaces;

namespace Domain.Entities
{
    public class BacklogItem
    {
        private readonly List<TaskActivity> _activities = new();
        private readonly List<Message> _messages = new();
        private readonly List<INotificationObserver> _observers = new();

        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Type { get; private set; }

        public IReadOnlyCollection<TaskActivity> Activities => _activities.AsReadOnly();
        public IReadOnlyCollection<Message> Messages => _messages.AsReadOnly();
        public IReadOnlyCollection<INotificationObserver> Observers => _observers.AsReadOnly();

        public IBacklogItemState CurrentState { get; private set; }

        public BacklogItem(string title, string description, string type, IBacklogItemState initialState)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be empty.");

            Id = Guid.NewGuid();
            Title = title;
            Description = description;
            Type = type;
            CurrentState = initialState ?? throw new ArgumentNullException(nameof(initialState));
        }

        public void AddActivity(string title)
        {
            _activities.Add(new TaskActivity(title));
        }

        public void AddMessage(string author, string content)
        {
            _messages.Add(new Message(author, content));
        }

        public void AttachObserver(INotificationObserver observer)
        {
            if (observer == null)
                throw new ArgumentNullException.ThrowIfNull(observer);

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

        public void StartWork()
        {
            CurrentState.Start(this);
        }

        public void MoveToTesting()
        {
            CurrentState.MoveToTesting(this);
        }

        public void Complete()
        {
            CurrentState.Complete(this);
        }
    }
}