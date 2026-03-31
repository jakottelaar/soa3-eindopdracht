using AvansDevOps.Domain.Models.Sprints;
using AvansDevOps.Domain.Models.Sprints.States;

namespace AvansDevOps.Tests;

public class SprintStateTests
{
    private Sprint CreateSprint() => new Sprint(new ReleaseSprint()) { Name = "Test Sprint" };

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
        sprint.NextState();
        Assert.IsType<SprintActiveState>(sprint.State);
    }

    [Fact]
    public void Sprint_Next_FromActive_GoesToFinished()
    {
        var sprint = CreateSprint();
        sprint.State = new SprintActiveState();
        sprint.NextState();
        Assert.IsType<SprintFinishedState>(sprint.State);
    }
}