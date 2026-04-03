using Domain.Interfaces;

namespace Domain.Observers
{
    // Concrete Observer:
    // Deze observer vertaalt domeinmeldingen naar Slack-notificaties.
    // Hierdoor kunnen meerdere kanalen parallel reageren op dezelfde gebeurtenis.
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