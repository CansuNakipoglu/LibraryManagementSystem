using LibaryManagementSystem.Core.Models.Entities;
using LibaryManagementSystem.Data.Repositories.Abstracts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LibaryManagementSystem.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DbSet<T> dbSet;
        private readonly IDbContext _dbContext;

        public GenericRepository(IDbContext context)
        {
            dbSet = context.Set<T>();
            _dbContext = context;
        }

        public virtual EntityState Add(T entity)
        {
            return dbSet.Add(entity).State;
        }

        public virtual EntityState Delete(T entity)
        {
            return dbSet.Remove(entity).State;
        }

        public bool Exists(Expression<Func<T, bool>> predicate)
        {
            return dbSet.Any(predicate);
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return dbSet.Where(predicate);
        }

        public IQueryable<T> GetAll()
        {
            return dbSet;
        }

        public List<Book> GetAllBooks()
        {
            var bookDbSet = _dbContext.Set<Book>();
            return bookDbSet.Include(x=> x.Author).ToList();
        }

        public async Task<T?> GetAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual EntityState Update(T entity)
        {
            return dbSet.Update(entity).State;
        }
    }
}
