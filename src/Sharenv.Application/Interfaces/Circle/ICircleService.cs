using Sharenv.Application.Models;
using Sharenv.Application.Models.Circle;
using Sharenv.Domain.Entities;

namespace Sharenv.Application.Interfaces
{
    public interface ICircleService : IEntityService<Circle>
    {
        /// <summary>
        /// Query circles with a query context
        /// </summary>
        /// <param name="queryContext"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Result<PagedData<Circle>> QueryPublicCircles(CircleQueryContext queryContext);

        /// <summary>
        /// Query circles by query context and user
        /// </summary>
        /// <param name="queryContext"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Result<PagedData<Circle>> QueryUserCircles(CircleQueryContext queryContext, int userId);
    }
}
