using Domain.Interfaces;
using Domain.SprintStates;

namespace Domain.Entities
{
    public class Sprint
    {
        private readonly List<BacklogItem> _backlogItems = new();

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public string SprintType { get; private set; }
        public ISprintState CurrentState { get; private set; }
        public Pipeline? Pipeline { get; private set; }
        public string? ReviewSummary { get; private set; }

        public IReadOnlyCollection<BacklogItem> BacklogItems => _backlogItems.AsReadOnly();

        public Sprint(string name, DateTime startDate, DateTime endDate, string sprintType)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Sprint name cannot be empty.");

            if (endDate <= startDate)
                throw new ArgumentException("End date must be after start date.");

            Id = Guid.NewGuid();
            Name = name;
            StartDate = startDate;
            EndDate = endDate;
            SprintType = sprintType;
            CurrentState = new CreatedSprintState();
        }

        public void AddBacklogItem(BacklogItem item)
        {
            if (CurrentState.Name != "Created")
                throw new InvalidOperationException("Backlog items can only be added while sprint is in Created state.");

            _backlogItems.Add(item);
        }

        public void SetPipeline(Pipeline pipeline)
        {
            Pipeline = pipeline;
        }

        public void UploadReviewSummary(string summary)
        {
            if (string.IsNullOrWhiteSpace(summary))
                throw new ArgumentException("Review summary cannot be empty.");

            ReviewSummary = summary;
        }

        public void SetState(ISprintState state)
        {
            CurrentState = state;
        }

        public void StartSprint() => CurrentState.StartSprint(this);
        public void FinishSprint() => CurrentState.FinishSprint(this);
        public void StartRelease() => CurrentState.StartRelease(this);
        public void CancelRelease() => CurrentState.CancelRelease(this);
        public void CloseSprint() => CurrentState.CloseSprint(this);
    }
}