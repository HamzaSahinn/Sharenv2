
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
    }
}
