using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TestMaker.Common.Repository
{
    public interface IRepository<T> where T : Entity
    {
        Task<List<T>> GetAsync();

        Task<List<T>> GetAsync(Expression<Func<T, bool>> predicate);

        Task<List<T>> GetAsync(Expression<Func<T, bool>> predicate, int skip, int take);

        Task<int> CountAsync();

        Task<int> CountAsync(Expression<Func<T, bool>> predicate);

        Task<T?> GetAsync(Guid id);

        Task CreateAsync(T entity);

        Task CreateAsync(List<T> entities);

        Task UpdateAsync(T entity);

        Task UpdateAsync(List<T> entities);

        Task DeleteAsync(Guid id);

        Task DeleteAsync(Expression<Func<T, bool>> predicate);

        Task RestoreAsync(Guid id);

        Task RestoreAsync(Expression<Func<T, bool>> predicate);
    }
}
