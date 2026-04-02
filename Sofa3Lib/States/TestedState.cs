using Domain.Entities;
using Domain.Interfaces;

namespace Domain.States
{
    public class TestedState : IBacklogItemState
    {
        public string Name => "Tested";

        public void Start(BacklogItem item)
        {
            throw new InvalidOperationException("Item is already tested.");
        }

        public void MoveToReadyForTesting(BacklogItem item)
        {
            throw new InvalidOperationException("Item is already beyond Ready For Testing.");
        }

        public void StartTesting(BacklogItem item)
        {
            throw new InvalidOperationException("Testing is already completed.");
        }

        public void ApproveTesting(BacklogItem item)
        {
            throw new InvalidOperationException("Testing is already approved.");
        }

        public void RejectTesting(BacklogItem item)
        {
            throw new InvalidOperationException("Testing is already completed.");
        }

        public void ApproveDone(BacklogItem item)
        {
            if (!item.AreAllActivitiesCompleted())
                throw new InvalidOperationException("All activities must be completed before moving to Done.");

            item.SetState(new DoneState());
        }

        public void RejectDone(BacklogItem item)
        {
            item.SetState(new ReadyForTestingState());
        }
    }
}