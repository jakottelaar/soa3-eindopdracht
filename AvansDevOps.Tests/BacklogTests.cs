using AvansDevOps.Domain.Models.Activities;
using AvansDevOps.Domain.Models.Activities.States;
using AvansDevOps.Domain.Models.BacklogItems.States;
using AvansDevOps.Domain.Models.Notifications;
using AvansDevOps.Domain.Models.Notifications.Channels;
using NSubstitute;

namespace AvansDevOps.Tests;

public class BacklogTests
{
    //TC-01:
    [Fact]
    public void StartsInTodoWhenAddedToSprint()
    {
        var sprint = TestFactory.CreateReleaseSprint();
        var item = TestFactory.CreateBacklogItem("Item 01");

        sprint.BacklogItems.Add(item);

        Assert.IsType<BacklogItemTodoState>(item.State);
    }

    //TC-02:
    [Fact]
    public void CanOnlyMoveFromTodoToDoing()
    {
        var item = TestFactory.CreateBacklogItem("Item 02");

        item.Start();

        Assert.IsType<BacklogItemDoingState>(item.State);
        Assert.Throws<InvalidOperationException>(item.Start);
    }

    //TC-03:
    [Fact]
    public void FollowsCompleteWorkflowToDone()
    {
        var item = TestFactory.CreateBacklogItem("Item 03");

        item.Start();
        item.MarkReadyForTesting();
        item.StartTesting();
        item.Approve();
        item.Approve();

        Assert.IsType<BacklogItemDoneState>(item.State);
    }

    //TC-04:
    [Fact]
    public void NotifiesTestersWhenReadyForTesting()
    {
        var channel = Substitute.For<INotificationChannel>();
        var observer = new NotificationObserver([channel]);
        var item = TestFactory.CreateBacklogItem("Item 04");
        item.State = new BacklogItemDoingState();
        item.Subscribe(observer);

        item.MarkReadyForTesting();

        channel.Received(1).Send(Arg.Is<string>(m => m.Contains("Backlog Item Ready for Testing") && m.Contains("Item 04")));
    }

    //TC-05:
    [Fact]
    public void MovesFromTestingBackToTodoOnRejection()
    {
        var item = TestFactory.CreateBacklogItem("Item 05");
        item.State = new BacklogItemTestingState();

        item.Reject();

        Assert.IsType<BacklogItemTodoState>(item.State);
    }

    //TC-06:
    [Fact]
    public void MovesFromTestedBackToReadyForTestingOnRejection()
    {
        var item = TestFactory.CreateBacklogItem("Item 06");
        item.State = new BacklogItemTestedState();

        item.Reject();

        Assert.IsType<BacklogItemReadyForTestingState>(item.State);
    }

    //TC-07:
    [Fact]
    public void MovesFromTestedToTodoAndNotifiesScrumMaster()
    {
        var channel = Substitute.For<INotificationChannel>();
        var observer = new NotificationObserver([channel]);
        var item = TestFactory.CreateBacklogItem("Item 07");
        item.State = new BacklogItemTestedState();
        item.Subscribe(observer);

        item.ResetToTodoAndNotifyScrumMaster();

        Assert.IsType<BacklogItemTodoState>(item.State);
        channel.Received(1).Send(Arg.Is<string>(m => m.Contains("Scrum Master") && m.Contains("Item 07")));
    }

    //TC-08:
    [Fact]
    public void CanMoveToDoneWhenAllActivitiesAreDone()
    {
        var item = TestFactory.CreateBacklogItem("Item 08");
        item.Activities =
        [
            new Activity { Title = "Act 1", State = new ActivityDoneState() },
            new Activity { Title = "Act 2", State = new ActivityDoneState() }
        ];
        item.State = new BacklogItemTestedState();

        item.Approve();

        Assert.IsType<BacklogItemDoneState>(item.State);
    }

    //TC-09:
    [Fact]
    public void ForbidsDirectTransitionFromTodoToTesting()
    {
        var item = TestFactory.CreateBacklogItem("Item 11");

        Assert.Throws<InvalidOperationException>(item.StartTesting);
        Assert.IsType<BacklogItemTodoState>(item.State);
    }

    //TC-10:
    [Fact]
    public void ForbidsTransitionFromDoingToTodoOutsideFlow()
    {
        var item = TestFactory.CreateBacklogItem("Item 12");
        item.State = new BacklogItemDoingState();

        Assert.Throws<InvalidOperationException>(item.Reject);
        Assert.IsType<BacklogItemDoingState>(item.State);
    }

    //TC-11:
    [Fact]
    public void ForbidsDirectTransitionToDoneWithoutTested()
    {
        var item = TestFactory.CreateBacklogItem("Item 13");
        item.Start();

        Assert.Throws<InvalidOperationException>(item.Approve);
        Assert.IsNotType<BacklogItemDoneState>(item.State);
    }
}