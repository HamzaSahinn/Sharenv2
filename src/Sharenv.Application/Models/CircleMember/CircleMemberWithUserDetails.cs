using Sharenv.Domain.Entities;

namespace Sharenv.Application.Models
{
    public class CircleMemberWithUserDetails
    {
        /// <summary>
        /// Gets or sets memberFullName
        /// </summary>
        public string MemberFullname { get; set; }

        /// <summary>
        /// Gets or sets role
        /// </summary>
        public CircleMemberRole RoleEnum
        {
            get
            {
                return (CircleMemberRole)Role;
            }
            set
            {
                Role = (int)value;
            }
        }

        /// <summary>
        /// Gets or sets role
        /// </summary>
        public int Role {  get; set; }

        /// <summary>
        /// Gets or sets JoinedAt
        /// </summary>
        public DateTime JoinedAt { get; set; }
    }
}
