namespace AvansDevOps.Domain.Models.SCM;

    public class GitAdapter : ISourceControl
    {
        private readonly GitLibrary _gitLibrary;

        public GitAdapter(GitLibrary gitLibrary)
        {
            _gitLibrary = gitLibrary;
        }

        public void Commit(string message)
        {
            _gitLibrary.GitCommit(message);
        }

        public void CreateBranch(string branchName)
        {
            _gitLibrary.GitCreateBranch(branchName);
        }

        public void Checkout(string branchName)
        {
            _gitLibrary.GitCheckout(branchName);
        }
    }