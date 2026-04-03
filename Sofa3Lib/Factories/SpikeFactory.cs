using Domain.Entities;
using Domain.Interfaces;
using Domain.States;

namespace Domain.Factories
{
    // Factory Method:
    // Deze concrete factory maakt een Spike item aan.
    // Dit past goed bij de opdracht omdat meerdere backlog-itemvarianten ondersteund moeten worden.

    public class SpikeFactory : IBacklogItemFactory
    {
        public BacklogItem Create(string title, string description)
        {
            return new BacklogItem(title, description, "Spike", new ToDoState());
        }
    }
}