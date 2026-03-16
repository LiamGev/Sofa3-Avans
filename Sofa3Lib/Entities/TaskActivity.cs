namespace sofa3Domain.Entities
{
    public class TaskActivity
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public bool IsCompleted { get; private set; }

        public TaskActivity(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Activity title cannot be empty.");

            Id = Guid.NewGuid();
            Title = title;
            IsCompleted = false;
        }

        public void MarkAsCompleted()
        {
            IsCompleted = true;
        }
    }
}