using Sharenv.Application.Interfaces;
using Sharenv.Application.Models;
using Sharenv.Application.Models.Circle;
using Sharenv.Application.Validation;
using Sharenv.Domain.Entities;
using Sharenv.Infra.Data;
using System.Transactions;

namespace Sharenv.Infra.Service
{
    public class CircleService : EntityService<Circle>, ICircleService
    {
        public CircleService(SharenvDbContext repository) : base(repository)
        {
        }

        /// <summary>
        /// Query circles with a query context
        /// </summary>
        /// <param name="queryContext"></param>
        /// <returns></returns>
        public Result<PagedData<Circle>> QueryPublicCircles(CircleQueryContext queryContext)
        {
            return Execute<PagedData<Circle>>(res =>
            {
                ArgumentValidation.ThrowIfNull(queryContext);

                IQueryable<Circle> query = _repositroy.Circle.Where(x => x.IsPublic);

                if (!string.IsNullOrWhiteSpace(queryContext.Name))
                {
                    query = query.Where(x => x.Name.StartsWith(queryContext.Name));
                }

                //TODO: Elatic search can be integrated
                if (!string.IsNullOrWhiteSpace(queryContext.Description))
                {
                    query = query.Where(x => x.Description.StartsWith(queryContext.Description));
                }

                query = ApplySorting(query, queryContext);
                res.Value = ApplyPagination(query, queryContext);
            });
        }

        /// <summary>
        /// Query circles by query context and user
        /// </summary>
        /// <param name="queryContext"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Result<PagedData<Circle>> QueryUserCircles(CircleQueryContext queryContext, int userId)
        {
            return Execute<PagedData<Circle>>(res =>
            {
                ArgumentValidation.ThrowIfNull(queryContext);
                var query = from circleMember in _repositroy.CircleMember
                            join circle in _repositroy.Circle on circleMember.CircleId equals circle.Id
                            where circleMember.UserId == userId
                            select circle;

                if (!string.IsNullOrWhiteSpace(queryContext.Name))
                {
                    query = query.Where(x => x.Name.StartsWith(queryContext.Name));
                }

                //TODO: Elatic search can be integrated
                if (!string.IsNullOrWhiteSpace(queryContext.Description))
                {
                    query = query.Where(x => x.Description.StartsWith(queryContext.Description));
                }

                query = ApplySorting(query, queryContext);
                res.Value = ApplyPagination(query, queryContext);
            });
        }

        /// <summary>
        /// Create new circle and add initial user as member
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Result<Circle> CreateNewCircle (Circle entity, int userId)
        {
            return Execute<Circle>(res =>
            {
                ArgumentValidation.ThrowIfNull(entity);
                ArgumentValidation.ThrowIfPositive(entity.Id);
                
                using (var scope = new TransactionScope())
                {
                    _repositroy.Circle.Add(entity);
                    _repositroy.SaveChanges();

                    var member = new CircleMember() 
                    { 
                        CircleId = entity.Id,
                        UserId = userId ,
                        RoleEnum = CircleMemberRole.Admin,
                        JoinedAt = DateTime.Now,
                    };

                    _repositroy.CircleMember.Add(member);
                    _repositroy.SaveChanges();

                    scope.Complete();
                }

                res.Value = entity;
            });
        }
    }
}
