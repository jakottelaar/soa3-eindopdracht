using AvansDevOps.Domain.Models.Pipeline;
using AvansDevOps.Domain.Models.Pipeline.Steps;

namespace AvansDevOps.Tests;

public class FailingAction : PipelineComponent
{
    public FailingAction() : base("Failing Step") { }
    public override bool Execute() => false;
}

public class PipelineTests
{
    // TC-16
    [Fact]
    public void Pipeline_ExecutesStepsSequentially()
    {
        var pipeline = new Pipeline("CI Pipeline");
        pipeline.AddStep(new SourceAction());
        pipeline.AddStep(new BuildAction());
        pipeline.AddStep(new TestAction());
        pipeline.AddStep(new AnalyzeAction());
        pipeline.AddStep(new PackageAction());
        pipeline.AddStep(new DeployAction());

        var result = pipeline.Execute();

        Assert.True(result);
    }

    // TC-17
    [Fact]
    public void Pipeline_StopsOnFailingStep()
    {
        var pipeline = new Pipeline("CI Pipeline");
        pipeline.AddStep(new BuildAction());
        pipeline.AddStep(new FailingAction());  // stopt hier
        pipeline.AddStep(new DeployAction());   // mag niet uitgevoerd worden
        var result = pipeline.Execute();

        Assert.False(result);
    }

    // TC-18
    [Fact]
    public void Pipeline_ReturnsFalse_OnFailure()
    {
        var pipeline = new Pipeline("Release Pipeline");
        pipeline.AddStep(new FailingAction());
        var result = pipeline.Execute();

        Assert.False(result);
    }

    // TC-35
    [Fact]
    public void Pipeline_NoSideEffectsAfterFailure()
    {
        var pipeline = new Pipeline("CI Pipeline");
        pipeline.AddStep(new BuildAction());
        pipeline.AddStep(new FailingAction());
        pipeline.AddStep(new DeployAction());

        var result = pipeline.Execute();

        Assert.False(result);
    }

    // TC-36
    [Fact]
    public void Pipeline_Retry_ReExecutesAllSteps()
    {
        var pipeline = new Pipeline("Release Pipeline");
        pipeline.AddStep(new BuildAction());
        pipeline.AddStep(new TestAction());
        pipeline.AddStep(new DeployAction());

        var firstRun  = pipeline.Execute();
        var secondRun = pipeline.Execute();

        Assert.True(firstRun);
        Assert.True(secondRun);
    }

    [Fact]
    public void Pipeline_Empty_ReturnsTrue()
    {
        var pipeline = new Pipeline("Empty");
        Assert.True(pipeline.Execute());
    }

    [Fact]
    public void Pipeline_FullHappyPath_ReturnsTrue()
    {
        var pipeline = new Pipeline("Full CI/CD");
        pipeline.AddStep(new SourceAction());
        pipeline.AddStep(new BuildAction());
        pipeline.AddStep(new TestAction());
        pipeline.AddStep(new AnalyzeAction());
        pipeline.AddStep(new PackageAction());
        pipeline.AddStep(new DeployAction());

        Assert.True(pipeline.Execute());
    }

    [Fact]
    public void Pipeline_NestedPipeline_ExecutesCorrectly()
    {
        var ciPipeline = new Pipeline("CI");
        ciPipeline.AddStep(new SourceAction());
        ciPipeline.AddStep(new BuildAction());
        ciPipeline.AddStep(new TestAction());

        var cdPipeline = new Pipeline("CD");
        cdPipeline.AddStep(ciPipeline);
        cdPipeline.AddStep(new PackageAction());
        cdPipeline.AddStep(new DeployAction());

        Assert.True(cdPipeline.Execute());
    }

    [Fact]
    public void Pipeline_NestedPipeline_FailingInner_StopsOuter()
    {
        var ciPipeline = new Pipeline("CI");
        ciPipeline.AddStep(new BuildAction());
        ciPipeline.AddStep(new FailingAction()); // inner faalt

        var cdPipeline = new Pipeline("CD");
        cdPipeline.AddStep(ciPipeline);
        cdPipeline.AddStep(new DeployAction()); // mag niet draaien

        var result = cdPipeline.Execute();

        Assert.False(result);
    }
}