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
    public void BacklogItem_Next_FromTodo_GoesToDoing()
    {
        var item = CreateBacklogItem();
        item.NextState();
        Assert.IsType<BacklogItemDoingState>(item.State);
    }

    [Fact]
    public void BacklogItem_Next_FromDoing_GoesToReadyForTesting()
    {
        var item = CreateBacklogItem();
        item.State = new BacklogItemDoingState();
        item.NextState();
        Assert.IsType<BacklogItemReadyForTestingState>(item.State);
    }

    [Fact]
    public void BacklogItem_Next_FromReadyForTesting_GoesToTesting()
    {
        var item = CreateBacklogItem();
        item.State = new BacklogItemReadyForTestingState();
        item.NextState();
        Assert.IsType<BacklogItemTestingState>(item.State);
    }

    [Fact]
    public void BacklogItem_Next_FromTesting_GoesToTested()
    {
        var item = CreateBacklogItem();
        item.State = new BacklogItemTestingState();
        item.NextState();
        Assert.IsType<BacklogItemTestedState>(item.State);
    }

    [Fact]
    public void BacklogItem_Next_FromTested_WithAllActivitiesDone_GoesToDone()
    {
        var item = CreateBacklogItem();
        item.Activities = new List<Activity>
        {
            new Activity { Title = "Act 1", State = new ActivityDoneState() },
            new Activity { Title = "Act 2", State = new ActivityDoneState() }
        };
        item.State = new BacklogItemTestedState();
        item.NextState();
        Assert.IsType<BacklogItemDoneState>(item.State);
    }

    // FR-05: niet done als activiteiten nog open zijn
    [Fact]
    public void BacklogItem_Next_FromTested_WithUnfinishedActivities_ThrowsException()
    {
        var item = CreateBacklogItem();
        item.Activities = new List<Activity>
        {
            new Activity { Title = "Act 1", State = new ActivityDoneState() },
            new Activity { Title = "Act 2", State = new ActivityDoingState() }
        };
        item.State = new BacklogItemTestedState();
        Assert.Throws<InvalidOperationException>(() => item.NextState());
    }

    // FR-05: geen activiteiten = mag wel done worden
    [Fact]
    public void BacklogItem_Next_FromTested_WithNoActivities_GoesToDone()
    {
        var item = CreateBacklogItem();
        item.State = new BacklogItemTestedState();
        item.NextState();
        Assert.IsType<BacklogItemDoneState>(item.State);
    }

    // FR-12: tester vindt bug → terug naar todo
    [Fact]
    public void BacklogItem_Previous_FromTesting_GoesToTodo()
    {
        var item = CreateBacklogItem();
        item.State = new BacklogItemTestingState();
        item.PreviousState();
        Assert.IsType<BacklogItemTodoState>(item.State);
    }

    // FR-13: lead developer keurt af → terug naar ReadyForTesting
    [Fact]
    public void BacklogItem_Previous_FromTested_GoesToReadyForTesting()
    {
        var item = CreateBacklogItem();
        item.State = new BacklogItemTestedState();
        item.PreviousState();
        Assert.IsType<BacklogItemReadyForTestingState>(item.State);
    }

    [Fact]
    public void BacklogItem_Previous_FromTodo_ThrowsException()
    {
        var item = CreateBacklogItem();
        Assert.Throws<InvalidOperationException>(() => item.PreviousState());
    }

    [Fact]
    public void BacklogItem_Next_FromDone_ThrowsException()
    {
        var item = CreateBacklogItem();
        item.State = new BacklogItemDoneState();
        Assert.Throws<InvalidOperationException>(() => item.NextState());
    }

    [Fact]
    public void BacklogItem_Previous_FromDone_ThrowsException()
    {
        var item = CreateBacklogItem();
        item.State = new BacklogItemDoneState();
        Assert.Throws<InvalidOperationException>(() => item.PreviousState());
    }
}
