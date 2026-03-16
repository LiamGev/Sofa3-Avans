using sofa3Domain.Entities;
using sofa3Domain.Interfaces;

namespace sofa3Domain.States
{
    public class InProgressState : IBacklogItemState
    {
        public string Name => "In Progress";

        public void Start(BacklogItem item)
        {
            throw new InvalidOperationException("Item is already in progress.");
        }

        public void MoveToTesting(BacklogItem item)
        {
            item.SetState(new TestingState());
        }

        public void Complete(BacklogItem item)
        {
            throw new InvalidOperationException("Move item to Testing first.");
        }
    }
}