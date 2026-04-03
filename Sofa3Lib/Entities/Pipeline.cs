using Domain.Interfaces;

namespace Domain.Entities
{
    // Context voor het Strategy pattern en root voor een Composite pipeline-structuur.
    // Als er geen losse componenten zijn, gebruikt de pipeline een strategie om stappen uit te voeren.
    // Als er wel componenten zijn, wordt de pipeline als samengestelde structuur uitgevoerd.
    public class Pipeline
    {
        // Composite pattern:
        // Een pipeline kan opgebouwd worden uit meerdere componenten,
        // zoals stages en acties die samen één boomstructuur vormen.
        private readonly List<IPipelineComponent> _components = new();

        public string Name { get; private set; }

        // Strategy pattern:
        // De pipeline kan verschillende uitvoerstrategieën gebruiken,
        // bijvoorbeeld basic, full, review of release.
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

        // Strategy + Composite:
        // Als componenten aanwezig zijn, wordt de samengestelde pipeline-structuur uitgevoerd.
        // Anders valt de pipeline terug op de gekozen strategie.
        // Zo ondersteunt het ontwerp zowel simpele als uitbreidbare pipeline-definities.
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

        // Strategy pattern:
        // Hiermee kan het algoritme voor pipeline-uitvoering runtime worden gewisseld
        // zonder de Pipeline-klasse zelf te wijzigen.
        public void ChangeStrategy(IPipelineStrategy strategy)
        {
            Strategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
        }
    }
}