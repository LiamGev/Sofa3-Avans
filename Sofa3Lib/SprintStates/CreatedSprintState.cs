using Domain.Entities;
using Domain.Interfaces;

namespace Domain.SprintStates
{
    // Concrete State:
    // De sprint is aangemaakt maar nog niet actief.
    // Alleen vanuit deze toestand mag de sprint gestart worden.
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