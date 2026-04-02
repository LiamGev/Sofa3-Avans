using Domain.Entities;
using Domain.Interfaces;

namespace Domain.States
{
    public class DoneState : IBacklogItemState
    {
        public string Name => "Done";

        public void Start(BacklogItem item)
        {
            throw new InvalidOperationException("Cannot restart a completed item.");
        }

        public void MoveToReadyForTesting(BacklogItem item)
        {
            throw new InvalidOperationException("Completed item cannot move directly to Ready For Testing.");
        }

        public void StartTesting(BacklogItem item)
        {
            throw new InvalidOperationException("Completed item cannot be tested again directly.");
        }

        public void ApproveTesting(BacklogItem item)
        {
            throw new InvalidOperationException("Item is already completed.");
        }

        public void RejectTesting(BacklogItem item)
        {
            throw new InvalidOperationException("Item is already completed.");
        }

        public void ApproveDone(BacklogItem item)
        {
            throw new InvalidOperationException("Item is already completed.");
        }

        public void RejectDone(BacklogItem item)
        {
            item.SetState(new ToDoState());
        }
    }
}