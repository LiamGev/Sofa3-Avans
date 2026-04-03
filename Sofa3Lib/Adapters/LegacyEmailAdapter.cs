using Domain.Interfaces;

namespace Domain.Adapters
{
    // Adapter pattern:
    // Deze adapter maakt een bestaande legacy e-mailservice bruikbaar via het nieuwe
    // `INotificationChannel` contract. Daardoor kan oude infrastructuur hergebruikt worden
    // zonder de domeinlaag aan te passen.
    public class LegacyEmailAdapter : INotificationChannel
    {
        private readonly LegacyEmailService _legacyEmailService;

        public LegacyEmailAdapter(LegacyEmailService legacyEmailService)
        {
            _legacyEmailService = legacyEmailService;
        }

        // Adapter pattern:
        // Deze methode vertaalt de nieuwe interface-aanroep naar de oude `SendLegacy` aanroep.
        public void Send(string message)
        {
            _legacyEmailService.SendLegacy(message);
        }
    }
}