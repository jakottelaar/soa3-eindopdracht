using AvansDevOps.Domain.Models.Users;
using AvansDevOps.Domain.Models.BacklogItems;
using AvansDevOps.Domain.Models.Sprints.States;
using AvansDevOps.Domain.Models.Sprints.Reports;
using AvansDevOps.Domain.Models.Notifications;

namespace AvansDevOps.Domain.Models.Sprints;

public class Sprint : IObservable
{
    public string Name { get; set; } = string.Empty;
    public  DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public ISprintState State { get; set; } = new SprintCreatedState();
    public ISprintStrategy SprintStrategy { get; set; }

    public List<BacklogItem> BacklogItems { get; set; } = new();
    public IUser ScrumMaster { get; set; }
    public List<IUser> Developers { get; set; } = new();
    public Report? Report { get; set; }
    
    private readonly List<IObserver> observers = [];

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

    public void SetReport(Report report)
    {
        Report = report;
    }

    /// <summary>
    /// Allows the Scrum Master to add a custom report to the sprint after it has finished.
    /// Can be used for both review sprints and release sprints. Reports are optional.
    /// </summary>
    public void AddReport(Report report)
    {
        if (GetCurrentStateName() != "SprintFinishedState" && GetCurrentStateName() != "SprintReleasedState")
        {
            throw new InvalidOperationException("Reports can only be added after the sprint has finished.");
        }
        SetReport(report);
        Console.WriteLine($"Report added to sprint '{Name}' by Scrum Master.");
    }

    /// <summary>
    /// Allows the Scrum Master to add a summary to a review sprint after it has finished.
    /// This method should be called on a finished review sprint.
    /// </summary>
    public void AddReviewSummary(string summary)
    {
        if (!(SprintStrategy is ReviewSprintStrategy))
        {
            throw new InvalidOperationException("Review summaries can only be added to review sprints.");
        }

        if (GetCurrentStateName() != "SprintFinishedState")
        {
            throw new InvalidOperationException("Summary can only be added after the sprint has finished.");
        }

        if (Report == null)
        {
            // Create a basic report if one hasn't been added yet
            var report = new ReportBuilder()
                .AddHeader($"Review Report for {Name}")
                .AddFooter("Generated on " + DateTime.Now.ToString("yyyy-MM-dd"))
                .AddTeamComposition($"Scrum Master: {ScrumMaster.Name}\nDevelopers: {string.Join(", ", Developers.Select(d => d.Name))}")
                .AddBurndownChart("Burndown chart: [Simulated chart data]")
                .AddEffortPerDeveloper("Effort per developer: [Simulated effort data]")
                .SetFormat("PDF")
                .Build();

            SetReport(report);
        }

        Report!.SetSummary(summary);
        Console.WriteLine($"Review summary added to sprint '{Name}' by Scrum Master: \"{summary}\"");
    }

    public void NotifyObservers(string message)
    {
        foreach (var observer in observers)
        {
            observer.Update(message);
        }
    }

    public void Subscribe(IObserver observer)
    {
        if (!observers.Contains(observer))
        {
            observers.Add(observer);
        }
    }

    public void Unsubscribe(IObserver observer)
    {
        observers.Remove(observer);
    }
}