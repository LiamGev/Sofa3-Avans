using Domain.Interfaces;

namespace Domain.Entities
{
    // Composite pattern - Composite node:
    // Een stage bevat andere pipelinecomponenten en voert deze recursief uit.
    // Hierdoor kunnen pipelines hiërarchisch worden opgebouwd uit stages en acties.
    public class PipelineStage : IPipelineComponent
    {
        private readonly List<IPipelineComponent> _children = new();

        public string Name { get; }

        public PipelineStage(string name)
        {
            Name = name;
        }

        // Composite pattern:
        // Deze methode voegt child-componenten toe aan de stage,
        // zodat complexe pipeline-structuren opgebouwd kunnen worden.
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