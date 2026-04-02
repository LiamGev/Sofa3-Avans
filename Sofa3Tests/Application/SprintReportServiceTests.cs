using App.Services;
using Domain.Entities;
using Domain.Strategies;
using Xunit;

namespace Tests.Application
{
    public class SprintReportServiceTests
    {
        [Fact]
        public void GenerateDefaultReport_Returns_Rendered_Report()
        {
            var sprint = new Sprint(
                "Sprint 1",
                new DateTime(2025, 1, 1),
                new DateTime(2025, 1, 14),
                "Release");

            sprint.SetPipeline(new Pipeline("Release Pipeline", new ReleasePipelineStrategy()));

            var service = new SprintReportService();

            var result = service.GenerateDefaultReport(sprint);

            Assert.Contains("Avans DevOps Sprint Report", result);
            Assert.Contains("Team: Developers, Scrum Master, Testers", result);
            Assert.Contains("Sprint: Sprint 1", result);
            Assert.Contains("Pipeline: Release Pipeline", result);
            Assert.Contains("Generated on", result);
        }
    }
}