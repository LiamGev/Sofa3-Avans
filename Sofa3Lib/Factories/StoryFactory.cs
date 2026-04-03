using Domain.Entities;
using Domain.Interfaces;
using Domain.States;

namespace Domain.Factories
{
    // Factory Method:
    // Deze concrete factory maakt een specifiek type BacklogItem aan, namelijk een Story.
    // Het aanmaakproces is losgekoppeld van de code die het object gebruikt.
    // Daardoor kan de applicatie eenvoudig wisselen tussen Story, Bug, Task en Spike
    // zonder dat de clientlogica hoeft te weten hoe elk object precies wordt opgebouwd.

    public class StoryFactory : IBacklogItemFactory
    {
        // Factory Method:
        // De factory bepaalt hier zelf welk type item wordt gemaakt en met welke beginstatus.
        // Elk backlog item start bewust in de To Do state, passend bij de Scrum-flow uit de casus.

        public BacklogItem Create(string title, string description)
        {
            return new BacklogItem(title, description, "Story", new ToDoState());
        }
    }
}