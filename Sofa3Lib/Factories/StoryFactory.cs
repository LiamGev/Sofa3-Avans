using sofa3Domain.Entities;
using sofa3Domain.Interfaces;
using sofa3Domain.States;

namespace sofa3Domain.Factories
{
    public class StoryFactory : IBacklogItemFactory
    {
        public BacklogItem Create(string title, string description)
        {
            return new BacklogItem(title, description, "Story", new ToDoState());
        }
    }
}