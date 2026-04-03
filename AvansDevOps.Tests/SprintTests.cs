using AvansDevOps.Domain.Models.Notifications;
using AvansDevOps.Domain.Models.Notifications.Channels;
using AvansDevOps.Domain.Models.Pipeline;
using AvansDevOps.Domain.Models.Sprints;
using AvansDevOps.Domain.Models.Sprints.Reports;
using AvansDevOps.Domain.Models.Sprints.States;
using NSubstitute;

namespace AvansDevOps.Tests;

public class SprintTests
{
    //TC-12:
    [Fact]
    public void AllowsEditingPropertiesOnlyInCreatedPhase()
    {
        var sprint = TestFactory.CreateReleaseSprint();

        sprint.Name = "Planned Sprint";
        sprint.StartDate = new DateTime(2026, 1, 1);
        sprint.EndDate = new DateTime(2026, 1, 14);

        sprint.GetState().Start(sprint);

        Assert.Throws<InvalidOperationException>(() => sprint.Name = "Changed while active");
    }

    //TC-13:
    [Fact]
    public void AllowsAddingBacklogItemsOnlyInCreatedPhase()
    {
        var sprint = TestFactory.CreateReleaseSprint();

        sprint.BacklogItems.Add(TestFactory.CreateBacklogItem("Item 15-A"));

        sprint.GetState().Start(sprint);

        Assert.Throws<InvalidOperationException>(() => sprint.BacklogItems.Add(TestFactory.CreateBacklogItem("Item 15-B")));
    }

    //TC-14:
    [Fact]
    public void DoesNotAllowSprintChangesInActivePhase()
    {
        var sprint = TestFactory.CreateReleaseSprint();
        sprint.StartDate = DateTime.Today;
        sprint.EndDate = DateTime.Today.AddDays(14);

        sprint.GetState().Start(sprint);

        Assert.Throws<InvalidOperationException>(() => sprint.StartDate = DateTime.Today.AddDays(1));
        Assert.Throws<InvalidOperationException>(() => sprint.EndDate = DateTime.Today.AddDays(20));
    }

    //TC-15:
    [Fact]
    public void PreventsAddingBacklogItemsDuringActivePhase()
    {
        var sprint = TestFactory.CreateReleaseSprint();
        sprint.GetState().Start(sprint);

        Assert.Throws<InvalidOperationException>(() => sprint.BacklogItems.Add(TestFactory.CreateBacklogItem("Item 17")));
    }

    //TC-16:
    [Fact]
    public void TransitionsToFinishedAfterEndDate()
    {
        var sprint = TestFactory.CreateReviewSprint();
        sprint.StartDate = new DateTime(2026, 1, 1);
        sprint.EndDate = new DateTime(2026, 1, 10);
        sprint.SetReport(new ReportBuilder().AddSummary("Review complete").Build());

        sprint.GetState().Start(sprint);
        sprint.UpdateStateByDate(new DateTime(2026, 1, 11));

        Assert.IsType<SprintFinishedState>(sprint.State);
    }

    //TC-17:
    [Fact]
    public void RequiresReviewDocumentToCloseReviewSprint()
    {
        var sprint = TestFactory.CreateReviewSprint();
        sprint.GetState().Start(sprint);

        Assert.Throws<InvalidOperationException>(() => sprint.GetState().Finish(sprint));

        sprint.SetReport(new ReportBuilder().AddSummary("Retrospective and review done").Build());
        sprint.GetState().Finish(sprint);

        Assert.IsType<SprintFinishedState>(sprint.State);
    }

    //TC-18:
    [Fact]
    public void StartsPipelineAutomaticallyAfterFinished()
    {
        var counters = new Dictionary<string, int> { ["build"] = 0 };
        var pipeline = new Pipeline("Auto Pipeline");
        pipeline.AddStep(new CountingStep("build", counters));

        var sprint = TestFactory.CreateReleaseSprint(pipeline);
        sprint.BacklogItems.Add(TestFactory.CreateDoneBacklogItem("Done item"));

        sprint.GetState().Start(sprint);
        sprint.GetState().Finish(sprint);

        Assert.Equal(1, counters["build"]);
    }

    //TC-19:
    [Fact]
    public void MarksSprintAsReleasedAndSendsNotificationsAfterSuccessfulPipeline()
    {
        var pipeline = new Pipeline("Successful Pipeline");
        pipeline.AddStep(new TrackingStep("ok", new List<string>()));

        var channel = Substitute.For<INotificationChannel>();
        var observer = new NotificationObserver([channel]);

        var sprint = TestFactory.CreateReleaseSprint(pipeline);
        sprint.Subscribe(observer);
        sprint.BacklogItems.Add(TestFactory.CreateDoneBacklogItem("Done item"));

        sprint.GetState().Start(sprint);
        sprint.GetState().Finish(sprint);

        Assert.IsType<SprintReleasedState>(sprint.State);
        channel.Received(1).Send(Arg.Is<string>(m => m.Contains("Release Successful")));
    }

    //TC-20:
    [Fact]
    public void AllowsRetryOrCancelAfterFailedPipeline()
    {
        var pipeline = new Pipeline("Flaky Pipeline");
        pipeline.AddStep(new FailingTrackingStep("fail", new List<string>()));

        var sprint = TestFactory.CreateReleaseSprint(pipeline);
        sprint.BacklogItems.Add(TestFactory.CreateDoneBacklogItem("Done item"));

        sprint.GetState().Start(sprint);
        sprint.GetState().Finish(sprint);

        Assert.IsType<SprintFinishedState>(sprint.State);

        sprint.GetState().StartRelease(sprint);
        Assert.IsType<SprintReleasedState>(sprint.State);

        sprint.GetState().Cancel(sprint);
        Assert.IsType<SprintCancelledState>(sprint.State);
    }

    //TC-21:
    [Fact]
    public void SendsNotificationsToPoAndScrumMasterOnCancellation()
    {
        var channelOne = Substitute.For<INotificationChannel>();
        var channelTwo = Substitute.For<INotificationChannel>();

        var sprint = TestFactory.CreateReleaseSprint();
        sprint.Subscribe(new NotificationObserver([channelOne]));
        sprint.Subscribe(new NotificationObserver([channelTwo]));
        sprint.State = new SprintReleasedState();

        sprint.GetState().Cancel(sprint);

        channelOne.Received(1).Send(Arg.Is<string>(m => m.Contains("Release Cancelled")));
        channelTwo.Received(1).Send(Arg.Is<string>(m => m.Contains("Release Cancelled")));
    }

    //TC-22:
    [Fact]
    public void PreventsReleaseSprintWithoutLinkedPipeline()
    {
        var scrumMaster = TestFactory.CreateScrumMaster("sm24");

        Assert.Throws<InvalidOperationException>(() => new Sprint(new ReleaseSprintStrategy(), scrumMaster));
    }
}
