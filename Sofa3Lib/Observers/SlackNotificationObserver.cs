using Domain.Interfaces;

namespace Domain.Observers
{
    public class SlackNotificationObserver : INotificationObserver
    {
        public List<string> SentMessages { get; } = new();

        public void Update(string message)
        {
            SentMessages.Add($"SLACK: {message}");
        }
    }
}