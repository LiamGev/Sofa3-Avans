using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ISprintState
    {
        string Name { get; }

        void StartSprint(Sprint sprint);
        void FinishSprint(Sprint sprint);
        void StartRelease(Sprint sprint);
        void CancelRelease(Sprint sprint);
        void CloseSprint(Sprint sprint);
    }
}