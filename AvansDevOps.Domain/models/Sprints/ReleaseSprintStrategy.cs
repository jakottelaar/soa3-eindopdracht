using System.Linq;
using AvansDevOps.Domain.Models.Sprints.Reports;

namespace AvansDevOps.Domain.Models.Sprints;

public class ReleaseSprintStrategy : ISprintStrategy
{
    public void Execute(Sprint sprint)
    {
        Console.WriteLine($"Sprint '{sprint.Name}' finished as Release Sprint.");
        Console.WriteLine("Validating release readiness...");

        // Check if all backlog items are done
        var allDone = sprint.BacklogItems.All(b => b.GetState().GetType().Name == "BacklogItemDoneState");
        var completedItems = sprint.BacklogItems.Count(b => b.GetState().GetType().Name == "BacklogItemDoneState");
        var totalItems = sprint.BacklogItems.Count;

        Console.WriteLine($"Completed Backlog Items: {completedItems}/{totalItems}");

        if (allDone)
        {
            Console.WriteLine("✓ All backlog items are completed. Ready for release.");
            Console.WriteLine("The Scrum Master can initiate the release pipeline when ready.");
        }
        else
        {
            Console.WriteLine("✗ Not all backlog items are completed. Release is blocked.");
            Console.WriteLine("Please complete all items before initiating release.");
        }
    }
}