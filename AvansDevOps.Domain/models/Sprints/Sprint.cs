using AvansDevOps.Domain.Models.Users;
using AvansDevOps.Domain.Models.BacklogItems;
using AvansDevOps.Domain.Models.Sprints.States;

namespace AvansDevOps.Domain.Models.Sprints;

public class Sprint
{
    public string Name { get; set; } = string.Empty;
    public  DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public ISprintState State { get; set; } = new SprintCreatedState();
    public ISprintStrategy SprintStrategy { get; set; }

    public List<BacklogItem> BacklogItems { get; set; } = new();
    public IUser ScrumMaster { get; set; }

    public Sprint(ISprintStrategy sprintStrategy, IUser scrumMaster)
    {
        SprintStrategy = sprintStrategy;
        ScrumMaster = scrumMaster;
    }
    
    public void SetState(ISprintState state)
    {
        State = state;
    }
    
    public ISprintState GetState()
    {
        return State;
    }

    public string GetCurrentStateName()
    {
        return State.GetType().Name;
    }

    public void DisplayStatus()
    {
        Console.WriteLine($"[Sprint: {Name} | State: {GetCurrentStateName()} | Start: {StartDate:yyyy-MM-dd} | End: {EndDate:yyyy-MM-dd}]");
    }
}