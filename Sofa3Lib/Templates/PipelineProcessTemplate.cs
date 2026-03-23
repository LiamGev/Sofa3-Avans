using Domain.Entities;

namespace Domain.Templates
{
    public abstract class PipelineProcessTemplate
    {
        protected readonly Pipeline Pipeline;
        protected readonly List<string> Steps = new();

        protected PipelineProcessTemplate(Pipeline pipeline)
        {
            Pipeline = pipeline ?? throw new ArgumentNullException(nameof(pipeline));
        }

        public List<string> Execute()
        {
            Validate();
            Prepare();
            RunPipeline();
            FinalizeProcess();

            return Steps;
        }

        protected virtual void Validate()
        {
            Steps.Add("Validate pipeline");
        }

        protected virtual void Prepare()
        {
            Steps.Add("Prepare pipeline");
        }

        protected virtual void RunPipeline()
        {
            Steps.AddRange(Pipeline.Run());
        }

        protected virtual void FinalizeProcess()
        {
            Steps.Add("Finalize pipeline");
        }
    }
}