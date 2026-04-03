using AvansDevOps.Domain.Models.Sprints.Reports;
using System.Linq;

namespace AvansDevOps.Domain.Models.Sprints;

public class ReviewSprintStrategy : ISprintStrategy
{
    public void Execute(Sprint sprint)
    {
        Console.WriteLine($"Executing review for sprint '{sprint.Name}'.");

        if (sprint.Report == null)
        {
            throw new InvalidOperationException("No report set for review sprint.");
        }

        // The report has been added by Scrum Master, including summary
        // Display the review report
        Console.WriteLine("Review Report:");
        Console.WriteLine($"Header: {sprint.Report.Header}");
        Console.WriteLine($"Team Composition: {sprint.Report.TeamComposition}");
        Console.WriteLine($"Burndown Chart: {sprint.Report.BurndownChart}");
        Console.WriteLine($"Effort Per Developer: {sprint.Report.EffortPerDeveloper}");
        Console.WriteLine($"Summary: {sprint.Report.Summary}");
        Console.WriteLine($"Format: {sprint.Report.Format}");
        Console.WriteLine($"Footer: {sprint.Report.Footer}");

        // Additional review logic: check backlog items status
        var completedItems = sprint.BacklogItems.Count(b => b.GetState().GetType().Name == "BacklogItemDoneState");
        var totalItems = sprint.BacklogItems.Count;
        Console.WriteLine($"Completed Backlog Items: {completedItems}/{totalItems}");

        if (completedItems == totalItems)
        {
            Console.WriteLine("All backlog items completed. Sprint review successful.");
        }
        else
        {
            Console.WriteLine("Some backlog items are not completed. Review issues.");
        }
    }
}
