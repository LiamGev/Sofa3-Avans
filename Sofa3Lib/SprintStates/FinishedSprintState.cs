using Domain.Entities;
using Domain.Interfaces;

namespace Domain.SprintStates
{
    // Concrete State:
    // Een afgeronde sprint kan nu, afhankelijk van het type en de configuratie,
    // worden gesloten of een releaseproces starten.
    public class FinishedSprintState : ISprintState
    {
        public string Name => "Finished";

        public void StartSprint(Sprint sprint) => throw new InvalidOperationException("Sprint is already finished.");

        public void FinishSprint(Sprint sprint) => throw new InvalidOperationException("Sprint is already finished.");

        // State pattern:
        // Release starten is alleen toegestaan als een pipeline aan de sprint gekoppeld is.
        // Daarmee wordt een belangrijke business rule uit de casus centraal afgedwongen.
        public void StartRelease(Sprint sprint)
        {
            if (!sprint.SprintType.Equals("Release", StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("Only release sprints can start a release.");

            if (sprint.Pipeline == null)
                throw new InvalidOperationException("Sprint must have a pipeline before release can start.");

            sprint.SetState(new ReleasingSprintState());
        }

        public void CancelRelease(Sprint sprint) => throw new InvalidOperationException("Release has not started.");

        public void CloseSprint(Sprint sprint)
        {
            if (!sprint.CanBeClosed())
                throw new InvalidOperationException("A review sprint can only be closed after a review summary has been uploaded.");

            sprint.SetState(new ClosedSprintState());
        }
    }
}