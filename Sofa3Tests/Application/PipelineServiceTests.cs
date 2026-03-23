using App.Services;
using Xunit;

namespace Tests.Application
{
    public class PipelineServiceTests
    {
        [Fact]
        public void RunBasicPipeline_Returns_Expected_Steps()
        {
            var service = new PipelineService();

            var result = service.RunBasicPipeline();

            Assert.Contains("Validate pipeline", result);
            Assert.Contains("Prepare pipeline", result);
            Assert.Contains("Build", result);
            Assert.Contains("Test", result);
            Assert.Contains("Finalize pipeline", result);
        }

        [Fact]
        public void RunFullPipeline_Returns_Expected_Steps()
        {
            var service = new PipelineService();

            var result = service.RunFullPipeline();

            Assert.Contains("Validate pipeline", result);
            Assert.Contains("Prepare full pipeline", result);
            Assert.Contains("Build", result);
            Assert.Contains("Test", result);
            Assert.Contains("Code Analysis", result);
            Assert.Contains("Deploy", result);
            Assert.Contains("Finalize full pipeline", result);
        }
    }
}