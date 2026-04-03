using Domain.Entities;

namespace Domain.Templates
{
    // Concrete Template subclass:
    // Deze klasse hergebruikt het vaste processkelet,
    // maar specialiseert enkele stappen voor een uitgebreidere pipelineflow.
    public class FullPipelineProcess : PipelineProcessTemplate
    {
        public FullPipelineProcess(Pipeline pipeline) : base(pipeline)
        {
        }

        // Template Method override:
        // Alleen deze stap wordt aangepast voor de full pipeline,
        // terwijl de globale uitvoervolgorde gelijk blijft.
        protected override void Prepare()
        {
            Steps.Add("Prepare full pipeline");
        }

        // Template Method override:
        // Ook de afronding kan per pipelinevariant verschillen,
        // zonder het basale algoritme te dupliceren.
        protected override void FinalizeProcess()
        {
            Steps.Add("Finalize full pipeline");
        }
    }
}