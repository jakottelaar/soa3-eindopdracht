namespace AvansDevOps.Domain.Models.Pipeline.Steps;

public class PackageAction : PipelineComponent
{
    public PackageAction() : base("Packaging") { }

    public override bool Execute()
    {
        Console.WriteLine("-> Applicatie verpakken in Docker container...");
        return true;
    }
}