using Domain.Builders;
using Domain.Entities;
using Domain.Strategies;
using Xunit;

namespace Tests.Domain
{
    public class SprintReportBuilderTests
    {
        [Fact]
        public void SprintReport_Render_IncludesHeaderSectionsAndFooter()
        {
            var report = new SprintReport
            {
                Header = "Header",
                Footer = "Footer"
            };

            report.Sections.Add("Section 1");
            report.Sections.Add("Section 2");

            var rendered = report.Render();

            Assert.Contains("Header", rendered);
            Assert.Contains("Section 1", rendered);
            Assert.Contains("Section 2", rendered);
            Assert.Contains("Footer", rendered);
        }

        [Fact]
        public void SprintReportBuilder_Builds_Report_With_All_Parts()
        {
            var sprint = new Sprint(
                "Sprint 1",
                new DateTime(2025, 1, 1),
                new DateTime(2025, 1, 14),
                "Release");

            sprint.SetPipeline(new Pipeline("Release Pipeline", new ReleasePipelineStrategy()));

            var builder = new SprintReportBuilder();

            var report = builder
                .AddHeader("Avans Header")
                .AddTeamSection("Team A")
                .AddBacklogSummary(sprint)
                .AddPipelineSummary(sprint)
                .AddFooter("Avans Footer")
                .Build();

            var rendered = report.Render();

            Assert.Contains("Avans Header", rendered);
            Assert.Contains("Team: Team A", rendered);
            Assert.Contains("Sprint: Sprint 1", rendered);
            Assert.Contains("Sprint State: Created", rendered);
            Assert.Contains("Backlog items: 0", rendered);
            Assert.Contains("Pipeline: Release Pipeline", rendered);
            Assert.Contains("Avans Footer", rendered);
        }

        [Fact]
        public void SprintReportBuilder_PipelineSummary_Shows_NoPipelineConfigured_WhenPipelineIsMissing()
        {
            var sprint = new Sprint(
                "Sprint 1",
                new DateTime(2025, 1, 1),
                new DateTime(2025, 1, 14),
                "Review");

            var builder = new SprintReportBuilder();

            var report = builder
                .AddPipelineSummary(sprint)
                .Build();

            var rendered = report.Render();

            Assert.Contains("Pipeline: No pipeline configured", rendered);
        }
    }
}