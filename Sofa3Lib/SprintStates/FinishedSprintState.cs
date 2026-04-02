using Domain.Entities;
using Domain.Interfaces;

namespace Domain.SprintStates
{
    public class FinishedSprintState : ISprintState
    {
        public string Name => "Finished";

        public void StartSprint(Sprint sprint) => throw new InvalidOperationException("Sprint is already finished.");

        public void FinishSprint(Sprint sprint) => throw new InvalidOperationException("Sprint is already finished.");

        public void StartRelease(Sprint sprint)
        {
            if (sprint.Pipeline == null)
                throw new InvalidOperationException("Sprint must have a pipeline before release can start.");

            sprint.SetState(new ReleasingSprintState());
        }

        public void CancelRelease(Sprint sprint) => throw new InvalidOperationException("Release has not started.");
        public void CloseSprint(Sprint sprint) => sprint.SetState(new ClosedSprintState());
    }
}