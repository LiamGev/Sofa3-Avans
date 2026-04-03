using Domain.Interfaces;

namespace Domain.Strategies
{
    // Concrete Strategy:
    // Deze strategie definieert een minimale pipeline met alleen build en test.
    // Dit past bij de casus waarin meerdere pipelinevarianten ondersteund moeten worden.
    public class BasicPipelineStrategy : IPipelineStrategy
    {
        public List<string> Execute()
        {
            return new List<string>
            {
                "Build",
                "Test"
            };
        }
    }
}