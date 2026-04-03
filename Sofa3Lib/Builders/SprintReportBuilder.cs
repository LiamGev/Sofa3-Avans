using Domain.Entities;
using Domain.Interfaces;

namespace Domain.Builders
{
    // Builder pattern:
    // Deze builder bouwt een SprintReport stap voor stap op.
    // Dat is passend omdat een rapport uit meerdere optionele onderdelen bestaat,
    // zoals header, footer, teaminfo en samenvattingen.
    public class SprintReportBuilder : ISprintReportBuilder
    {
        // Builder pattern:
        // Dit is het product dat tijdens meerdere builder-stappen geleidelijk wordt samengesteld.
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

        // Builder pattern:
        // Pas aan het einde wordt het volledig opgebouwde rapport teruggegeven aan de client.
        public SprintReport Build()
        {
            return _report;
        }
    }
}