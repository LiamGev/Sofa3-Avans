using Domain.Entities;
using Domain.Interfaces;
using Domain.States;

namespace Domain.Factories
{
    // Factory Method:
    // Deze factory encapsuleert het aanmaken van een Bug backlog item.
    // Dit voorkomt duplicated creation logic in services of controllers.

    public class BugFactory : IBacklogItemFactory
    {
        public BacklogItem Create(string title, string description)
        {
            return new BacklogItem(title, description, "Bug", new ToDoState());
        }
    }
}