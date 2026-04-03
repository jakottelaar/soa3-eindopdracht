using AvansDevOps.Domain.Models.Sprints;
using AvansDevOps.Domain.Models.Users;
using AvansDevOps.Domain.Models.BacklogItems;
using AvansDevOps.Domain.Models.Activities;
using AvansDevOps.Domain.Models.Discussion;
using AvansDevOps.Domain.Models.Discussion.Composite;
using AvansDevOps.Domain.Models.Discussion.Visitor;
using AvansDevOps.Domain.Models.Sprints.Reports;
using AvansDevOps.Domain.Models.Notifications;
using AvansDevOps.Domain.Models.Notifications.Channels;

namespace AvansDevOps.Domain;

public class Program
{
    public static void Main()
    {
        Console.WriteLine("╔════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║    SPRINT & BACKLOG ITEM MANAGEMENT WITH NOTIFICATIONS    ║");
        Console.WriteLine("╚════════════════════════════════════════════════════════════╝\n");

        // Setup notification channels
        var emailChannel = new EmailChannel();
        var slackChannel = new SlackChannel();
        var notificationChannels = new List<INotificationChannel> { emailChannel, slackChannel };
        var notificationObserver = new NotificationObserver(notificationChannels);

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

        var productOwner = new ProductOwner
        {
            Name = "Emma Brown",
            Email = "emma.brown@company.com"
        };

        // ========== DEMO 1: REVIEW SPRINT WITH NOTIFICATIONS ==========
        Console.WriteLine("=== DEMO 1: REVIEW SPRINT WITH NOTIFICATIONS ===\n");

        // Create a review sprint
        var reviewSprint = new Sprint(new ReviewSprintStrategy(), scrumMaster)
        {
            Name = "Sprint 1 - User Authentication Review",
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(14)
        };

        reviewSprint.Developers.Add(developer1);
        reviewSprint.Developers.Add(developer2);

        Console.WriteLine("=== SPRINT CREATION ===");
        Console.WriteLine($"Sprint Type: Review Sprint");
        Console.WriteLine($"Sprint: {reviewSprint.Name}");
        reviewSprint.DisplayStatus();
        Console.WriteLine();

        // Start sprint
        Console.WriteLine("=== STARTING SPRINT ===");
        reviewSprint.GetState().Start(reviewSprint);
        reviewSprint.DisplayStatus();
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

        // Add activities to backlog item 2
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

        Console.WriteLine("Created 3 backlog items with activities\n");

        // Subscribe to notifications
        Console.WriteLine("=== SETTING UP NOTIFICATION SUBSCRIPTIONS ===");
        backlogItem1.Subscribe(notificationObserver);
        backlogItem2.Subscribe(notificationObserver);
        backlogItem3.Subscribe(notificationObserver);
        reviewSprint.Subscribe(notificationObserver);
        Console.WriteLine("✓ Backlog items and sprint subscribed to notifications\n");

        // Create discussion for backlog item 1
        var discussion = new Discussion(backlogItem1, scrumMaster);
        discussion.Subscribe(notificationObserver);
        Console.WriteLine("✓ Discussion subscribed to notifications\n");

        // Add to sprint
        reviewSprint.BacklogItems.Add(backlogItem1);
        reviewSprint.BacklogItems.Add(backlogItem2);
        reviewSprint.BacklogItems.Add(backlogItem3);
        Console.WriteLine("Added all backlog items to sprint\n");

        // Work through backlog items
        Console.WriteLine("=== COMPLETING BACKLOG ITEMS & OBSERVING NOTIFICATIONS ===");
        backlogItem1.Start();
        Console.WriteLine("Marking item ready for testing...");
        backlogItem1.MarkReadyForTesting();
        backlogItem1.StartTesting();
        backlogItem1.Approve();

        backlogItem2.Start();
        activity1.Start();
        activity1.Complete();
        activity2.Start();
        activity2.Complete();
        activity3.Start();
        activity3.Complete();
        backlogItem2.MarkReadyForTesting();
        backlogItem2.StartTesting();
        backlogItem2.Approve();

        backlogItem3.Start();
        backlogItem3.MarkReadyForTesting();
        backlogItem3.StartTesting();
        backlogItem3.Approve();

        Console.WriteLine("All backlog items completed!\n");

        // Sprint completion for REVIEW sprint
        Console.WriteLine("=== REVIEW SPRINT COMPLETION ===");
        Console.WriteLine($"Total items: {reviewSprint.BacklogItems.Count}");
        Console.WriteLine("All items are Done. Finishing sprint...");
        reviewSprint.GetState().Finish(reviewSprint);
        reviewSprint.DisplayStatus();
        Console.WriteLine();

        // Scrum Master adds summary for review sprint
        Console.WriteLine("=== SCRUM MASTER ADDS REVIEW SUMMARY ===");
        var reviewSummary = "Sprint objectives achieved. All user authentication features implemented and tested. " +
                           "Team performed well with good collaboration. Ready for next sprint.";
        reviewSprint.AddReviewSummary(reviewSummary);
        Console.WriteLine();
        Console.WriteLine("\n=== DEMO 2: RELEASE SPRINT ===\n");

        // Create a release sprint
        var releaseSprint = new Sprint(new ReleaseSprintStrategy(), scrumMaster)
        {
            Name = "Sprint 2 - Payment Processing Release",
            StartDate = DateTime.Now.AddDays(14),
            EndDate = DateTime.Now.AddDays(28)
        };

        releaseSprint.Developers.Add(developer1);
        releaseSprint.Developers.Add(developer2);

        // Subscribe to notifications
        releaseSprint.Subscribe(notificationObserver);

        Console.WriteLine("=== SPRINT CREATION ===");
        Console.WriteLine($"Sprint Type: Release Sprint");
        Console.WriteLine($"Sprint: {releaseSprint.Name}");
        releaseSprint.DisplayStatus();
        Console.WriteLine();

        // Start sprint
        Console.WriteLine("=== STARTING SPRINT ===");
        releaseSprint.GetState().Start(releaseSprint);
        releaseSprint.DisplayStatus();
        Console.WriteLine();

        // Create backlog items for release sprint
        Console.WriteLine("=== CREATING BACKLOG ITEMS ===");
        var releaseItem1 = new BacklogItem
        {
            Title = "Implement Payment Gateway",
            Description = "Integrate Stripe payment processing",
            StoryPoints = 13,
            AssignedUser = developer1
        };

        var releaseItem2 = new BacklogItem
        {
            Title = "Payment Error Handling",
            Description = "Handle various payment failure scenarios",
            StoryPoints = 8,
            AssignedUser = developer2
        };

        // Subscribe release items to notifications
        releaseItem1.Subscribe(notificationObserver);
        releaseItem2.Subscribe(notificationObserver);

        releaseSprint.BacklogItems.Add(releaseItem1);
        releaseSprint.BacklogItems.Add(releaseItem2);
        Console.WriteLine("Added 2 backlog items to release sprint\n");

        // Complete backlog items
        Console.WriteLine("=== COMPLETING BACKLOG ITEMS & OBSERVING NOTIFICATIONS ===");
        releaseItem1.Start();
        Console.WriteLine("Marking item ready for testing...");
        releaseItem1.MarkReadyForTesting();
        releaseItem1.StartTesting();
        releaseItem1.Approve();

        releaseItem2.Start();
        releaseItem2.MarkReadyForTesting();
        releaseItem2.StartTesting();
        releaseItem2.Approve();

        Console.WriteLine("All backlog items completed!\n");

        // Sprint completion for RELEASE sprint
        Console.WriteLine("=== RELEASE SPRINT COMPLETION ===");
        Console.WriteLine("All items ready. Finishing sprint...");
        releaseSprint.GetState().Finish(releaseSprint);
        releaseSprint.DisplayStatus();
        Console.WriteLine();

        // Scrum Master initiates the release pipeline
        Console.WriteLine("=== SCRUM MASTER INITIATES RELEASE PIPELINE ===");
        Console.WriteLine("Starting release process...");
        releaseSprint.GetState().StartRelease(releaseSprint);
        releaseSprint.DisplayStatus();
        Console.WriteLine();

        // Release pipeline execution - Scenario 1: Failure
        Console.WriteLine("=== RELEASE PIPELINE EXECUTION - FIRST ATTEMPT ===");
        Console.WriteLine("Deployment encountered an issue during production testing...");
        releaseSprint.GetState().ReleaseFailed(releaseSprint);
        releaseSprint.DisplayStatus();
        Console.WriteLine();

        // Scrum Master retries the release
        Console.WriteLine("=== SCRUM MASTER RETRIES RELEASE ===");
        Console.WriteLine("Issue fixed. Initiating release again...");
        releaseSprint.GetState().StartRelease(releaseSprint);
        releaseSprint.DisplayStatus();
        Console.WriteLine();

        // Release pipeline execution - Scenario 2: Success
        Console.WriteLine("=== RELEASE PIPELINE EXECUTION - RETRY SUCCESSFUL ===");
        Console.WriteLine("Deployment successful! Release complete.");
        releaseSprint.GetState().ReleaseSucceeded(releaseSprint);
        releaseSprint.DisplayStatus();
        Console.WriteLine();

        // Discussion Demo with Notifications
        Console.WriteLine("=== DISCUSSION NOTIFICATIONS DEMO ===");
        Console.WriteLine("Testing discussion replies trigger notifications...\n");

        // Reply to the initial post
        var reply1 = new DiscussionPost("We should show inline validation messages and disable the submit button until valid.", developer2);
        discussion.AddPost(reply1);

        var reply2 = new DiscussionPost("Good idea! Also, let's add ARIA labels for accessibility.", tester);
        reply1.AddReply(reply2);

        Console.WriteLine("✓ Notifications sent for all discussion replies\n");

        Console.WriteLine("╔════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║                    DEMO COMPLETE                           ║");
        Console.WriteLine("║                                                            ║");
        Console.WriteLine("║  Notifications were triggered for:                         ║");
        Console.WriteLine("║  ✓ Backlog items ready for testing (→ Testers)             ║");
        Console.WriteLine("║  ✓ Testing rejected items (→ Scrum Master)                 ║");
        Console.WriteLine("║  ✓ Items completed (→ Team Members)                        ║");
        Console.WriteLine("║  ✓ Discussion replies (→ Team Members)                     ║");
        Console.WriteLine("║  ✓ Release success/failure (→ Scrum Master & Product Owner)║");
        Console.WriteLine("║  ✓ Release retry success (→ Scrum Master & Product Owner)  ║");
        Console.WriteLine("╚════════════════════════════════════════════════════════════╝");
    }
}
