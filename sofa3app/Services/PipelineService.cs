using Domain.Entities;
using Domain.Strategies;
using Domain.Templates;

namespace App.Services
{
    public class PipelineService
    {
        public static List<string> RunBasicPipeline()
        {
            var strategy = new BasicPipelineStrategy();
            var pipeline = new Pipeline("Basic Pipeline", strategy);
            var process = new BasicPipelineProcess(pipeline);

            return process.Execute();
        }

        public static List<string> RunFullPipeline()
        {
            var strategy = new FullPipelineStrategy();
            var pipeline = new Pipeline("Full Pipeline", strategy);
            var process = new FullPipelineProcess(pipeline);

            return process.Execute();
        }
    }
}