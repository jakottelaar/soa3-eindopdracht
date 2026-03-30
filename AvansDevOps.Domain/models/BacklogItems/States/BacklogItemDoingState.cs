namespace AvansDevOps.Domain.Models.BacklogItems.States;

public class BacklogItemDoingState : IBacklogItemState
{
    public void Next(BacklogItem context)
    {
        context.State = new BacklogItemReadyForTestingState();
    }

    public void Previous(BacklogItem context)
    {
        context.State = new BacklogItemTodoState();
    }
}