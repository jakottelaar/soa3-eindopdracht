using AvansDevOps.Domain.Models.BacklogItems;
using AvansDevOps.Domain.Models.BacklogItems.States;
using AvansDevOps.Domain.Models.Activities;
using AvansDevOps.Domain.Models.Activities.States;

namespace AvansDevOps.Tests;

public class BacklogItemStateTests
{
    private BacklogItem CreateBacklogItem() => new BacklogItem { Title = "Test Item" };

    [Fact]
    public void BacklogItem_StartsInTodoState()
    {
        var item = CreateBacklogItem();
        Assert.IsType<BacklogItemTodoState>(item.State);
    }

    [Fact]
    public void BacklogItem_FromTodo_Start_GoesToDoing()
    {
        var item = CreateBacklogItem();
        item.Start();
        Assert.IsType<BacklogItemDoingState>(item.State);
    }

    [Fact]
    public void BacklogItem_FromDoing_MarkReadyForTesting_GoesToReadyForTesting()
    {
        var item = CreateBacklogItem();
        item.State = new BacklogItemDoingState();

        item.MarkReadyForTesting();

        Assert.IsType<BacklogItemReadyForTestingState>(item.State);
    }

    [Fact]
    public void BacklogItem_FromReadyForTesting_StartTesting_GoesToTesting()
    {
        var item = CreateBacklogItem();
        item.State = new BacklogItemReadyForTestingState();

        item.StartTesting();

        Assert.IsType<BacklogItemTestingState>(item.State);
    }

    [Fact]
    public void BacklogItem_FromTesting_Approve_GoesToTested()
    {
        var item = CreateBacklogItem();
        item.State = new BacklogItemTestingState();

        item.Approve();

        Assert.IsType<BacklogItemTestedState>(item.State);
    }

    [Fact]
    public void BacklogItem_FromTested_WithAllActivitiesDone_Approve_GoesToDone()
    {
        var item = CreateBacklogItem();
        item.Activities = new List<Activity>
        {
            new Activity { Title = "Act 1", State = new ActivityDoneState() },
            new Activity { Title = "Act 2", State = new ActivityDoneState() }
        };
        item.State = new BacklogItemTestedState();

        item.Approve();

        Assert.IsType<BacklogItemDoneState>(item.State);
    }

    [Fact]
    public void BacklogItem_FromTested_WithUnfinishedActivities_Approve_ThrowsException()
    {
        var item = CreateBacklogItem();
        item.Activities = new List<Activity>
        {
            new Activity { Title = "Act 1", State = new ActivityDoneState() },
            new Activity { Title = "Act 2", State = new ActivityDoingState() }
        };
        item.State = new BacklogItemTestedState();

        Assert.Throws<InvalidOperationException>(() => item.Approve());
    }

    [Fact]
    public void BacklogItem_FromTested_WithNoActivities_Approve_GoesToDone()
    {
        var item = CreateBacklogItem();
        item.State = new BacklogItemTestedState();

        item.Approve();

        Assert.IsType<BacklogItemDoneState>(item.State);
    }

    [Fact]
    public void BacklogItem_FromTesting_Reject_GoesToTodo()
    {
        var item = CreateBacklogItem();
        item.State = new BacklogItemTestingState();

        item.Reject();

        Assert.IsType<BacklogItemTodoState>(item.State);
    }

    [Fact]
    public void BacklogItem_FromTested_Reject_GoesToReadyForTesting()
    {
        var item = CreateBacklogItem();
        item.State = new BacklogItemTestedState();

        item.Reject();

        Assert.IsType<BacklogItemReadyForTestingState>(item.State);
    }

    [Fact]
    public void BacklogItem_FromTodo_Reject_ThrowsException()
    {
        var item = CreateBacklogItem();

        Assert.Throws<InvalidOperationException>(() => item.Reject());
    }

    [Fact]
    public void BacklogItem_FromDone_Approve_ThrowsException()
    {
        var item = CreateBacklogItem();
        item.State = new BacklogItemDoneState();

        Assert.Throws<InvalidOperationException>(() => item.Approve());
    }

    [Fact]
    public void BacklogItem_FromDone_Reject_ThrowsException()
    {
        var item = CreateBacklogItem();
        item.State = new BacklogItemDoneState();

        Assert.Throws<InvalidOperationException>(() => item.Reject());
    }
}