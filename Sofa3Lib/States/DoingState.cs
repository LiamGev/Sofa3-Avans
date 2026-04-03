using Domain.Entities;
using Domain.Interfaces;

namespace Domain.States
{
    // Concrete State:
    // Deze state representeert werk in uitvoering.
    // Vanuit hier mag het item naar Ready For Testing, conform de Scrum-flow uit de opdracht.
    public class DoingState : IBacklogItemState
    {
        public string Name => "Doing";

        public void Start(BacklogItem item)
        {
            throw new InvalidOperationException("Item is already in progress.");
        }

        // State + Observer:
        // Bij deze overgang verandert niet alleen de status,
        // maar worden testers ook direct geïnformeerd via observers.
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