using Sharenv.Application.Interfaces;
using Sharenv.Application.Models;
using Sharenv.Application.Models.Circle;
using Sharenv.Application.Validation;
using Sharenv.Domain.Entities;
using Sharenv.Infra.Data;

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
    }
}
