using Domain.Interfaces;

namespace Domain.Observers
{
    public class SlackNotificationObserver : INotificationObserver
    {
        private readonly INotificationChannel _channel;

        public SlackNotificationObserver(INotificationChannel channel)
        {
            _channel = channel;
        }

        public void Update(string message)
        {
            _channel.Send($"SLACK: {message}");
        }
    }
}