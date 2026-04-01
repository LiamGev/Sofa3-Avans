using App.Services;
using Xunit;

namespace Tests.Application
{
    public class PipelineServiceTests
    {
        [Fact]
        public void RunBasicPipeline_Returns_Expected_Steps_In_Order()
        {
            var result = PipelineService.RunBasicPipeline();

            Assert.NotNull(result);
            Assert.Equal(5, result.Count);
            Assert.Equal("Validate pipeline", result[0]);
            Assert.Equal("Prepare pipeline", result[1]);
            Assert.Equal("Build", result[2]);
            Assert.Equal("Test", result[3]);
            Assert.Equal("Finalize pipeline", result[4]);
        }

        [Fact]
        public void RunFullPipeline_Returns_Expected_Steps_In_Order()
        {
            var result = PipelineService.RunFullPipeline();

            Assert.NotNull(result);
            Assert.Equal(7, result.Count);
            Assert.Equal("Validate pipeline", result[0]);
            Assert.Equal("Prepare full pipeline", result[1]);
            Assert.Equal("Build", result[2]);
            Assert.Equal("Test", result[3]);
            Assert.Equal("Code Analysis", result[4]);
            Assert.Equal("Deploy", result[5]);
            Assert.Equal("Finalize full pipeline", result[6]);
        }

        [Fact]
        public void RunBasicPipeline_DoesNot_Contain_FullPipeline_Only_Steps()
        {
            var result = PipelineService.RunBasicPipeline();

            Assert.DoesNotContain("Code Analysis", result);
            Assert.DoesNotContain("Deploy", result);
            Assert.DoesNotContain("Prepare full pipeline", result);
            Assert.DoesNotContain("Finalize full pipeline", result);
        }

        [Fact]
        public void RunFullPipeline_Contains_Additional_FullPipeline_Steps()
        {
            var result = PipelineService.RunFullPipeline();

            Assert.Contains("Code Analysis", result);
            Assert.Contains("Deploy", result);
            Assert.Contains("Prepare full pipeline", result);
            Assert.Contains("Finalize full pipeline", result);
        }
    }
}