using Sharenv.Application.Interfaces;
using Sharenv.Application.Models;
using Sharenv.Application.Validation;
using Sharenv.Domain.Entities;
using Sharenv.Infra.Data;

namespace Sharenv.Infra.Service
{
    public class CircleMemberService : EntityService<CircleMember>, ICircleMemberService
    {
        public CircleMemberService(SharenvDbContext repository) : base(repository)
        {
        }

        /// <summary>
        /// Get members of circle by id
        /// </summary>
        /// <param name="queryContext"></param>
        /// <returns></returns>
        public Result<PagedData<CircleMemberWithUserDetails>> GetMembers(CircleMembersGetQueryContext queryContext)
        {
            return Execute<PagedData<CircleMemberWithUserDetails>>(res =>
            {
                ArgumentValidation.ThrowIfNull(queryContext);
                ArgumentValidation.ThrowIfLessThan(queryContext.CircleId, 1);

                var query = from circleMember in _repositroy.CircleMember
                            where circleMember.CircleId == queryContext.CircleId
                            join user in _repositroy.User on circleMember.UserId equals user.Id
                            select new CircleMemberWithUserDetails()
                            {
                                JoinedAt = circleMember.JoinedAt,
                                MemberFullname = user.FullName,
                                Role = circleMember.Role,
                            };

                query = ApplySorting(query, queryContext);
                res.Value = ApplyPagination(query, queryContext);
            });
        }

        /// <summary>
        /// Is user member of circle
        /// </summary>
        /// <param name="circleId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Result<bool> IsMemberOf(int circleId, int userId)
        {
            return Execute<bool>(res =>
            {
                res.Value = _repositroy.CircleMember.Any(x => x.CircleId == circleId && x.UserId == userId);
            });
        }

        /// <summary>
        /// Get role of the user in given circle
        /// </summary>
        /// <param name="circleId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Result<CircleMemberRole> GetMemberRole(int circleId, int userId)
        {
            return Execute<CircleMemberRole>(res =>
            {
                var member = _repositroy.CircleMember.FirstOrDefault(x => x.CircleId == circleId && x.UserId == userId);
                if(member == null)
                {
                    res.AddError("Does not member of the Circle");
                }

                res.Value = member.RoleEnum;
            });
        }
    }
}
