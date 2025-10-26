using System.ComponentModel.DataAnnotations.Schema;

namespace Sharenv.Domain.Entities
{
    public class Activity : AuditableEntity
    {
        /// <summary>
        /// Gets or sets circleId
        /// </summary>
        public int CircleId { get; set; }

        /// <summary>
        /// Gets or sets name of activity
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets banner url
        /// </summary>
        public string? BannerUrl { get; set; }

        /// <summary>
        /// Gets or sets state
        /// </summary>
        [NotMapped]
        public ActivityState StateEnum
        {
            get
            {
                return (ActivityState)State;
            }

            set
            {
                State = (int)value;
            }
        }

        /// <summary>
        /// Gets or sets state do not use this property
        /// Use StateEnum this is exits for presistance operations
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// Gets or sets startDate
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Gets or sets endDate
        /// </summary>
        public DateTime? EndDate { get; set; }
    }
}
