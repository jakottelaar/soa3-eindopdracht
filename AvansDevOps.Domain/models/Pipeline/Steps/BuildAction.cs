namespace AvansDevOps.Domain.Models.Pipeline.Steps;

public class BuildAction : PipelineComponent
{
    public BuildAction() : base("Build") { }

    public override bool Execute()
    {
        Console.WriteLine("-> Applicatie bouwen...");
        return true; 
    }
}