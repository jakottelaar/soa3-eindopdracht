using AvansDevOps.Domain.Models.Users;
using AvansDevOps.Domain.Models.BacklogItems;
using AvansDevOps.Domain.Models.Sprints.States;

namespace AvansDevOps.Domain.Models.Sprints;

public class Sprint(ISprintType sprintType)
{
    public string Name { get; set; } = string.Empty;
    public  DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public ISprintState State { get; set; } = new SprintCreatedState();
    public ISprintType SprintType { get; set; } = sprintType;

    public List<BacklogItem> BacklogItems { get; set; } = new();
    public  IUser? ScrumMaster { get; set; }

    public void NextState() =>
        State.Next(this);

    public void PreviousState() =>
        State.Previous(this);

    public void StartSprint() =>
        State.Next(this);

    public void FinishSprint()
    {
        SprintType.HandleFinish(this);
    }
    
}