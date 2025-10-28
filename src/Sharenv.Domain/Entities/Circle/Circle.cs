
namespace Sharenv.Domain.Entities
{
    public class Circle : AuditableEntity
    {   
        /// <summary>
        /// Gets or sets name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// gets or sets description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets isPublic
        /// </summary>
        public bool IsPublic { get; set; }
    }
}
