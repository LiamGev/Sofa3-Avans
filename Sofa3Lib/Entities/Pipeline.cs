using sofa3Domain.Interfaces;

namespace sofa3Domain.Entities
{
    public class Pipeline
    {
        public string Name { get; private set; }
        public IPipelineStrategy Strategy { get; private set; }

        public Pipeline(string name, IPipelineStrategy strategy)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Pipeline name cannot be empty.");

            Name = name;
            Strategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
        }

        public List<string> Run()
        {
            return Strategy.Execute();
        }

        public void ChangeStrategy(IPipelineStrategy strategy)
        {
            Strategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
        }
    }
}