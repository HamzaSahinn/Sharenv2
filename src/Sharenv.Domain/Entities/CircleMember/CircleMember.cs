
namespace Sharenv.Domain.Entities
{
    public class CircleMember : AuditableEntity
    {
        /// <summary>
        /// Gets or sets circleId
        /// </summary>
        public int CircleId { get; set; }

        /// <summary>
        /// Gets and set userId
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets roleEnum
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
        /// Gets or sets joiendAt
        /// </summary>
        public DateTime JoinedAt { get; set; }
    }
}
