using Domain.Strategies;
using Xunit;

namespace Tests.Domain
{
    public class StrategyTests
    {
        [Fact]
        public void BasicPipelineStrategy_Returns_Build_And_Test()
        {
            var strategy = new BasicPipelineStrategy();

            var steps = strategy.Execute();

            Assert.Contains("Build", steps);
            Assert.Contains("Test", steps);
            Assert.DoesNotContain("Deploy", steps);
            Assert.Equal(2, steps.Count);
        }

        [Fact]
        public void FullPipelineStrategy_Returns_All_Expected_Steps()
        {
            var strategy = new FullPipelineStrategy();

            var steps = strategy.Execute();

            Assert.Contains("Build", steps);
            Assert.Contains("Test", steps);
            Assert.Contains("Code Analysis", steps);
            Assert.Contains("Deploy", steps);
            Assert.Equal(4, steps.Count);
        }
    }
}