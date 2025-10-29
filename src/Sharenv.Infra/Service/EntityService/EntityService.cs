using Sharenv.Application.Interfaces;
using Sharenv.Application.Models;
using Sharenv.Application.Models.Common;
using Sharenv.Application.Service;
using Sharenv.Application.Validation;
using Sharenv.Domain.Entities;
using Sharenv.Infra.Data;
using System.Linq.Expressions;
using System.Reflection;

namespace Sharenv.Infra.Service
{
    public class EntityService<TEntity> : SharenvBaseService, IEntityService<TEntity> where TEntity : BaseEntity
    {
        private static MethodInfo? _orderBy = typeof(Queryable).GetMethods()
                .Where(m => m.Name == "OrderBy" && m.IsGenericMethodDefinition)
                .Where(m => m.GetParameters().Length == 2)
                .Single();

        private static MethodInfo? _orderByDescending = typeof(Queryable).GetMethods()
                .Where(m => m.Name == "OrderByDescending" && m.IsGenericMethodDefinition)
                .Where(m => m.GetParameters().Length == 2)
                .Single();

        protected SharenvDbContext _repositroy;

        public EntityService(SharenvDbContext repository) 
        {
            _repositroy = repository;
        }

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual Result<TEntity> Delete(TEntity entity)
        {
            return Execute<TEntity>(res =>
            {
                var removed =_repositroy.Set<TEntity>().Remove(entity);
                res.Value = removed.Entity;

                _repositroy.SaveChanges();
            });
        }

        /// <summary>
        /// Get bnase entity by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual Result<TEntity> GetById(int id)
        {
            return Execute<TEntity>(res =>
            {
                res.Value = _repositroy.Set<TEntity>().First(x => x.Id == id);
            });
        }

        /// <summary>
        /// Insert new entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual Result<TEntity> Insert(TEntity entity)
        {
            return Execute<TEntity>(res =>
            {
                ArgumentValidation.ThrowIfPositive(entity.Id);

                var added = _repositroy.Set<TEntity>().Add(entity);
                res.Value = added.Entity;

                _repositroy.SaveChanges();
            });
        }

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual Result<TEntity> Update(TEntity entity)
        {
            return Execute<TEntity>(res =>
            {
                ArgumentValidation.ThrowIfLessThan(entity.Id, 1);

                var added = _repositroy.Set<TEntity>().Update(entity);
                res.Value = added.Entity;

                _repositroy.SaveChanges();
            });
        }

        /// <summary>
        /// Apply pagination
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="queryContext"></param>
        protected virtual PagedData<T> ApplyPagination<T>(IQueryable<T> query, QueryContext queryContext)
        {
            int totalCount = query.Count();

            int skip = (queryContext.PageIndex - 1) * queryContext.PageSize;

            query = query.Skip(skip).Take(queryContext.PageSize);

            var items = query.ToList();

            return new PagedData<T>(
            items,
            totalCount,
            queryContext.PageIndex,
            queryContext.PageSize);
        }

        /// <summary>
        /// Apply sorting
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="sortBy"></param>
        /// <param name="isAscending"></param>
        /// <returns></returns>
        protected IQueryable<T> ApplySorting<T>(IQueryable<T> query, string? sortBy, bool isAscending) where T : class
        {
            if (string.IsNullOrWhiteSpace(sortBy))
            {
                return query;
            }

            var propertyInfo = typeof(T).GetProperty(sortBy, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (propertyInfo == null)
            {
                return query;
            }

            var parameter = Expression.Parameter(typeof(T), "x");
            var propertyAccess = Expression.Property(parameter, propertyInfo);
            var conversion = Expression.Convert(propertyAccess, typeof(object));
            var lambda = Expression.Lambda<Func<T, object>>(conversion, parameter);

            var orderByMethod = isAscending ? _orderBy : _orderByDescending;
            var genericMethod = orderByMethod.MakeGenericMethod(typeof(T), typeof(object));

            return (IQueryable<T>)genericMethod.Invoke(null, new object[] { query, lambda })!;
        }

        /// <summary>
        /// Apply sorting
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="queryContext"></param>
        /// <returns></returns>
        protected IQueryable<T> ApplySorting<T>(IQueryable<T> query, SortableQueryContext queryContext) where T : class
        {
            return ApplySorting(query, queryContext.SortBy, queryContext.IsAscending);
        }

        /// <summary>
        /// Execute given action in db transaction
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <returns></returns>
        protected Result<T> ExecuteInDbTransaction<T>(Action<Result<T>> action)
        {
            var result = new Result<T>();
            using (var scope = _repositroy.Database.BeginTransaction())
            {
                try
                {
                    action(result);
                    scope.Commit();
                }
                catch (Exception ex)
                {
                    scope.Rollback();
                    result.AddException(ex);
                }
            }

            return result;
        }

        /// <summary>
        /// Execute given action in db transaction
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <returns></returns>
        protected Result ExecuteInDbTransaction(Action<Result> action)
        {
            var result = new Result();
            using (var scope = _repositroy.Database.BeginTransaction())
            {
                try
                {
                    action(result);
                    _repositroy.SaveChanges();
                    scope.Commit();
                }
                catch (Exception ex)
                {
                    scope.Rollback();
                    result.AddException(ex);
                }
            }

            return result;
        }
    }
}
