namespace Domain.Entities
{
    public class Message
    {
        public Guid Id { get; private set; }
        public string Author { get; private set; }
        public string Content { get; private set; }
        public DateTime CreatedAt { get; private set; }

        // Hiermee kun je discussiethreads en replies modelleren.
        public Guid? ParentMessageId { get; private set; }

        public bool IsThreadStarter => ParentMessageId == null;

        public Message(string author, string content, Guid? parentMessageId = null)
        {
            if (string.IsNullOrWhiteSpace(author))
                throw new ArgumentException("Author cannot be empty.");

            if (string.IsNullOrWhiteSpace(content))
                throw new ArgumentException("Content cannot be empty.");

            Id = Guid.NewGuid();
            Author = author;
            Content = content;
            ParentMessageId = parentMessageId;
            CreatedAt = DateTime.UtcNow;
        }
    }
}