using Domain.Interfaces;

namespace Domain.Strategies
{
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