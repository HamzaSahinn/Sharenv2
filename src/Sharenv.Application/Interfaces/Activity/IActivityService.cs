using Sharenv.Application.Models;
using Sharenv.Domain.Entities;

namespace Sharenv.Application.Interfaces
{
    public interface IActivityService : IEntityService<Activity>
    {
        /// <summary>
        /// Query circle activites by query context with user control
        /// </summary>
        /// <param name="queryContext"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Result<PagedData<Activity>> QueryCircleActivity(CircleActivityQueryContext queryContext, int userId);

        /// <summary>
        /// Query circle activites by query context
        /// </summary>
        /// <param name="queryContext"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Result<PagedData<Activity>> QueryCircleActivity(CircleActivityQueryContext queryContext);

        /// <summary>
        /// Get actvity details with user control
        /// </summary>
        /// <param name="activityId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="SharenvUnauthorizedEntityAccessException"></exception>
        public Result<ActivityWithDetails> GetActivityWithDetails(int activityId, int userId);

        /// <summary>
        /// Delete activity with all related data
        /// </summary>
        /// <param name="activityId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="SharenvUnauthorizedEntityAccessException"></exception>
        public Result<Activity> DeleteActivityWithData(int activityId, int userId);
    }
}
