namespace AvansDevOps.Domain.Models.Sprints;

public interface ISprintState
{
    void Next(Sprint context);
    void Previous(Sprint context);
}