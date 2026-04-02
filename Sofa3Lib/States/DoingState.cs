using Domain.Entities;
using Domain.Interfaces;

namespace Domain.States
{
    public class DoingState : IBacklogItemState
    {
        public string Name => "Doing";

        public void Start(BacklogItem item)
        {
            throw new InvalidOperationException("Item is already in progress.");
        }

        public void MoveToReadyForTesting(BacklogItem item)
        {
            item.SetState(new ReadyForTestingState());
            item.NotifyObservers($"Testers should review backlog item '{item.Title}'.");
        }

        public void StartTesting(BacklogItem item)
        {
            throw new InvalidOperationException("Move item to Ready For Testing first.");
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