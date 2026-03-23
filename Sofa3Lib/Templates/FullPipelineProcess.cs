using Domain.Entities;

namespace Domain.Templates
{
    public class FullPipelineProcess : PipelineProcessTemplate
    {
        public FullPipelineProcess(Pipeline pipeline) : base(pipeline)
        {
        }

        protected override void Prepare()
        {
            Steps.Add("Prepare full pipeline");
        }

        protected override void FinalizeProcess()
        {
            Steps.Add("Finalize full pipeline");
        }
    }
}