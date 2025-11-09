using Sharenv.Application.Interfaces;
using Sharenv.Application.Models;
using Sharenv.Application.Validation;
using Sharenv.Domain.Entities;
using Sharenv.Infra.Data;

namespace Sharenv.Infra.Service
{
    public class MomentService : EntityService<Moment>, IMomentService
    {
        public MomentService(SharenvDbContext repository) : base(repository)
        {

        }

        /// <summary>
        /// Create new moment with empty data
        /// </summary>
        /// <param name="activityId"></param>
        /// <returns></returns>
        public Result<Moment> Create(int activityId)
        {
            return Execute<Moment>(res =>
            {
                ArgumentValidation.ThrowIfLessThan(activityId, 1);

                res.Value = this.Insert(new Moment { ActivityId = activityId }).ValueOrException;
            });
        }

        /// <summary>
        /// Create or update moment entitiy
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Result<Moment> CreateOrUpdate(Moment entity)
        {
            return Execute<Moment>(res =>
            {
                ArgumentValidation.ThrowIfNull(entity);
                ArgumentValidation.ThrowIfLessThan(entity.Id, 0);

                if (entity.Id == 0)
                {
                    res.Value = this.Insert(entity).ValueOrException;
                }
                else
                {
                    res.Value = this.Update(entity).ValueOrException;
                }
            });
        }
    }
}
