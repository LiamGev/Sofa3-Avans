using Domain.Interfaces;

namespace Domain.Adapters
{
    public class SlackChannel : INotificationChannel
    {
        public List<string> SentMessages { get; } = new();

        public void Send(string message)
        {
            SentMessages.Add(message);
        }
    }
}