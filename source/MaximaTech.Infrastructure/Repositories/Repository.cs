using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MaximaTech.Core.Interfaces.Repositories;
using MaximaTech.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MaximaTech.Infrastructure.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly ApplicationDbContext _dbContext;

        public Repository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual void Add(TEntity entity)
        {
            _dbContext.Add(entity);
        }

        public virtual void AddMany(List<TEntity> entities)
        {
            _dbContext.AddRange(entities);
        }

        public virtual async Task<int> CountAsync()
        {
            return await _dbContext
                .Set<TEntity>()
                .CountAsync();
        }

        public virtual int Count()
        {
            return _dbContext
                .Set<TEntity>()
                .Count();
        }

        public virtual async Task<List<TEntity>> AllAsync()
        {
            return await _dbContext
                .Set<TEntity>()
                .ToListAsync();
        }

        public virtual List<TEntity> All()
        {
            return _dbContext
                .Set<TEntity>()
                .ToList();
        }

        public virtual TEntity FindById(Guid id)
        {
            return _dbContext
                .Set<TEntity>()
                .Find(id);
        }

        public virtual async Task<TEntity> FindByIdAsync(Guid id)
        {
            return await _dbContext
                .Set<TEntity>()
                .FindAsync(id);
        }

        public virtual void Remove(TEntity entity)
        {
            _dbContext
                .Set<TEntity>()
                .Remove(entity);
        }

        public virtual void Update(TEntity entity)
        {
            _dbContext
                .Set<TEntity>()
                .Update(entity);
        }
    }
}