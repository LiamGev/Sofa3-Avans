using Domain.Interfaces;

namespace Domain.Adapters
{
    public class EmailChannel : INotificationChannel
    {
        public List<string> SentMessages { get; } = new();

        public void Send(string message)
        {
            SentMessages.Add(message);
        }
    }
}