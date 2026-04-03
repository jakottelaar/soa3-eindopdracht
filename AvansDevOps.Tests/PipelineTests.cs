using AvansDevOps.Domain.Models.Pipeline;
using AvansDevOps.Domain.Models.Notifications;
using AvansDevOps.Domain.Models.Notifications.Channels;
using NSubstitute;

namespace AvansDevOps.Tests;

public class PipelineTests
{
    //TC-23:
    [Fact]
    public void ExecutesActionsSequentially()
    {
        var executionOrder = new List<string>();
        var pipeline = new Pipeline("Pipeline 25");
        pipeline.AddStep(new TrackingStep("A", executionOrder));
        pipeline.AddStep(new TrackingStep("B", executionOrder));
        pipeline.AddStep(new TrackingStep("C", executionOrder));

        var result = pipeline.Execute();

        Assert.True(result);
        Assert.Equal(["A", "B", "C"], executionOrder);
    }

    //TC-24:
    [Fact]
    public void StopsAtFailingAction()
    {
        var executionOrder = new List<string>();
        var pipeline = new Pipeline("Pipeline 26");
        pipeline.AddStep(new TrackingStep("A", executionOrder));
        pipeline.AddStep(new FailingTrackingStep("B", executionOrder));
        pipeline.AddStep(new TrackingStep("C", executionOrder));

        var result = pipeline.Execute();

        Assert.False(result);
        Assert.Equal(["A", "B"], executionOrder);
    }

    //TC-25:
    [Fact]
    public void SendsNotificationToScrumMasterOnFailure()
    {
        var channel = Substitute.For<INotificationChannel>();
        var observer = new NotificationObserver([channel]);
        var pipeline = new Pipeline("Pipeline 27");
        pipeline.Subscribe(observer);
        pipeline.AddStep(new FailingTrackingStep("fail", new List<string>()));

        var result = pipeline.Execute();

        Assert.False(result);
        channel.Received(1).Send(Arg.Is<string>(m => m.Contains("failed") && m.Contains("Pipeline 27")));
    }

    //TC-26:
    [Fact]
    public void StopsCompletelyWithoutSideEffects()
    {
        var sideEffects = new List<string>();
        var pipeline = new Pipeline("Pipeline 28");
        pipeline.AddStep(new TrackingStep("PreCheck", sideEffects));
        pipeline.AddStep(new FailingTrackingStep("FailHere", sideEffects));
        pipeline.AddStep(new TrackingStep("ShouldNotRun", sideEffects));

        var result = pipeline.Execute();

        Assert.False(result);
        Assert.DoesNotContain("ShouldNotRun", sideEffects);
    }

    //TC-27:
    [Fact]
    public void ReexecutesCompletePipelineOnRetry()
    {
        var counters = new Dictionary<string, int>
        {
            ["A"] = 0,
            ["B"] = 0,
            ["C"] = 0
        };

        var pipeline = new Pipeline("Pipeline 29");
        pipeline.AddStep(new CountingStep("A", counters));
        pipeline.AddStep(new CountingStep("B", counters));
        pipeline.AddStep(new CountingStep("C", counters));

        var firstResult = pipeline.Execute();
        var secondResult = pipeline.Execute();

        Assert.True(firstResult);
        Assert.True(secondResult);
        Assert.Equal(2, counters["A"]);
        Assert.Equal(2, counters["B"]);
        Assert.Equal(2, counters["C"]);
    }
}