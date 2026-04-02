using AvansDevOps.Domain.Models.Sprints;
using AvansDevOps.Domain.Models.Sprints.States;
using AvansDevOps.Domain.Models.Users;

namespace AvansDevOps.Tests;

public class SprintStateTests
{
    private Sprint CreateSprint() => new Sprint(new ReleaseSprintStrategy(), new ScrumMaster("John Doe", "john.doe@example.com")) { Name = "Test Sprint" };

    [Fact]
    public void Sprint_StartsInCreatedState()
    {
        var sprint = CreateSprint();
        Assert.IsType<SprintCreatedState>(sprint.State);
    }

    [Fact]
    public void Sprint_Next_FromCreated_GoesToActive()
    {
        var sprint = CreateSprint();
        sprint.SetState(new SprintActiveState());
        Assert.IsType<SprintActiveState>(sprint.State);
    }

    [Fact]
    public void Sprint_Next_FromActive_GoesToFinished()
    {
        var sprint = CreateSprint();
        sprint.State = new SprintActiveState();
        sprint.SetState(new SprintFinishedState());
        Assert.IsType<SprintFinishedState>(sprint.State);
    }
}