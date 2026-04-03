namespace AvansDevOps.Domain.Models.Pipeline.Steps;

public class SourceAction : PipelineComponent
{
    public SourceAction() : base("Source Fetching") { }

    public override bool Execute()
    {
        Console.WriteLine("-> Projectbroncode ophalen uit Git repository...");
        return true;
    }
}