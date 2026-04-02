namespace AvansDevOps.Domain.Models.SCM;

    public interface ISourceControl
    {
        void Commit(string message);
        void CreateBranch(string branchName);
        void Checkout(string branchName);
    }