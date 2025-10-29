using Microsoft.EntityFrameworkCore;
using Sharenv.Application.Interfaces;
using Sharenv.Application.Models;
using Sharenv.Application.Models.Circle;
using Sharenv.Application.Validation;
using Sharenv.Domain.Entities;
using Sharenv.Domain.Exceptions;
using Sharenv.Infra.Data;

namespace Sharenv.Infra.Service
{
    public class CircleService : EntityService<Circle>, ICircleService
    {
        private ICircleMemberService _circleMemberService;

        public CircleService(ICircleMemberService circleMemberService, SharenvDbContext repository) : base(repository)
        {
            _circleMemberService = circleMemberService;
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

                ExecuteInDbTransaction(res =>
                {
                    _repositroy.Circle.Add(entity);
                    _repositroy.SaveChanges();

                    var member = new CircleMember()
                    {
                        CircleId = entity.Id,
                        UserId = userId,
                        RoleEnum = CircleMemberRole.Admin,
                        JoinedAt = DateTime.Now,
                    };

                    _repositroy.CircleMember.Add(member);
                }).ThrowIfError();

                res.Value = entity;
            });
        }

        /// <summary>
        /// Delete circle with data
        /// </summary>
        /// <param name="circleId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="SharenvUnauthorizedEntityAccessException"></exception>
        public Result<Circle> DeleteCircleWithData(int circleId, int userId)
        {
            return Execute<Circle>(res =>
            {
                ArgumentValidation.ThrowIfLessThan(circleId, 1);

                var role = _circleMemberService.GetMemberRole(circleId, userId).ValueOrException;
                if(role != CircleMemberRole.Admin)
                {
                    throw new SharenvUnauthorizedEntityAccessException();
                }

                var entity = _repositroy.Circle.FirstOrDefault(x => x.Id == circleId);
                if(entity == null)
                {
                    res.AddError("Circle did not found");
                    return;
                }

                var activityQuery = _repositroy.Activity.Where(x => x.CircleId == entity.Id);
                var activityIds = activityQuery.Select(x => x.Id).ToList();

                var momentQuery = _repositroy.Moment.Where(x => activityIds.Contains(x.ActivityId));
                var momentIds = momentQuery.Select(x => x.Id).ToList();

                ExecuteInDbTransaction(res =>
                {
                    momentQuery.ExecuteDelete();
                    activityQuery.ExecuteDelete();

                    //TODO: Delete disk data

                    _repositroy.Circle.Remove(entity);
                }).ThrowIfError();
            });
        }
    }
}
