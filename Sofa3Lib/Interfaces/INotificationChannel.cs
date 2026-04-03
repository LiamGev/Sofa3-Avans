namespace Domain.Interfaces
{
    // Abstraction voor notificatiekanalen.
    // Observers werken tegen deze interface zodat ze niet afhankelijk zijn van
    // één concrete implementatie zoals e-mail, Slack of een legacy service.
    public interface INotificationChannel
    {
        void Send(string message);
    }
}