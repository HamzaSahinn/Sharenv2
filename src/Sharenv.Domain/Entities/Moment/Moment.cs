
namespace Sharenv.Domain.Entities
{
    public class Moment : AuditableEntity
    {
        /// <summary>
        /// Gets or sets actvity id
        /// </summary>
        public int ActivityId { get; set; }

        /// <summary>
        /// Moment Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets description
        /// </summary>
        public string Description { get; set; }
    }
}
