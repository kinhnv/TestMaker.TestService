﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TM.TestService.Infrastructure.Entities;

namespace TM.TestService.Infrastructure.Repositories
{
    public interface IRepository<T> where T : Entity
    {
        Task<List<T>> GetAllAsync();

        Task<T> GetAsync(Guid id);

        Task<bool> CreateAsync(T entity);

        Task<bool> UpdateAsync(T entity);

        Task<bool> DeleteAsync(Guid id);
    }
}
