using Domain.Interfaces;

namespace Domain.Strategies
{
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