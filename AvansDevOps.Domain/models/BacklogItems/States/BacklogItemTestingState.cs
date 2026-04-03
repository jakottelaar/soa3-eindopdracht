namespace AvansDevOps.Domain.Models.BacklogItems.States;

public class BacklogItemTestingState : IBacklogItemState
{
    public void Start(BacklogItem item)
    {
        throw new InvalidOperationException("Item is already in testing.");
    }

    public void MarkReadyForTesting(BacklogItem item)
    {
        throw new InvalidOperationException("Item is already in testing state.");
    }

    public void StartTesting(BacklogItem item)
    {
        throw new InvalidOperationException("Item is already in testing state.");
    }

    public void Approve(BacklogItem item)
    {
        Console.WriteLine($"  ✓ BacklogItem '{item.Title}' testing approved. Moving to Tested state.");
        item.SetState(new BacklogItemTestedState());
    }

    public void Reject(BacklogItem item)
    {
        Console.WriteLine($"  ✗ BacklogItem '{item.Title}' testing rejected. Moving back to Todo state.");
        item.SetState(new BacklogItemTodoState());
    }
}