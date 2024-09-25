using LibaryManagementSystem.Data.Repositories.Abstracts;

namespace LibaryManagementSystem.Data.Repositories
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private IDbContext _dbContext;

        public UnitOfWork(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Commit()
        {
            return _dbContext.SaveChanges();
        }

        public async Task<int> CommitAsync()
        {
            return await _dbContext.SaveChangesAsync(CancellationToken.None);
        }
    }
}
