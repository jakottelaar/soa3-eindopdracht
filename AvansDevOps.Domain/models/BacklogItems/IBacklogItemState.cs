namespace AvansDevOps.Domain.Models.BacklogItems;

public interface IBacklogItemState
{
    void Next(BacklogItem context);
    void Previous(BacklogItem context);
}