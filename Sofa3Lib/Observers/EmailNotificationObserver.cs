using sofa3Domain.Interfaces;

namespace sofa3Domain.Observers
{
    public class EmailNotificationObserver : INotificationObserver
    {
        public List<string> SentMessages { get; } = new();

        public void Update(string message)
        {
            SentMessages.Add($"EMAIL: {message}");
        }
    }
}