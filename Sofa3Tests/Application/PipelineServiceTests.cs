using App.Services;
using Xunit;

namespace Tests.Application
{
    public class PipelineServiceTests
    {
        [Fact]
        public void RunBasicPipeline_Returns_Expected_Steps()
        {
            var result = PipelineService.RunBasicPipeline();

            Assert.NotNull(result);
            Assert.Contains("Validate pipeline", result);
            Assert.Contains("Prepare pipeline", result);
            Assert.Contains("Build", result);
            Assert.Contains("Test", result);
            Assert.Contains("Publish results", result);
            Assert.Contains("Finalize pipeline", result);
        }

        [Fact]
        public void RunBasicPipeline_Returns_Steps_In_Expected_Order()
        {
            var result = PipelineService.RunBasicPipeline();

            Assert.Equal("Validate pipeline", result[0]);
            Assert.Equal("Prepare pipeline", result[1]);
            Assert.Equal("Build", result[2]);
            Assert.Equal("Test", result[3]);
            Assert.Equal("Publish results", result[4]);
            Assert.Equal("Finalize pipeline", result[5]);
        }

        [Fact]
        public void RunFullPipeline_Returns_Expected_Steps()
        {
            var result = PipelineService.RunFullPipeline();

            Assert.NotNull(result);
            Assert.Contains("Validate pipeline", result);
            Assert.Contains("Prepare full pipeline", result);
            Assert.Contains("Build", result);
            Assert.Contains("Test", result);
            Assert.Contains("Code Analysis", result);
            Assert.Contains("Deploy", result);
            Assert.Contains("Publish results", result);
            Assert.Contains("Finalize pipeline", result);
        }

        [Fact]
        public void RunFullPipeline_Returns_Steps_In_Expected_Order()
        {
            var result = PipelineService.RunFullPipeline();

            Assert.Equal("Validate pipeline", result[0]);
            Assert.Equal("Prepare full pipeline", result[1]);
            Assert.Equal("Build", result[2]);
            Assert.Equal("Test", result[3]);
            Assert.Equal("Code Analysis", result[4]);
            Assert.Equal("Deploy", result[5]);
            Assert.Equal("Publish results", result[6]);
            Assert.Equal("Finalize pipeline", result[7]);
        }

        [Fact]
        public void RunBasicPipeline_DoesNot_Contain_FullPipeline_Only_Steps()
        {
            var result = PipelineService.RunBasicPipeline();

            Assert.DoesNotContain("Code Analysis", result);
            Assert.DoesNotContain("Deploy", result);
            Assert.DoesNotContain("Prepare full pipeline", result);
        }

        [Fact]
        public void RunFullPipeline_Contains_Additional_FullPipeline_Steps()
        {
            var result = PipelineService.RunFullPipeline();

            Assert.Contains("Code Analysis", result);
            Assert.Contains("Deploy", result);
            Assert.Contains("Prepare full pipeline", result);
        }

        [Fact]
        public void RunCompositeReleasePipeline_Returns_Composite_Stages_And_Actions()
        {
            var result = PipelineService.RunCompositeReleasePipeline();

            Assert.NotNull(result);
            Assert.Contains("Validate pipeline", result);
            Assert.Contains("Prepare full pipeline", result);
            Assert.Contains("Stage: Source", result);
            Assert.Contains("Fetch Source", result);
            Assert.Contains("Stage: Build", result);
            Assert.Contains("Install Packages", result);
            Assert.Contains("Build", result);
            Assert.Contains("Stage: Test", result);
            Assert.Contains("Run Unit Tests", result);
            Assert.Contains("Publish Test Results", result);
            Assert.Contains("Stage: Deploy", result);
            Assert.Contains("Deploy to Test Environment", result);
            Assert.Contains("Publish results", result);
            Assert.Contains("Finalize pipeline", result);
        }
    }
}