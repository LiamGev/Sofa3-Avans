namespace Domain.Entities
{
    public class Project
    {
        private readonly List<BacklogItem> _backlogItems = new();

        public Guid Id { get; private set; }
        public string Name { get; private set; }

        public IReadOnlyCollection<BacklogItem> BacklogItems => _backlogItems.AsReadOnly();

        public Project(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Project name cannot be empty.");

            Id = Guid.NewGuid();
            Name = name;
        }

        public void AddBacklogItem(BacklogItem item)
        {
            if (item == null)
                throw new ArgumentNullException.ThrowIfNull(nameof(item));

            _backlogItems.Add(item);
        }

        public BacklogItem? GetBacklogItemById(Guid id)
        {
            return _backlogItems.FirstOrDefault(x => x.Id == id);
        }
    }
}