namespace AvansDevOps.Domain.Models.Sprints;

public class ReviewSprintStrategy : ISprintStrategy
{
    public void Execute(Sprint sprint)
    {
        Console.WriteLine($"Sprint '{sprint.Name}' finished as Review Sprint.");
        Console.WriteLine("The Scrum Master can now add a report and summary if needed.");

        // Check backlog items status
        var completedItems = sprint.BacklogItems.Count(b => b.GetState().GetType().Name == "BacklogItemDoneState");
        var totalItems = sprint.BacklogItems.Count;
        Console.WriteLine($"Completed Backlog Items: {completedItems}/{totalItems}");
    }
}
