using AvansDevOps.Domain.Models.BacklogItems;
using AvansDevOps.Domain.Models.BacklogItems.States;
using AvansDevOps.Domain.Models.Pipeline;
using AvansDevOps.Domain.Models.Sprints;
using AvansDevOps.Domain.Models.Users;

namespace AvansDevOps.Tests;

internal static class TestFactory
{
    internal static BacklogItem CreateBacklogItem(string title)
    {
        return new BacklogItem { Title = title };
    }

    internal static BacklogItem CreateDoneBacklogItem(string title)
    {
        return new BacklogItem
        {
            Title = title,
            State = new BacklogItemDoneState()
        };
    }

    internal static Sprint CreateReleaseSprint()
    {
        return new Sprint(new ReleaseSprintStrategy(), CreateScrumMaster("release-sm"), new Pipeline("Default Release Pipeline"))
        {
            Name = "Release Sprint"
        };
    }

    internal static Sprint CreateReleaseSprint(Pipeline pipeline)
    {
        return new Sprint(new ReleaseSprintStrategy(), CreateScrumMaster("release-sm"), pipeline)
        {
            Name = "Release Sprint"
        };
    }

    internal static Sprint CreateReviewSprint()
    {
        return new Sprint(new ReviewSprintStrategy(), CreateScrumMaster("review-sm"))
        {
            Name = "Review Sprint"
        };
    }

    internal static ScrumMaster CreateScrumMaster(string name)
    {
        return new ScrumMaster(name, $"{name}@example.com");
    }

    internal static Developer CreateDeveloper(string name)
    {
        return new Developer(
            name: name,
            email: $"{name}@example.com"
        );
    }
}

internal sealed class TrackingStep : PipelineComponent
{
    private readonly IList<string> executionOrder;

    public TrackingStep(string name, IList<string> executionOrder) : base(name)
    {
        this.executionOrder = executionOrder;
    }

    public override bool Execute()
    {
        executionOrder.Add(Name);
        return true;
    }
}

internal sealed class FailingTrackingStep : PipelineComponent
{
    private readonly IList<string> executionOrder;

    public FailingTrackingStep(string name, IList<string> executionOrder) : base(name)
    {
        this.executionOrder = executionOrder;
    }

    public override bool Execute()
    {
        executionOrder.Add(Name);
        return false;
    }
}

internal sealed class CountingStep : PipelineComponent
{
    private readonly IDictionary<string, int> counters;

    public CountingStep(string name, IDictionary<string, int> counters) : base(name)
    {
        this.counters = counters;
    }

    public override bool Execute()
    {
        counters[Name]++;
        return true;
    }
}