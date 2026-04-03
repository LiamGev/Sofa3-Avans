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

        // Stub-export zoals toegestaan in de opdracht.
        public string Export(string format)
        {
            if (string.IsNullOrWhiteSpace(format))
                throw new ArgumentException("Format cannot be empty.");

            return format.ToLowerInvariant() switch
            {
                "txt" => Render(),
                "pdf" => $"[PDF EXPORT]{Environment.NewLine}{Render()}",
                "png" => $"[PNG EXPORT]{Environment.NewLine}{Render()}",
                _ => throw new InvalidOperationException("Unsupported export format.")
            };
        }
    }
}