using System.Linq;
using AvansDevOps.Domain.Models.Sprints.Reports;

namespace AvansDevOps.Domain.Models.Sprints;

public class ReleaseSprintStrategy : ISprintStrategy
{
    public void Execute(Sprint sprint)
    {
        Console.WriteLine($"Executing release for sprint '{sprint.Name}'.");

        // Build release report
        var report = new ReportBuilder()
            .AddHeader($"Release Report for {sprint.Name}")
            .AddFooter("Generated on " + DateTime.Now.ToString("yyyy-MM-dd"))
            .AddTeamComposition($"Scrum Master: {sprint.ScrumMaster.Name}\nDevelopers: {string.Join(", ", sprint.Developers.Select(d => d.Name))}")
            .AddBurndownChart("Burndown chart: [Simulated chart data]")
            .AddEffortPerDeveloper("Effort per developer: [Simulated effort data]")
            .SetFormat("PDF")
            .AddSummary("Release completed successfully.")
            .Build();

        sprint.SetReport(report);

        // Display report
        Console.WriteLine("Release Report:");
        Console.WriteLine($"Header: {report.Header}");
        Console.WriteLine($"Summary: {report.Summary}");

        // Check if all backlog items are done
        var allDone = sprint.BacklogItems.All(b => b.GetState().GetType().Name == "BacklogItemDoneState");

        if (allDone)
        {
            Console.WriteLine("All backlog items are completed. Starting release process.");
            // Since sprint is finished, call StartRelease
            sprint.GetState().StartRelease(sprint);
        }
        else
        {
            Console.WriteLine("Not all backlog items are completed. Cannot release. Sprint remains finished.");
            // Optionally, could set to a failed state, but for now, just log
        }
    }
}