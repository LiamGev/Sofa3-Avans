using Domain.Interfaces;

namespace Domain.Observers
{
    // Concrete Observer:
    // Deze observer reageert op notificaties van een BacklogItem
    // en zet de melding door via een notificatiekanaal voor e-mail.
    public class EmailNotificationObserver : INotificationObserver
    {
        // Bridge naar een notificatiekanaal:
        // de observer weet dat hij e-mailgedrag uitvoert,
        // maar blijft los van de concrete implementatie van het kanaal.
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