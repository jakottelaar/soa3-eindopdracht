namespace AvansDevOps.Domain.Models.SCM;

public class GitLibrary
{
    public void GitCommit(string message, string repo, string branch, string author)
    {
        Console.WriteLine($"Committing to {repo} on branch {branch} with message '{message}' by author {author}");
    }

    public void GitCreateBranch(string branchName, string repo)
    {
        Console.WriteLine($"Creating branch {branchName} in repository {repo}");
    }

    public void GitCheckout(string repo)
    {
        Console.WriteLine($"Checking out repository {repo}");
    }
}