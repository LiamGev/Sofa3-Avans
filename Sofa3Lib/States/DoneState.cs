using sofa3Domain.Entities;
using sofa3Domain.Interfaces;

namespace sofa3Domain.States
{
    public class DoneState : IBacklogItemState
    {
        public string Name => "Done";

        public void Start(BacklogItem item)
        {
            throw new InvalidOperationException("Cannot restart a completed item.");
        }

        public void MoveToTesting(BacklogItem item)
        {
            throw new InvalidOperationException("Completed item cannot move to Testing.");
        }

        public void Complete(BacklogItem item)
        {
            throw new InvalidOperationException("Item is already completed.");
        }
    }
}