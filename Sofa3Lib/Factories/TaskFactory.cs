using Domain.Entities;
using Domain.Interfaces;
using Domain.States;

namespace Domain.Factories
{
    // Factory Method:
    // Deze concrete factory maakt een Task item aan.
    // Door factories te gebruiken blijft de creatie uitbreidbaar volgens Open/Closed.

    public class TaskFactory : IBacklogItemFactory
    {
        public BacklogItem Create(string title, string description)
        {
            return new BacklogItem(title, description, "Task", new ToDoState());
        }
    }
}