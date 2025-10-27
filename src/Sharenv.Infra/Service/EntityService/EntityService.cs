using Microsoft.EntityFrameworkCore;
using Sharenv.Application.Interfaces;
using Sharenv.Application.Service;
using Sharenv.Application.Validation;
using Sharenv.Domain.Entities;
using Sharenv.Domain.Models;

namespace Sharenv.Infra.Service
{
    public class EntityService<T> : SharenvBaseService, IEntityService<T> where T : BaseEntity
    {
        protected DbContext _repositroy;

        public EntityService(DbContext repository) 
        {
            _repositroy = repository;
        }

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual Result<T> Delete(T entity)
        {
            return Execute<T>(res =>
            {
                var removed =_repositroy.Set<T>().Remove(entity);
                res.Value = (T) removed.Entity;

                _repositroy.SaveChanges();
            });
        }

        /// <summary>
        /// Get bnase entity by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual Result<T> GetById(int id)
        {
            return Execute<T>(res =>
            {
                res.Value = _repositroy.Set<T>().First(x => x.Id == id);
            });
        }

        /// <summary>
        /// Insert new entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual Result<T> Insert(T entity)
        {
            return Execute<T>(res =>
            {
                ArgumentValidation.ThrowIfPositive(entity.Id);

                var added = _repositroy.Set<T>().Add(entity);
                res.Value = added.Entity;

                _repositroy.SaveChanges();
            });
        }

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual Result<T> Update(T entity)
        {
            return Execute<T>(res =>
            {
                ArgumentValidation.ThrowIfLessThan(entity.Id, 1);

                var added = _repositroy.Set<T>().Update(entity);
                res.Value = added.Entity;

                _repositroy.SaveChanges();
            });
        }
    }
}
