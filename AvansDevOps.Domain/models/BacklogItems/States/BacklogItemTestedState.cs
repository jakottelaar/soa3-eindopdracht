namespace AvansDevOps.Domain.Models.BacklogItems.States;

public class BacklogItemTestedState : IBacklogItemState
{
    public void Start(BacklogItem item)
    {
        throw new InvalidOperationException("Item is already tested and complete.");
    }

    public void MarkReadyForTesting(BacklogItem item)
    {
        throw new InvalidOperationException("Item is already tested.");
    }

    public void StartTesting(BacklogItem item)
    {
        throw new InvalidOperationException("Item is already tested.");
    }

    public void Approve(BacklogItem item)
    {
        if (item.AreAllActivitiesDone())
        {
            Console.WriteLine($"  ✓ BacklogItem '{item.Title}' fully approved. Moving to Done state.");
            item.SetState(new BacklogItemDoneState());
            // Notify team members that item is complete and discussion is locked
            item.NotifyObservers(
                $"NOTIFICATION: Backlog Item Completed\n" +
                $"Item: '{item.Title}'\n" +
                "This item has been completed and is moved to Done. Associated discussions are now locked.");
        }
        else
        {
            Console.WriteLine($"  ✗ Cannot approve '{item.Title}' - not all activities are completed.");
            Console.WriteLine("    Please complete all activities before approving the backlog item.");
            throw new InvalidOperationException($"Cannot approve backlog item '{item.Title}' - activities not finished");
        }
    }

    public void Reject(BacklogItem item)
    {
        item.SetState(new BacklogItemReadyForTestingState());
    }
}