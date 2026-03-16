using sofa3Domain.Entities;
using sofa3Domain.Interfaces;

namespace sofa3Domain.States
{
    public class ToDoState : IBacklogItemState
    {
        public string Name => "To Do";

        public void Start(BacklogItem item)
        {
            item.SetState(new InProgressState());
        }

        public void MoveToTesting(BacklogItem item)
        {
            throw new InvalidOperationException("Cannot move directly from To Do to Testing.");
        }

        public void Complete(BacklogItem item)
        {
            throw new InvalidOperationException("Cannot complete an item directly from To Do.");
        }
    }
}