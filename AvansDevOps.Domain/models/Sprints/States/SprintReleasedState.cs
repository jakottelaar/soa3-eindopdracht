namespace AvansDevOps.Domain.Models.Sprints.States;

public class SprintReleasedState : ISprintState
{
    public void Start(Sprint sprint)
    {
        throw new InvalidOperationException("Cannot start a sprint that is already in released state.");
    }

    public void Finish(Sprint sprint)
    {
        throw new InvalidOperationException("Use ReleaseSucceeded() or ReleaseFailed() to complete a release.");
    }

    public void StartRelease(Sprint sprint)
    {
        throw new InvalidOperationException("Sprint is already in released state.");
    }

    public void ReleaseSucceeded(Sprint sprint)
    {
        Console.WriteLine($"✓ Sprint '{sprint.Name}' release succeeded! Release deployment complete.");
        Console.WriteLine("Notifying Scrum Master and Product Owner of successful release...");
        
        // Notify Scrum Master and Product Owner
        var notificationMessage = $"NOTIFICATION: Release Successful\n" +
                                 $"Sprint: '{sprint.Name}'\n" +
                                 $"Status: Release deployment to production completed successfully.\n" +
                                 $"Time: {DateTime.Now:yyyy-MM-dd HH:mm:ss}\n" +
                                 $"All backlog items have been successfully deployed.";
        
        sprint.NotifyObservers(notificationMessage);
        // Sprint remains in released state as a terminal success state
    }

    public void ReleaseFailed(Sprint sprint)
    {
        Console.WriteLine($"✗ Sprint '{sprint.Name}' release failed. Returning to Finished state.");
        Console.WriteLine("Notifying Scrum Master and Product Owner of failed release...");
        
        // Notify Scrum Master and Product Owner
        var notificationMessage = $"NOTIFICATION: Release Failed\n" +
                                 $"Sprint: '{sprint.Name}'\n" +
                                 $"Status: Release deployment encountered errors and was rolled back.\n" +
                                 $"Time: {DateTime.Now:yyyy-MM-dd HH:mm:ss}\n" +
                                 $"Scrum Master can retry the release when ready.";
        
        sprint.NotifyObservers(notificationMessage);
        Console.WriteLine("The Scrum Master can retry the release when ready.");
        sprint.SetState(new SprintFinishedState());
    }

    public void Cancel(Sprint sprint)
    {
        Console.WriteLine($"Sprint '{sprint.Name}' cancelled during release. Transitioning to Cancelled state.");
        sprint.NotifyObservers($"NOTIFICATION: Release Cancelled\nSprint: '{sprint.Name}'\nThe release was cancelled by the Scrum Master.");
        sprint.SetState(new SprintCancelledState());
    }
}