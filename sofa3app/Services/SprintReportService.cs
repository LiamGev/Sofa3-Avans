using Domain.Builders;
using Domain.Entities;

namespace App.Services
{
    public class SprintReportService
    {
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