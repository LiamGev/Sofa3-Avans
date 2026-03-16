using sofa3Domain.Entities;

namespace sofa3Domain.Interfaces
{
    public interface IBacklogItemState
    {
        string Name { get; }

        void Start(BacklogItem item);
        void MoveToTesting(BacklogItem item);
        void Complete(BacklogItem item);
    }
}