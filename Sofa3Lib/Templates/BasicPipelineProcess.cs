using Domain.Entities;

namespace Domain.Templates
{
    // Concrete Template subclass:
    // Deze klasse gebruikt het standaard algoritme van de template zonder overrides.
    // Dat is geschikt voor de eenvoudige pipelinevariant.
    public class BasicPipelineProcess : PipelineProcessTemplate
    {
        public BasicPipelineProcess(Pipeline pipeline) : base(pipeline)
        {
        }
    }
}