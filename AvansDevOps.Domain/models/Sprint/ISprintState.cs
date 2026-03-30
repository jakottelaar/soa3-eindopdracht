namespace AvansDevOps.Domain.Models.Sprint;

public interface ISprintState
{
    void Next(Sprint context);
    void Previous(Sprint context);
}