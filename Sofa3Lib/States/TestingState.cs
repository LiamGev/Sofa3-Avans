using Domain.Entities;
using Domain.Interfaces;

namespace Domain.States
{
    // Concrete State:
    // Deze state bevat de regels tijdens de testfase.
    // Goedkeuren leidt naar Tested; afkeuren stuurt het item terug naar To Do,
    // zoals in de opdrachttekst is beschreven.
    public class TestingState : IBacklogItemState
    {
        public string Name => "Testing";

        public void Start(BacklogItem item)
        {
            throw new InvalidOperationException("Item is already beyond Doing.");
        }

        public void MoveToReadyForTesting(BacklogItem item)
        {
            throw new InvalidOperationException("Item is already in testing.");
        }

        public void StartTesting(BacklogItem item)
        {
            throw new InvalidOperationException("Testing has already started.");
        }

        public void ApproveTesting(BacklogItem item)
        {
            item.SetState(new TestedState());
        }

        public void RejectTesting(BacklogItem item)
        {
            item.SetState(new ToDoState());
            item.NotifyObservers($"Testing failed for '{item.Title}'. Scrum master should be informed.");
        }

        public void ApproveDone(BacklogItem item)
        {
            throw new InvalidOperationException("Item must first become Tested.");
        }

        public void RejectDone(BacklogItem item)
        {
            throw new InvalidOperationException("Item must first become Tested.");
        }
    }
}