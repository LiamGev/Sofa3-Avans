using Domain.Interfaces;
using Domain.SprintStates;

namespace Domain.Entities
{
    public class Sprint
    {
        private readonly List<BacklogItem> _backlogItems = new();
        private readonly List<string> _developers = new();

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public string SprintType { get; private set; }
        public ISprintState CurrentState { get; private set; }
        public Pipeline? Pipeline { get; private set; }
        public string? ReviewSummary { get; private set; }

        // Rollen uit de casus
        public string? ScrumMaster { get; private set; }
        public string? ProductOwner { get; private set; }

        public IReadOnlyCollection<string> Developers => _developers.AsReadOnly();
        public IReadOnlyCollection<BacklogItem> BacklogItems => _backlogItems.AsReadOnly();

        public Sprint(string name, DateTime startDate, DateTime endDate, string sprintType)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Sprint name cannot be empty.");

            if (endDate <= startDate)
                throw new ArgumentException("End date must be after start date.");

            if (string.IsNullOrWhiteSpace(sprintType))
                throw new ArgumentException("Sprint type cannot be empty.");

            Id = Guid.NewGuid();
            Name = name;
            StartDate = startDate;
            EndDate = endDate;
            SprintType = sprintType;
            CurrentState = new CreatedSprintState();
        }

        public void SetScrumMaster(string scrumMaster)
        {
            if (string.IsNullOrWhiteSpace(scrumMaster))
                throw new ArgumentException("Scrum master cannot be empty.");

            ScrumMaster = scrumMaster;
        }

        public void SetProductOwner(string productOwner)
        {
            if (string.IsNullOrWhiteSpace(productOwner))
                throw new ArgumentException("Product owner cannot be empty.");

            ProductOwner = productOwner;
        }

        public void AddDeveloper(string developer)
        {
            if (string.IsNullOrWhiteSpace(developer))
                throw new ArgumentException("Developer name cannot be empty.");

            if (_developers.Any(d => d.Equals(developer, StringComparison.OrdinalIgnoreCase)))
                return;

            _developers.Add(developer);
        }

        public void AddBacklogItem(BacklogItem item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            if (CurrentState.Name != "Created")
                throw new InvalidOperationException("Backlog items can only be added while sprint is in Created state.");

            // Als een item al een developer heeft, moet die onderdeel zijn van de sprint.
            if (!string.IsNullOrWhiteSpace(item.AssignedDeveloper) &&
                !_developers.Any(d => d.Equals(item.AssignedDeveloper, StringComparison.OrdinalIgnoreCase)))
            {
                throw new InvalidOperationException("Assigned developer must be part of the sprint team.");
            }

            _backlogItems.Add(item);
        }

        public void SetPipeline(Pipeline pipeline)
        {
            Pipeline = pipeline ?? throw new ArgumentNullException(nameof(pipeline));
        }

        public void UploadReviewSummary(string summary)
        {
            if (string.IsNullOrWhiteSpace(summary))
                throw new ArgumentException("Review summary cannot be empty.");

            ReviewSummary = summary;
        }

        public bool RequiresReviewSummary()
        {
            return SprintType.Equals("Review", StringComparison.OrdinalIgnoreCase);
        }

        public bool CanBeClosed()
        {
            if (RequiresReviewSummary() && string.IsNullOrWhiteSpace(ReviewSummary))
                return false;

            return true;
        }

        public void SetState(ISprintState state)
        {
            CurrentState = state ?? throw new ArgumentNullException(nameof(state));
        }

        public void StartSprint() => CurrentState.StartSprint(this);
        public void FinishSprint() => CurrentState.FinishSprint(this);
        public void StartRelease() => CurrentState.StartRelease(this);
        public void CancelRelease() => CurrentState.CancelRelease(this);
        public void CloseSprint() => CurrentState.CloseSprint(this);
    }
}