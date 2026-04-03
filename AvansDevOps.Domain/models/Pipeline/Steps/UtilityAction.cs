namespace AvansDevOps.Domain.Models.Pipeline.Steps;

public class UtilityAction : PipelineComponent
{
    public UtilityAction() : base("Utility") { }

    public override bool Execute()
    {
        Console.WriteLine("-> Utility actie uitvoeren...");
        return true; 
    }
}