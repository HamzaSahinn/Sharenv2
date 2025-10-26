
namespace Sharenv.Domain.Entities
{
    public class AuditableEntity : BaseEntity
    {
        /// <summary>
        /// Gets or sets createdBy 
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets createdAt
        /// </summary>
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets updatedBy
        /// </summary>
        public int UpdatedBy { get; set; }

        /// <summary>
        /// Gets or sets updatedAt
        /// </summary>
        public DateTime? UpdatedAt { get; set; }
    }
}
