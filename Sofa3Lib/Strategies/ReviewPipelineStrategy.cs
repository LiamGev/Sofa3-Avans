using Domain.Interfaces;

namespace Domain.Strategies
{
    // Concrete Strategy:
    // Deze strategie ondersteunt een pipelinevariant voor review-sprints,
    // waarbij publicatie van testresultaten relevant is maar deployment optioneel kan zijn.
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