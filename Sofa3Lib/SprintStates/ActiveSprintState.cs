using Domain.Entities;
using Domain.Interfaces;

namespace Domain.SprintStates
{
    public class ActiveSprintState : ISprintState
    {
        public string Name => "Active";

        public void StartSprint(Sprint sprint) => throw new InvalidOperationException("Sprint is already active.");
        public void FinishSprint(Sprint sprint) => sprint.SetState(new FinishedSprintState());
        public void StartRelease(Sprint sprint) => throw new InvalidOperationException("Sprint must finish first.");
        public void CancelRelease(Sprint sprint) => throw new InvalidOperationException("Release has not started.");
        public void CloseSprint(Sprint sprint) => throw new InvalidOperationException("Sprint cannot close directly.");
    }
}