namespace LibaryManagementSystem.Data.Repositories.Abstracts
{
    public interface IUnitOfWork
    {
        int Commit();
        Task<int> CommitAsync();
    }
}
