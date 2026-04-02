using Domain.Entities;
using Domain.Interfaces;

namespace Domain.States
{
    public class ReadyForTestingState : IBacklogItemState
    {
        public string Name => "Ready For Testing";

        public void Start(BacklogItem item)
        {
            throw new InvalidOperationException("Item is already being worked on.");
        }

        public void MoveToReadyForTesting(BacklogItem item)
        {
            throw new InvalidOperationException("Item is already ready for testing.");
        }

        public void StartTesting(BacklogItem item)
        {
            item.SetState(new TestingState());
        }

        public void ApproveTesting(BacklogItem item)
        {
            throw new InvalidOperationException("Testing must start first.");
        }

        public void RejectTesting(BacklogItem item)
        {
            throw new InvalidOperationException("Testing must start first.");
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