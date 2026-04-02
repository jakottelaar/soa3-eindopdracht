namespace AvansDevOps.Domain.Models.BacklogItems.States;

public class BacklogItemTestingState : IBacklogItemState
{
    public void Start(BacklogItem item)
    {
        throw new InvalidOperationException();
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
        item.SetState(new BacklogItemTestedState());
    }

    public void Reject(BacklogItem item)
    {
        item.SetState(new BacklogItemTodoState());
    }
}