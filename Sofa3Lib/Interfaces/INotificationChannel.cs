namespace Domain.Interfaces
{
    public interface INotificationChannel
    {
        void Send(string message);
    }
}