namespace AvansDevOps.Domain.Models.BacklogItems.States;

public class BacklogItemTodoState : IBacklogItemState
{
    public void Next(BacklogItem context)
    {
        context.State = new BacklogItemDoingState();
    }

    public void Previous(BacklogItem context)
    {
        throw new InvalidOperationException("BacklogItem is already in the first state.");
    }
}