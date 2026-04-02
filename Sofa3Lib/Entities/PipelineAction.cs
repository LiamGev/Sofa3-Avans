using Domain.Interfaces;

namespace Domain.Entities
{
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