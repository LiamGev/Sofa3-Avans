using Domain.Entities;
using Domain.Interfaces;
using Domain.States;

namespace Domain.Factories
{
    public class StoryFactory : IBacklogItemFactory
    {
        public BacklogItem Create(string title, string description)
        {
            return new BacklogItem(title, description, "Story", new ToDoState());
        }
    }
}