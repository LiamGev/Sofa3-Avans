using sofa3Domain.Entities;
using sofa3Domain.Interfaces;

namespace sofa3Domain.States
{
    public class TestingState : IBacklogItemState
    {
        public string Name => "Testing";

        public void Start(BacklogItem item)
        {
            throw new InvalidOperationException("Item is already beyond In Progress.");
        }

        public void MoveToTesting(BacklogItem item)
        {
            throw new InvalidOperationException("Item is already in Testing.");
        }

        public void Complete(BacklogItem item)
        {
            item.SetState(new DoneState());
        }
    }
}