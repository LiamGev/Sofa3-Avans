using Domain.Interfaces;

namespace Domain.Strategies
{
    // Concrete Strategy:
    // Deze strategie bevat een uitgebreidere flow met analyse en deployment.
    // Daardoor kan hetzelfde Pipeline-object ander gedrag krijgen zonder conditionele logica.
    public class FullPipelineStrategy : IPipelineStrategy
    {
        public List<string> Execute()
        {
            return new List<string>
            {
                "Build",
                "Test",
                "Code Analysis",
                "Deploy"
            };
        }
    }
}