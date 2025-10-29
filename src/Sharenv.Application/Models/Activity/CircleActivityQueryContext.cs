using Sharenv.Application.Models.Common;
using Sharenv.Domain.Models;

namespace Sharenv.Application.Models
{
    public class CircleActivityQueryContext : SortableQueryContext
    {
        /// <summary>
        /// Gets or sets circleId
        /// </summary>
        public int CircleId { get; set; }

        /// <summary>
        /// Gets or sets name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets startDate range
        /// </summary>
        public DateTimeRange StartDate { get; set; }

        /// <summary>
        /// Gets or sets endDate range
        /// </summary>
        public DateTimeRange EndDate { get; set; }
    }
}
