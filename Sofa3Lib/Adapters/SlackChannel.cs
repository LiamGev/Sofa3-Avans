using Domain.Interfaces;

namespace Domain.Adapters
{
    // Stub / concrete channel:
    // Ook dit is een eenvoudige testbare implementatie van een notificatiekanaal.
    // Hierdoor kan domeinlogica getest worden zonder echte externe integratie.
    public class SlackChannel : INotificationChannel
    {
        public List<string> SentMessages { get; } = new();

        public void Send(string message)
        {
            SentMessages.Add(message);
        }
    }
}