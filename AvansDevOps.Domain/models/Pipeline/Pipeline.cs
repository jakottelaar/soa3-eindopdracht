namespace AvansDevOps.Domain.Models.Pipeline;

public class Pipeline : PipelineComponent
{
    private readonly List<IPipelineComponent> steps = [];
    public Pipeline(string name) : base(name) { }
    public void AddStep(IPipelineComponent step) => steps.Add(step);
    public void RemoveStep(IPipelineComponent step) => steps.Remove(step);

    public override bool Execute()
    {
        foreach (var step in steps)
        {
            if (!step.Execute())
                return false;
        }
        return true;
    }
}
