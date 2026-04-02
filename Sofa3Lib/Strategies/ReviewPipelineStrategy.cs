using Domain.Interfaces;

namespace Domain.Strategies
{
    public class ReviewPipelineStrategy : IPipelineStrategy
    {
        public List<string> Execute()
        {
            return new List<string>
            {
                "Fetch Source",
                "Build",
                "Test",
                "Code Analysis",
                "Publish Test Results"
            };
        }
    }
}