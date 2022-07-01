using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TestMaker.TestService.Infrastructure.Entities;

namespace TestMaker.TestService.Infrastructure.Repositories
{
    public interface IRepository<T> where T : Entity
    {
        Task<List<T>> GetAsync();

        Task<List<T>> GetAsync(Expression<Func<T, bool>> predicate);

        Task<List<T>> GetAsync(Expression<Func<T, bool>> predicate, int skip, int take);

        Task<int> CountAsync();

        Task<int> CountAsync(Expression<Func<T, bool>> predicate);

        Task<T> GetAsync(Guid id);

        Task<bool> CreateAsync(T entity);

        Task<bool> UpdateAsync(T entity);

        Task<bool> DeleteAsync(Guid id);
    }
}
