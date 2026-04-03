using AvansDevOps.Domain.Models.Sprints;
using AvansDevOps.Domain.Models.Users;
using AvansDevOps.Domain.Models.BacklogItems;
using AvansDevOps.Domain.Models.Activities;

namespace AvansDevOps.Domain;

public class Program
{
    public static void Main()
    {
        Console.WriteLine("=== SPRINT & BACKLOG ITEM MANAGEMENT DEMO ===\n");

        // Create team members
        var scrumMaster = new ScrumMaster(
            name: "John Doe",
            email: "john.doe@company.com"
        );

        var developer1 = new Developer
        {
            Name = "Alice Smith",
            Email = "alice.smith@company.com"
        };

        var developer2 = new Developer
        {
            Name = "Bob Johnson",
            Email = "bob.johnson@company.com"
        };

        var tester = new Tester
        {
            Name = "Carol Williams",
            Email = "carol.williams@company.com"
        };

        // Create a new sprint
        var sprint = new Sprint(new ReviewSprintStrategy(), scrumMaster)
        {
            Name = "Sprint 1 - User Authentication",
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(14)
        };

        Console.WriteLine("=== SPRINT CREATION ===");
        Console.WriteLine($"Sprint: {sprint.Name}");
        sprint.DisplayStatus();
        Console.WriteLine();

        // Start sprint
        Console.WriteLine("=== STARTING SPRINT ===");
        sprint.GetState().Start(sprint);
        sprint.DisplayStatus();
        Console.WriteLine();

        // Create backlog items
        Console.WriteLine("=== CREATING BACKLOG ITEMS ===");
        var backlogItem1 = new BacklogItem
        {
            Title = "Implement Login Page",
            Description = "Create login form with email/password validation",
            StoryPoints = 8,
            AssignedUser = developer1
        };

        var backlogItem2 = new BacklogItem
        {
            Title = "User Authentication Service",
            Description = "Develop JWT-based authentication service",
            StoryPoints = 13,
            AssignedUser = developer2
        };

        var backlogItem3 = new BacklogItem
        {
            Title = "Password Reset Functionality",
            Description = "Implement password reset with email verification",
            StoryPoints = 5,
            AssignedUser = developer1
        };

        // Add activities to backlog item 2 (too large for one developer)
        Console.WriteLine("Adding activities to 'User Authentication Service' (too large for one developer):");
        var activity1 = new Activity
        {
            Title = "Implement JWT Token Generation",
            AssignedUser = developer2
        };
        backlogItem2.AddActivity(activity1);

        var activity2 = new Activity
        {
            Title = "Create User Repository Layer",
            AssignedUser = developer1
        };
        backlogItem2.AddActivity(activity2);

        var activity3 = new Activity
        {
            Title = "Add Password Hashing Service",
            AssignedUser = developer2
        };
        backlogItem2.AddActivity(activity3);

        Console.WriteLine();
        Console.WriteLine("Created 3 backlog items");
        backlogItem1.DisplayStatus();
        backlogItem2.DisplayStatus();
        backlogItem3.DisplayStatus();
        Console.WriteLine();

        // Add to sprint
        sprint.BacklogItems.Add(backlogItem1);
        sprint.BacklogItems.Add(backlogItem2);
        sprint.BacklogItems.Add(backlogItem3);
        Console.WriteLine("Added all backlog items to sprint\n");

        // Work through backlog item states
        Console.WriteLine("=== BACKLOG ITEM 1: Implement Login Page ===");
        Console.WriteLine("Starting work on item...");
        backlogItem1.Start();
        backlogItem1.DisplayStatus();
        Console.WriteLine();

        Console.WriteLine("Marking ready for testing...");
        backlogItem1.MarkReadyForTesting();
        backlogItem1.DisplayStatus();
        Console.WriteLine();

        Console.WriteLine("Starting testing...");
        backlogItem1.StartTesting();
        backlogItem1.DisplayStatus();
        Console.WriteLine();

        Console.WriteLine("Approving tests...");
        backlogItem1.Approve();
        backlogItem1.DisplayStatus();
        Console.WriteLine();

        // Work through item 2 with activities
        Console.WriteLine("=== BACKLOG ITEM 2: User Authentication Service (with activities) ===");
        Console.WriteLine("This item is too large for one developer, so it has multiple activities:");
        backlogItem2.DisplayStatus();
        Console.WriteLine();

        Console.WriteLine("Starting work on the backlog item...");
        backlogItem2.Start();
        backlogItem2.DisplayStatus();
        Console.WriteLine();

        // Work on activities
        Console.WriteLine("Working on Activity 1: JWT Token Generation");
        activity1.Start();
        activity1.DisplayStatus();
        Console.WriteLine();

        Console.WriteLine("Completing Activity 1...");
        activity1.Complete();
        activity1.DisplayStatus();
        Console.WriteLine();

        Console.WriteLine("Working on Activity 2: User Repository Layer");
        activity2.Start();
        activity2.DisplayStatus();
        Console.WriteLine();

        Console.WriteLine("Completing Activity 2...");
        activity2.Complete();
        activity2.DisplayStatus();
        Console.WriteLine();

        Console.WriteLine("Working on Activity 3: Password Hashing Service");
        activity3.Start();
        activity3.DisplayStatus();
        Console.WriteLine();

        Console.WriteLine("Completing Activity 3...");
        activity3.Complete();
        activity3.DisplayStatus();
        Console.WriteLine();

        Console.WriteLine("All activities completed! Now marking backlog item ready for testing...");
        backlogItem2.MarkReadyForTesting();
        backlogItem2.DisplayStatus();
        Console.WriteLine();

        Console.WriteLine("Starting testing...");
        backlogItem2.StartTesting();
        backlogItem2.DisplayStatus();
        Console.WriteLine();

        Console.WriteLine("Approving tests...");
        backlogItem2.Approve();
        backlogItem2.DisplayStatus();
        Console.WriteLine();

        // Work through item 3 quickly
        Console.WriteLine("=== BACKLOG ITEM 3: Password Reset Functionality ===");
        Console.WriteLine("Starting work on item...");
        backlogItem3.Start();
        backlogItem3.DisplayStatus();
        Console.WriteLine();

        Console.WriteLine("Marking ready for testing...");
        backlogItem3.MarkReadyForTesting();
        backlogItem3.DisplayStatus();
        Console.WriteLine();

        Console.WriteLine("Starting testing...");
        backlogItem3.StartTesting();
        backlogItem3.DisplayStatus();
        Console.WriteLine();

        Console.WriteLine("Approving tests...");
        backlogItem3.Approve();
        backlogItem3.DisplayStatus();
        Console.WriteLine();

        // Sprint completion
        Console.WriteLine("=== SPRINT COMPLETION ===");
        Console.WriteLine("All backlog items completed! Sprint status:");
        Console.WriteLine($"  Total items: {sprint.BacklogItems.Count}");
        Console.WriteLine("  Items by state:");
        foreach (var item in sprint.BacklogItems)
        {
            item.DisplayStatus();
        }
        Console.WriteLine();

        Console.WriteLine("Finishing sprint...");
        sprint.GetState().Finish(sprint);
        sprint.DisplayStatus();
        Console.WriteLine();

        Console.WriteLine("Starting release...");
        sprint.GetState().StartRelease(sprint);
        sprint.DisplayStatus();
        Console.WriteLine();

        Console.WriteLine("Release succeeded!");
        sprint.GetState().ReleaseSucceeded(sprint);
        sprint.DisplayStatus();
        Console.WriteLine();

        Console.WriteLine("=== DEMO COMPLETE ===");
    }
}
