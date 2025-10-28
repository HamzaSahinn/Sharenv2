using Sharenv.Application.Models;
using Sharenv.Domain.Entities;

namespace Sharenv.Application.Interfaces
{
    public interface IEntityService<T> where T : BaseEntity
    {
        /// <summary>
        /// Insert entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Result<T> Insert(T entity);

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Result<T> Update(T entity);

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Result<T> Delete(T entity);

        /// <summary>
        /// Get entity by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Result<T> GetById(int id);
    }
}
