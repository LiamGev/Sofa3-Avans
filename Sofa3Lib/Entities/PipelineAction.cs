using Domain.Interfaces;

namespace Domain.Entities
{
    // Composite pattern - Leaf node:
    // Een action is een ondeelbare pipelinecomponent zonder kinderen.
    // Samen met PipelineStage deelt deze klasse dezelfde interface.
    public class PipelineAction : IPipelineComponent
    {
        public string Name { get; }

        public PipelineAction(string name)
        {
            Name = name;
        }

        public List<string> Execute()
        {
            return new List<string> { Name };
        }
    }
}