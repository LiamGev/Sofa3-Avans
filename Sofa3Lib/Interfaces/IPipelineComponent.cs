namespace Domain.Interfaces
{
    public interface IPipelineComponent
    {
        string Name { get; }
        List<string> Execute();
    }
}