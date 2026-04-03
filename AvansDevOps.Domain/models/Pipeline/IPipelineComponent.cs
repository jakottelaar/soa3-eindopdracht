namespace AvansDevOps.Domain.Models.Pipeline;

public interface IPipelineComponent
{
    string Name { get; }
    bool Execute();
}