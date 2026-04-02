using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ISprintReportBuilder
    {
        ISprintReportBuilder AddHeader(string header);
        ISprintReportBuilder AddFooter(string footer);
        ISprintReportBuilder AddTeamSection(string teamInfo);
        ISprintReportBuilder AddBacklogSummary(Sprint sprint);
        ISprintReportBuilder AddPipelineSummary(Sprint sprint);
        SprintReport Build();
    }
}