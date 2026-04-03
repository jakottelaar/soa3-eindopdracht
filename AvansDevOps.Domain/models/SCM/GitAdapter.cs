namespace AvansDevOps.Domain.Models.SCM;

    public class GitAdapter : ISourceControl
    {
        private readonly GitLibrary gitLibrary;

        public GitAdapter(GitLibrary gitLibrary)
        {
            this.gitLibrary = gitLibrary;
        }

        public void Commit(string message, string repo, string branch, string author)
        {
            gitLibrary.GitCommit(message, repo, branch, author);
        }

        public void CreateBranch(string branchName, string repo)
        {
            gitLibrary.GitCreateBranch(branchName, repo);
        }

        public void Checkout(string repo)
        {
            gitLibrary.GitCheckout(repo);
        }
    }