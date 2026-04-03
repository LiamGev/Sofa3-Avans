using Domain.Interfaces;

namespace Domain.Strategies
{
    // Concrete Strategy:
    // Deze strategie modelleert een releasegerichte pipeline met stappen
    // van source ophalen tot deployment.
    public class ReleasePipelineStrategy : IPipelineStrategy
    {
        public List<string> Execute()
        {
            return new List<string>
            {
                "Fetch Source",
                "Install Packages",
                "Build",
                "Test",
                "Code Analysis",
                "Deploy"
            };
        }
    }
}