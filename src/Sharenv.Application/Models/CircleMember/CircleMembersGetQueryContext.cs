using Sharenv.Application.Models.Common;

namespace Sharenv.Application.Models
{
    public class CircleMembersGetQueryContext : SortableQueryContext
    {
        /// <summary>
        /// Gets or sets circleId
        /// </summary>
        public int CircleId { get; set; }
    }
}
