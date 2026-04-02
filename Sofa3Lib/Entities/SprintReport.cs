namespace Domain.Entities
{
    public class SprintReport
    {
        public string Header { get; set; } = string.Empty;
        public string Footer { get; set; } = string.Empty;
        public List<string> Sections { get; } = new();

        public string Render()
        {
            var lines = new List<string>();

            if (!string.IsNullOrWhiteSpace(Header))
                lines.Add(Header);

            lines.AddRange(Sections);

            if (!string.IsNullOrWhiteSpace(Footer))
                lines.Add(Footer);

            return string.Join(Environment.NewLine, lines);
        }
    }
}