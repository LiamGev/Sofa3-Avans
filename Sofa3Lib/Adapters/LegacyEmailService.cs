namespace Domain.Adapters
{
    // Adaptee:
    // Dit is de bestaande legacy service met een afwijkende interface.
    // Zonder adapter zou deze niet direct passen in de nieuwe notificatiearchitectuur.
    public class LegacyEmailService
    {
        public List<string> LegacyMessages { get; } = new();

        public void SendLegacy(string content)
        {
            LegacyMessages.Add(content);
        }
    }
}