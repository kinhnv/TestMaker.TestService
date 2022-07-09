using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TestMaker.Common.Repository
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

        public async Task<T?> GetAsync(Guid id, bool isNoTracked = false)
        {
            var entity = await _dbContext.Set<T>().FindAsync(id);
            if (isNoTracked && entity != null) {
                _dbContext.Entry(entity).State = EntityState.Detached;
            }
            return entity;
        }

        public async Task CreateAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task CreateAsync(List<T> entities)
        {
            foreach (var entity in entities)
            {
                await _dbContext.Set<T>().AddAsync(entity);
            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(List<T> entities)
        {
            foreach (var entity in entities)
            {
                _dbContext.Entry(entity).State = EntityState.Modified;
            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await GetAsync(id);
            if (entity != null) {
                entity.IsDeleted = true;
                _dbContext.Entry(entity).State = EntityState.Modified;
            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Expression<Func<T, bool>> predicate)
        {
            var entities = await GetAsync(predicate);
            foreach (var entity in entities)
            {
                entity.IsDeleted = true;
                _dbContext.Entry(entity).State = EntityState.Modified;
            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task RestoreAsync(Guid id)
        {
            var entity = await GetAsync(id);
            if (entity != null) {
                entity.IsDeleted = false;
                _dbContext.Entry(entity).State = EntityState.Modified;
            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task RestoreAsync(Expression<Func<T, bool>> predicate)
        {
            var entities = await GetAsync(predicate);
            foreach (var entity in entities)
            {
                entity.IsDeleted = false;
                _dbContext.Entry(entity).State = EntityState.Modified;
            }
            await _dbContext.SaveChangesAsync();
        }
    }
}
