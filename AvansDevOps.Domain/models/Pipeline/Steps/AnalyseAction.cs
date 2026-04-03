namespace AvansDevOps.Domain.Models.Pipeline.Steps;

public class AnalyzeAction : PipelineComponent
{
    public AnalyzeAction() : base("Code Analysis") { }

    public override bool Execute()
    {
        Console.WriteLine("-> Code analyseren...");
        return true; 
    }
}