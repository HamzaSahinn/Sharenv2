using Sharenv.Application.Models;
using Sharenv.Domain.Entities;

namespace Sharenv.Application.Interfaces
{
    public interface ICircleMemberService : IEntityService<CircleMember>
    {
        /// <summary>
        /// Get members of circle by id
        /// </summary>
        /// <param name="queryContext"></param>
        /// <returns></returns>
        public Result<PagedData<CircleMemberWithUserDetails>> GetMembers(CircleMembersGetQueryContext queryContext);

        /// <summary>
        /// Is user member of circle
        /// </summary>
        /// <param name="circleId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Result<bool> IsMemberOf(int circleId, int userId);

        /// <summary>
        /// Get role of the user in given circle
        /// </summary>
        /// <param name="circleId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Result<CircleMemberRole> GetMemberRole(int circleId, int userId);
    }
}
