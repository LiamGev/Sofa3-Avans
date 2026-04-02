using Domain.Interfaces;

namespace Domain.Entities
{
    public class PipelineStage : IPipelineComponent
    {
        private readonly List<IPipelineComponent> _children = new();

        public string Name { get; }

        public PipelineStage(string name)
        {
            Name = name;
        }

        public void Add(IPipelineComponent component)
        {
            _children.Add(component);
        }

        public List<string> Execute()
        {
            var results = new List<string> { $"Stage: {Name}" };

            foreach (var child in _children)
            {
                results.AddRange(child.Execute());
            }

            return results;
        }
    }
}