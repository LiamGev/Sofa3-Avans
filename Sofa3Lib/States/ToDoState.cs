using Domain.Entities;
using Domain.Interfaces;

namespace Domain.States
{
    // Concrete State:
    // Deze klasse bevat het gedrag dat geldig is wanneer een backlog item in To Do staat.
    // Alleen toegestane overgangen worden hier toegestaan; ongeldige acties geven een fout.
    public class ToDoState : IBacklogItemState
    {
        public string Name => "To Do";

        public void Start(BacklogItem item)
        {
            item.SetState(new DoingState());
        }

        public void MoveToReadyForTesting(BacklogItem item)
        {
            throw new InvalidOperationException("Cannot move directly from To Do to Ready For Testing.");
        }

        public void StartTesting(BacklogItem item)
        {
            throw new InvalidOperationException("Cannot start testing from To Do.");
        }

        public void ApproveTesting(BacklogItem item)
        {
            throw new InvalidOperationException("Testing has not started yet.");
        }

        public void RejectTesting(BacklogItem item)
        {
            throw new InvalidOperationException("Testing has not started yet.");
        }

        public void ApproveDone(BacklogItem item)
        {
            throw new InvalidOperationException("Item is not tested yet.");
        }

        public void RejectDone(BacklogItem item)
        {
            throw new InvalidOperationException("Item is not tested yet.");
        }
    }
}