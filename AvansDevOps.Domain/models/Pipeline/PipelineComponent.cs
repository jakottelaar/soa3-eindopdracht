namespace AvansDevOps.Domain.Models.Pipeline;

public abstract class PipelineComponent : IPipelineComponent
{
    public string Name { get; protected set; }
    protected PipelineComponent(string name) => Name = name;
    public abstract bool Execute();
}