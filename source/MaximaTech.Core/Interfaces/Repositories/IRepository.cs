using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MaximaTech.Core.Interfaces.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Add new entity
        /// </summary>
        /// <param name="entity">Entity instance</param>
        void Add(TEntity entity);

        /// <summary>
        /// Add many entitites
        /// </summary>
        /// <param name="entities">Entities list</param>
        void AddMany(List<TEntity> entities);

        /// <summary>
        /// Get entity by id
        /// </summary>
        /// <param name="id">Entity id</param>
        /// <returns>Entity instance</returns>
        Task<TEntity> FindByIdAsync(Guid id);

        /// <summary>
        /// Get entity by id
        /// </summary>
        /// <param name="id">Entity id</param>
        /// <returns>Entity instance</returns>
        TEntity FindById(Guid id);

        /// <summary>
        /// Count all async
        /// </summary>
        /// <returns>Entities total</returns>
        Task<int> CountAsync();

        /// <summary>
        /// Count all
        /// </summary>
        /// <returns>Entities total</returns>
        int Count();

        /// <summary>
        /// Get all entities
        /// </summary>
        /// <returns>List of entities</returns>
        Task<List<TEntity>> AllAsync();

        /// <summary>
        /// Get all entities
        /// </summary>
        /// <returns>List of entities</returns>
        List<TEntity> All();

        /// <summary>
        /// Update an entity
        /// </summary>
        /// <param name="entity">Entity instance</param>
        void Update(TEntity entity);

        /// <summary>
        /// Remove entity
        /// </summary>
        /// <param name="entity">Entity instance</param>
        void Remove(TEntity entity);
    }
}