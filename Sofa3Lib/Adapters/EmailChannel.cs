using Domain.Interfaces;

namespace Domain.Adapters
{
    // Stub / concrete channel:
    // Dit is een eenvoudige kanaalimplementatie voor testen en demonstratie.
    // De opdracht staat stub-implementaties voor notificatiekanalen expliciet toe.
    public class EmailChannel : INotificationChannel
    {
        public List<string> SentMessages { get; } = new();

        public void Send(string message)
        {
            SentMessages.Add(message);
        }
    }
}