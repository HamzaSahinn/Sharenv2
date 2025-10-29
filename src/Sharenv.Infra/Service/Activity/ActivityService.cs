using Microsoft.EntityFrameworkCore;
using Sharenv.Application.Interfaces;
using Sharenv.Application.Models;
using Sharenv.Application.Validation;
using Sharenv.Domain.Entities;
using Sharenv.Domain.Exceptions;
using Sharenv.Infra.Data;

namespace Sharenv.Infra.Service
{
    public class ActivityService : EntityService<Activity>, IActivityService
    {
        private ICircleMemberService _circleMemberService;

        public ActivityService(ICircleMemberService circleMemberService, SharenvDbContext repository) : base(repository)
        {
            _circleMemberService = circleMemberService;
        }

        /// <summary>
        /// Query circle activites by query context with user control
        /// </summary>
        /// <param name="queryContext"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Result<PagedData<Activity>> QueryCircleActivity(CircleActivityQueryContext queryContext, int userId)
        {
            return Execute<PagedData<Activity>>(res =>
            {
                ArgumentValidation.ThrowIfNull(queryContext);
                ArgumentValidation.ThrowIfLessThan(queryContext.CircleId, 1);

                if(!_circleMemberService.IsMemberOf(queryContext.CircleId, userId).ValueOrException)
                {
                    throw new SharenvUnauthorizedEntityAccessException();
                }

                res.Value = QueryCircleActivity(queryContext).ValueOrException;
            });
        }

        /// <summary>
        /// Query circle activites by query context
        /// </summary>
        /// <param name="queryContext"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Result<PagedData<Activity>> QueryCircleActivity(CircleActivityQueryContext queryContext)
        {
            return Execute<PagedData<Activity>>(res =>
            {
                ArgumentValidation.ThrowIfNull(queryContext);
                ArgumentValidation.ThrowIfLessThan(queryContext.CircleId, 1);

                var query = from activity in _repositroy.Activity
                            where activity.CircleId == queryContext.CircleId
                            select activity;

                query = query.Where(x => 
                    (!queryContext.StartDate.Start.HasValue || x.StartDate >= queryContext.StartDate.Start.Value) && 
                    (!queryContext.StartDate.End.HasValue || x.StartDate <= queryContext.StartDate.End.Value) && 
                    x.StartDate.HasValue);

                query = query.Where(x => 
                    (!queryContext.EndDate.Start.HasValue || x.EndDate >= queryContext.EndDate.Start.Value) && 
                    (!queryContext.EndDate.End.HasValue || x.EndDate <= queryContext.EndDate.End.Value) && 
                    x.EndDate.HasValue);

                if (!string.IsNullOrWhiteSpace(queryContext.Name))
                {
                    query = query.Where(x => x.Name.StartsWith(queryContext.Name));
                }

                query = ApplySorting(query, queryContext);
                res.Value = ApplyPagination(query, queryContext);
            });
        }

        /// <summary>
        /// Get actvity details with user control
        /// </summary>
        /// <param name="activityId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="SharenvUnauthorizedEntityAccessException"></exception>
        public Result<ActivityWithDetails> GetActivityWithDetails(int activityId, int userId)
        {
            return Execute< ActivityWithDetails>(res =>
            {
                ArgumentValidation.ThrowIfLessThan(activityId, 1);

                var activity = _repositroy.Activity.FirstOrDefault(x => x.Id == activityId);
                if (activity == null)
                {
                    return;
                }

                if (!_circleMemberService.IsMemberOf(activity.CircleId, userId).ValueOrException)
                {
                    throw new SharenvUnauthorizedEntityAccessException();
                }

                var momentCount = _repositroy.Moment.Count(x => x.ActivityId == activityId);

                res.Value = new ActivityWithDetails()
                {
                    Activity = activity,
                    MomentsCount = momentCount,
                };
            });
        }

        /// <summary>
        /// Delete activity with all related data
        /// </summary>
        /// <param name="activityId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="SharenvUnauthorizedEntityAccessException"></exception>
        public Result<Activity> DeleteActivityWithData(int activityId, int userId)
        {
            return Execute<Activity>(res =>
            {
                ArgumentValidation.ThrowIfLessThan(activityId, 1);

                var entity = _repositroy.Activity.FirstOrDefault(x => x.Id == activityId);

                if(entity == null)
                {
                    res.AddError("Entity did not found");
                    return;
                }

                var role = _circleMemberService.GetMemberRole(entity.CircleId, userId).ValueOrException;
                if(role != CircleMemberRole.Admin)
                {
                    throw new SharenvUnauthorizedEntityAccessException();
                }

                var momentQuery = _repositroy.Moment.Where(x => x.ActivityId == entity.Id);
                var momentIds = momentQuery.Select(x => x.Id);
                
                ExecuteInDbTransaction(res =>
                {
                    
                    momentQuery.ExecuteDelete();
                    _repositroy.Activity.Remove(entity);

                    //TODO: Delete disk data

                    _repositroy.SaveChanges();
                }).ThrowIfError();

                res.Value = entity;
            });
        }
    }
}
