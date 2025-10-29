using Sharenv.Domain.Entities;

namespace Sharenv.Application.Models
{
    public class ActivityWithDetails
    {
        /// <summary>
        /// Gets or sets activity
        /// </summary>
        public Activity Activity { get; set; }

        /// <summary>
        /// Gets or sets momentsCount
        /// </summary>
        public int MomentsCount { get; set; }
    }
}
