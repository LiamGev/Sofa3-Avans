using Domain.Interfaces;

namespace Domain.Entities
{
    public class Pipeline
    {
        private readonly List<IPipelineComponent> _components = new();

        public string Name { get; private set; }
        public IPipelineStrategy Strategy { get; private set; }

        public Pipeline(string name, IPipelineStrategy strategy)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Pipeline name cannot be empty.");

            Name = name;
            Strategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
        }

        public void AddComponent(IPipelineComponent component)
        {
            _components.Add(component);
        }

        public List<string> Run()
        {
            if (_components.Any())
            {
                var result = new List<string>();

                foreach (var component in _components)
                {
                    result.AddRange(component.Execute());
                }

                return result;
            }

            return Strategy.Execute();
        }

        public void ChangeStrategy(IPipelineStrategy strategy)
        {
            Strategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
        }
    }
}