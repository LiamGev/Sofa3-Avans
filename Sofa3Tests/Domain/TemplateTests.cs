using Domain.Entities;
using Domain.Strategies;
using Domain.Templates;
using Xunit;

namespace Tests.Domain
{
    public class TemplateTests
    {
        [Fact]
        public void BasicPipelineProcess_Executes_Default_Template_Order()
        {
            var pipeline = new Pipeline("Basic", new BasicPipelineStrategy());
            var process = new BasicPipelineProcess(pipeline);

            var result = process.Execute();

            Assert.Equal("Validate pipeline", result[0]);
            Assert.Equal("Prepare pipeline", result[1]);
            Assert.Contains("Build", result);
            Assert.Contains("Test", result);
            Assert.Equal("Finalize pipeline", result[^1]);
        }

        [Fact]
        public void FullPipelineProcess_Uses_Overridden_Prepare_And_Finalize()
        {
            var pipeline = new Pipeline("Full", new FullPipelineStrategy());
            var process = new FullPipelineProcess(pipeline);

            var result = process.Execute();

            Assert.Equal("Validate pipeline", result[0]);
            Assert.Equal("Prepare full pipeline", result[1]);
            Assert.Contains("Code Analysis", result);
            Assert.Contains("Deploy", result);
            Assert.Equal("Finalize full pipeline", result[^1]);
        }
    }
}