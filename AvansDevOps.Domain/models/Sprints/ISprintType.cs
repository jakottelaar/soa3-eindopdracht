namespace AvansDevOps.Domain.Models.Sprints;

public interface ISprintStrategy
{
    void Execute(Sprint sprint);
}