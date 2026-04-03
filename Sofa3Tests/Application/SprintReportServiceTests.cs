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

        [Fact]
        public void ExportDefaultReport_AsPdf_ReturnsPdfStub()
        {
            var sprint = new Sprint(
                "Sprint 1",
                new DateTime(2025, 1, 1),
                new DateTime(2025, 1, 14),
                "Release");

            sprint.SetPipeline(new Pipeline("Release Pipeline", new ReleasePipelineStrategy()));

            var service = new SprintReportService();

            var result = service.ExportDefaultReport(sprint, "pdf");

            Assert.Contains("[PDF EXPORT]", result);
            Assert.Contains("Sprint: Sprint 1", result);
        }

        [Fact]
        public void ExportDefaultReport_AsPng_ReturnsPngStub()
        {
            var sprint = new Sprint(
                "Sprint 1",
                new DateTime(2025, 1, 1),
                new DateTime(2025, 1, 14),
                "Release");

            sprint.SetPipeline(new Pipeline("Release Pipeline", new ReleasePipelineStrategy()));

            var service = new SprintReportService();

            var result = service.ExportDefaultReport(sprint, "png");

            Assert.Contains("[PNG EXPORT]", result);
            Assert.Contains("Pipeline: Release Pipeline", result);
        }
    }
}