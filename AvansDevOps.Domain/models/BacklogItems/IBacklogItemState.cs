namespace AvansDevOps.Domain.Models.BacklogItems;

public interface IBacklogItemState
{
    void Start(BacklogItem item);
    void MarkReadyForTesting(BacklogItem item);
    void StartTesting(BacklogItem item);
    void Approve(BacklogItem item);
    void Reject(BacklogItem item);
}