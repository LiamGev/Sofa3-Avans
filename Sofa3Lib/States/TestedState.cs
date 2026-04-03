using Domain.Entities;
using Domain.Interfaces;

namespace Domain.States
{

    // Concrete State:
    // In deze toestand is het item functioneel getest,
    // maar nog niet definitief Done totdat ook de definition of done is goedgekeurd.
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

        // State pattern:
        // Deze overgang bevat extra business rules:
        // een item mag alleen Done worden als alle onderliggende activiteiten voltooid zijn.
        // Daardoor zit domeinlogica in de state en niet in losse clientcode.
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