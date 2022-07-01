using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TestMaker.TestService.Infrastructure.Entities;

namespace TestMaker.TestService.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        protected readonly DbContext _dbContext;

        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<T>> GetAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<List<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<List<T>> GetAsync(Expression<Func<T, bool>> predicate, int skip, int take)
        {
            return await _dbContext.Set<T>().Where(predicate).Skip(skip).Take(take).ToListAsync();
        }

        public async Task<int> CountAsync()
        {
            return await _dbContext.Set<T>().CountAsync();
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().Where(predicate).CountAsync();
        }

        public async Task<T> GetAsync(Guid id)
        {
            try
            {
                return await _dbContext.Set<T>().FindAsync(id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> CreateAsync(T entity)
        {
            try
            {
                await _dbContext.Set<T>().AddAsync(entity);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            try
            {
                _dbContext.Entry(entity).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();

                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var entity = await GetAsync(id);
                if (entity != null)
                {
                    _dbContext.Set<T>().Remove(entity);
                    await _dbContext.SaveChangesAsync();

                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
