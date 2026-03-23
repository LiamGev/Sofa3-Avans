using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IBacklogItemState
    {
        string Name { get; }

        void Start(BacklogItem item);
        void MoveToTesting(BacklogItem item);
        void Complete(BacklogItem item);
    }
}