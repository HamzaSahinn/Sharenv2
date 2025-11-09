using Sharenv.Application.Models;
using Sharenv.Domain.Entities;

namespace Sharenv.Application.Interfaces
{
    public interface IMomentService : IEntityService<Moment>
    {
        public Result<Moment> Create(int activityId);

        public Result<Moment> CreateOrUpdate(Moment entity);
    }
}
