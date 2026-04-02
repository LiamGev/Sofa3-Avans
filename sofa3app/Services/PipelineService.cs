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

        public static List<string> RunCompositeReleasePipeline()
        {
            var strategy = new ReleasePipelineStrategy();
            var pipeline = new Pipeline("Composite Release Pipeline", strategy);

            var sourceStage = new PipelineStage("Source");
            sourceStage.Add(new PipelineAction("Fetch Source"));

            var buildStage = new PipelineStage("Build");
            buildStage.Add(new PipelineAction("Install Packages"));
            buildStage.Add(new PipelineAction("Build"));

            var testStage = new PipelineStage("Test");
            testStage.Add(new PipelineAction("Run Unit Tests"));
            testStage.Add(new PipelineAction("Publish Test Results"));

            var deployStage = new PipelineStage("Deploy");
            deployStage.Add(new PipelineAction("Deploy to Test Environment"));

            pipeline.AddComponent(sourceStage);
            pipeline.AddComponent(buildStage);
            pipeline.AddComponent(testStage);
            pipeline.AddComponent(deployStage);

            var process = new FullPipelineProcess(pipeline);
            return process.Execute();
        }
    }
}