namespace AvansDevOps.Domain.Models.BacklogItems.States;

public class BacklogItemTodoState : IBacklogItemState
{
    
    public void Start(BacklogItem item)
    {
        item.SetState(new BacklogItemDoingState());
    }

    public void MarkReadyForTesting(BacklogItem item)
    {
        throw new InvalidOperationException();
    }

    public void StartTesting(BacklogItem item)
    {
        throw new InvalidOperationException();
    }

    public void Approve(BacklogItem item)
    {
        throw new InvalidOperationException();
    }

    public void Reject(BacklogItem item)
    {
        throw new InvalidOperationException();
    }

}