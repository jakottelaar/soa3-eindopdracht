namespace AvansDevOps.Domain.Models.BacklogItems.States;

public class BacklogItemTestingState : IBacklogItemState
{
    public void Next(BacklogItem context)
    {
        context.State = new BacklogItemTestedState();
    }

    public void Previous(BacklogItem context)
    {
        context.State = new BacklogItemTodoState();
    }
}