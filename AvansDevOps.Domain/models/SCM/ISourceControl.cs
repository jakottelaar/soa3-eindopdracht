namespace AvansDevOps.Domain.Models.SCM;

    public interface ISourceControl
    {
        void Commit(string message, string repo, string branch, string author);
        void CreateBranch(string branchName, string repo);
        void Checkout(string repo);
    }