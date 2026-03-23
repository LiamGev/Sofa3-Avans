using Domain.Interfaces;

namespace Domain.Adapters
{
    public class LegacyEmailAdapter : INotificationObserver
    {
        private readonly LegacyEmailService _legacyEmailService;

        public LegacyEmailAdapter(LegacyEmailService legacyEmailService)
        {
            _legacyEmailService = legacyEmailService;
        }

        public void Update(string message)
        {
            _legacyEmailService.SendLegacy(message);
        }
    }
}