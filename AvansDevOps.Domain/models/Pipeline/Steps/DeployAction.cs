namespace AvansDevOps.Domain.Models.Pipeline.Steps;

public class DeployAction : PipelineComponent
{
    public DeployAction() : base("Deployment") { }

    public override bool Execute()
    {
        Console.WriteLine("-> Applicatie deployen naar de cloud...");
        return true;
    }
}