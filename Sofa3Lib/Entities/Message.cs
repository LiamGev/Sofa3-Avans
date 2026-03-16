namespace sofa3Domain.Entities
{
    public class Message
    {
        public Guid Id { get; private set; }
        public string Author { get; private set; }
        public string Content { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public Message(string author, string content)
        {
            if (string.IsNullOrWhiteSpace(author))
                throw new ArgumentException("Author cannot be empty.");

            if (string.IsNullOrWhiteSpace(content))
                throw new ArgumentException("Content cannot be empty.");

            Id = Guid.NewGuid();
            Author = author;
            Content = content;
            CreatedAt = DateTime.UtcNow;
        }
    }
}