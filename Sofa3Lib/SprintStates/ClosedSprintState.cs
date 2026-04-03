using Domain.Entities;
using Domain.Interfaces;

namespace Domain.SprintStates
{
    // Concrete State:
    // Deze state stelt een volledig afgesloten sprint voor.
    // Verdere wijzigingen zijn hier niet meer toegestaan.
    public class ClosedSprintState : ISprintState
    {
        public string Name => "Closed";

        public void StartSprint(Sprint sprint) => throw new InvalidOperationException("Sprint is closed.");
        public void FinishSprint(Sprint sprint) => throw new InvalidOperationException("Sprint is closed.");
        public void StartRelease(Sprint sprint) => throw new InvalidOperationException("Sprint is closed.");
        public void CancelRelease(Sprint sprint) => throw new InvalidOperationException("Sprint is closed.");
        public void CloseSprint(Sprint sprint) => throw new InvalidOperationException("Sprint is already closed.");
    }
}