using Domain.Interfaces;

namespace Domain.Observers
{
    public class EmailNotificationObserver : INotificationObserver
    {
        private readonly INotificationChannel _channel;

        public EmailNotificationObserver(INotificationChannel channel)
        {
            _channel = channel;
        }

        public void Update(string message)
        {
            _channel.Send($"EMAIL: {message}");
        }
    }
}