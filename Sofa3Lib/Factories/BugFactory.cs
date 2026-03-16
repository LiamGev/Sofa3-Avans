using sofa3Domain.Entities;
using sofa3Domain.Interfaces;
using sofa3Domain.States;

namespace sofa3Domain.Factories
{
    public class BugFactory : IBacklogItemFactory
    {
        public BacklogItem Create(string title, string description)
        {
            return new BacklogItem(title, description, "Bug", new ToDoState());
        }
    }
}