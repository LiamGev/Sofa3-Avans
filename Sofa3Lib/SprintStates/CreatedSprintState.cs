using Domain.Entities;
using Domain.Interfaces;

namespace Domain.SprintStates
{
    public class CreatedSprintState : ISprintState
    {
        public string Name => "Created";

        public void StartSprint(Sprint sprint) => sprint.SetState(new ActiveSprintState());
        public void FinishSprint(Sprint sprint) => throw new InvalidOperationException("Sprint must start first.");
        public void StartRelease(Sprint sprint) => throw new InvalidOperationException("Sprint must finish first.");
        public void CancelRelease(Sprint sprint) => throw new InvalidOperationException("Release has not started.");
        public void CloseSprint(Sprint sprint) => throw new InvalidOperationException("Sprint cannot close yet.");
    }
}