using Domain.Entities;
using Domain.Interfaces;
using Domain.States;

namespace Domain.Factories
{
    public class BugFactory : IBacklogItemFactory
    {
        public BacklogItem Create(string title, string description)
        {
            return new BacklogItem(title, description, "Bug", new ToDoState());
        }
    }
}