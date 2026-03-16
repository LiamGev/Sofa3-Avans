using sofa3Domain.Interfaces;

namespace sofa3Domain.Strategies
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