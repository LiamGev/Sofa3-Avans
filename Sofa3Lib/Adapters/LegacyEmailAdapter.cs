using Domain.Interfaces;

namespace Domain.Adapters
{
    public class LegacyEmailAdapter : INotificationChannel
    {
        private readonly LegacyEmailService _legacyEmailService;

        public LegacyEmailAdapter(LegacyEmailService legacyEmailService)
        {
            _legacyEmailService = legacyEmailService;
        }

        public void Send(string message)
        {
            _legacyEmailService.SendLegacy(message);
        }
    }
}