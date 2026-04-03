namespace Domain.Interfaces
{
    // Observer pattern:
    // Dit is het abstracte observer-contract dat alle luisteraars moeten implementeren.
    public interface INotificationObserver
    {
        void Update(string message);
    }
}