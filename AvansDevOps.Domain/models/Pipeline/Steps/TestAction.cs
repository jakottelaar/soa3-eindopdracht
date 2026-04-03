namespace AvansDevOps.Domain.Models.Pipeline.Steps;

public class TestAction : PipelineComponent
{
    public TestAction() : base("Unit Testing") { }

    public override bool Execute()
    {
        Console.WriteLine("-> xUnit testen uitvoeren...");
        return true; 
    }
}