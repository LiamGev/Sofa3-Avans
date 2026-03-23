using Domain.Interfaces;

namespace Domain.Strategies
{
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