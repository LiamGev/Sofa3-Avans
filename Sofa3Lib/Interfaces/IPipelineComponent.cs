namespace Domain.Interfaces
{
    // Composite pattern:
    // Dit is de gemeenschappelijke interface voor zowel leaf- als composite-objecten
    // binnen de pipelineboom.
    public interface IPipelineComponent
    {
        string Name { get; }
        List<string> Execute();
    }
}