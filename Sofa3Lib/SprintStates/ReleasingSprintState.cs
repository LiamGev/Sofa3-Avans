using Domain.Entities;
using Domain.Interfaces;

namespace Domain.SprintStates
{
    // Concrete State:
    // Deze state representeert een sprint waarvan de release loopt.
    // Tijdens deze fase zijn alleen release-gerelateerde acties toegestaan.
    public class ReleasingSprintState : ISprintState
    {
        public string Name => "Releasing";

        public void StartSprint(Sprint sprint) => throw new InvalidOperationException("Sprint is already in release.");
        public void FinishSprint(Sprint sprint) => throw new InvalidOperationException("Sprint is already finished.");

        public void StartRelease(Sprint sprint) => throw new InvalidOperationException("Release already started.");

        public void CancelRelease(Sprint sprint) => sprint.SetState(new CancelledSprintState());

        public void CloseSprint(Sprint sprint) => sprint.SetState(new ClosedSprintState());
    }
}