using AvansDevOps.Domain.Models.Users;
using AvansDevOps.Domain.Models.Sprint.States;
using AvansDevOps.Domain.Models.BacklogItems;

namespace AvansDevOps.Domain.Models.Sprint;

public class Sprint
{
    public string Name { get; set; } = string.Empty;
    public  DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public ISprintState State { get; set; } = new SprintCreatedState();
    public List<BacklogItem> BacklogItems { get; set; } = new();
    public  IUser? ScrumMaster { get; set; }

    public void PreviousState() =>
    State.Previous(this);

    public void StartSprint() =>
    State.Next(this);

    public void FinishSprint() =>
    State.Next(this);

    
}