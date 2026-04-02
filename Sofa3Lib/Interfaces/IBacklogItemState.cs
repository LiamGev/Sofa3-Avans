using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IBacklogItemState
    {
        string Name { get; }

        void Start(BacklogItem item);
        void MoveToReadyForTesting(BacklogItem item);
        void StartTesting(BacklogItem item);
        void ApproveTesting(BacklogItem item);
        void RejectTesting(BacklogItem item);
        void ApproveDone(BacklogItem item);
        void RejectDone(BacklogItem item);
    }
}