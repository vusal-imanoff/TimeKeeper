﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TimeKeeperFinal.Core.IRepositories
{
    public interface IRepository<TEntity>
    {
        Task AddAsync(TEntity entity);
        Task<List<TEntity>> GetAllAsync(/*Expression<Func<TEntity, bool>> expression,*/ params string[] icludes);
        Task<List<TEntity>> GetAllForUsersAsync(Expression<Func<TEntity, bool>> expression, params string[] icludes);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression, params string[] icludes);
        Task<bool> IsExistsAsync(Expression<Func<TEntity, bool>> expression);
      
        void Remove(TEntity entity);
    }
}
