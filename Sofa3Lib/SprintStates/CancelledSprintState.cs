using Domain.Entities;
using Domain.Interfaces;

namespace Domain.SprintStates
{
    // Concrete State:
    // Deze eindtoestand maakt duidelijk dat een mislukte of geannuleerde release
    // niet zomaar terug de normale sprintflow in kan.
    public class CancelledSprintState : ISprintState
    {
        public string Name => "Cancelled";

        public void StartSprint(Sprint sprint) => throw new InvalidOperationException("Sprint is cancelled.");
        public void FinishSprint(Sprint sprint) => throw new InvalidOperationException("Sprint is cancelled.");
        public void StartRelease(Sprint sprint) => throw new InvalidOperationException("Sprint is cancelled.");
        public void CancelRelease(Sprint sprint) => throw new InvalidOperationException("Release is already cancelled.");
        public void CloseSprint(Sprint sprint) => throw new InvalidOperationException("Cancelled sprint cannot be closed.");
    }
}