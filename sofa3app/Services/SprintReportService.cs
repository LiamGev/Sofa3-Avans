using Domain.Builders;
using Domain.Entities;

namespace App.Services
{
    public class SprintReportService
    {
        // Builder pattern usage:
        // De service stelt hier een rapport samen via een fluent builder-interface.
        // Dit maakt de opbouw leesbaar en uitbreidbaar zonder telescoping constructors.
        public string GenerateDefaultReport(Sprint sprint)
        {
            var builder = new SprintReportBuilder();

            var report = builder
                .AddHeader("Avans DevOps Sprint Report")
                .AddTeamSection("Developers, Scrum Master, Testers")
                .AddBacklogSummary(sprint)
                .AddPipelineSummary(sprint)
                .AddFooter($"Generated on {DateTime.UtcNow:yyyy-MM-dd}")
                .Build();

            return report.Render();
        }

        public string ExportDefaultReport(Sprint sprint, string format)
        {
            var builder = new SprintReportBuilder();

            var report = builder
                .AddHeader("Avans DevOps Sprint Report")
                .AddTeamSection("Developers, Scrum Master, Testers")
                .AddBacklogSummary(sprint)
                .AddPipelineSummary(sprint)
                .AddFooter($"Generated on {DateTime.UtcNow:yyyy-MM-dd}")
                .Build();

            return report.Export(format);
        }
    }
}