namespace AvansDevOps.Domain.Models.Sprints;

public interface ISprintType
{
    void HandleFinish(Sprint sprint);
}