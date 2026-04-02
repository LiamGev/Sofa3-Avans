using Domain.Entities;
using Domain.Interfaces;

namespace Domain.Builders
{
    public class SprintReportBuilder : ISprintReportBuilder
    {
        private readonly SprintReport _report = new();

        public ISprintReportBuilder AddHeader(string header)
        {
            _report.Header = header;
            return this;
        }

        public ISprintReportBuilder AddFooter(string footer)
        {
            _report.Footer = footer;
            return this;
        }

        public ISprintReportBuilder AddTeamSection(string teamInfo)
        {
            _report.Sections.Add($"Team: {teamInfo}");
            return this;
        }

        public ISprintReportBuilder AddBacklogSummary(Sprint sprint)
        {
            _report.Sections.Add($"Sprint: {sprint.Name}");
            _report.Sections.Add($"Sprint State: {sprint.CurrentState.Name}");
            _report.Sections.Add($"Backlog items: {sprint.BacklogItems.Count}");
            return this;
        }

        public ISprintReportBuilder AddPipelineSummary(Sprint sprint)
        {
            var pipelineName = sprint.Pipeline?.Name ?? "No pipeline configured";
            _report.Sections.Add($"Pipeline: {pipelineName}");
            return this;
        }

        public SprintReport Build()
        {
            return _report;
        }
    }
}