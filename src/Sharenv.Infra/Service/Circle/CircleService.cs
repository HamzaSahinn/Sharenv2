using Sharenv.Application.Interfaces;
using Sharenv.Domain.Entities;
using Sharenv.Infra.Data;

namespace Sharenv.Infra.Service
{
    public class CircleService : EntityService<Circle>, ICircleService
    {
        public CircleService(SharenvDbContext repository) : base(repository)
        {
        }
    }
}
