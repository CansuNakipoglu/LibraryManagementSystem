using System.Linq.Expressions;
using LibaryManagementSystem.Core.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibaryManagementSystem.Data.Repositories.Abstracts
{
    public interface IGenericRepository<T>
    {
        EntityState Add(T entity);
        EntityState Delete(T entity);
        EntityState Update(T entity);

        Task<T> GetAsync(int id);

        IQueryable<T?> FindBy(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAll();

        bool Exists(Expression<Func<T, bool>> predicate);

        List<Book> GetAllBooks();
    }
}
