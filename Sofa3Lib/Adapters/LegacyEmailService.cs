namespace sofa3Domain.Adapters
{
    public class LegacyEmailService
    {
        public List<string> LegacyMessages { get; } = new();

        public void SendLegacy(string content)
        {
            LegacyMessages.Add(content);
        }
    }
}