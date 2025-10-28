using Sharenv.Application.Models.Common;

namespace Sharenv.Application.Models.Circle
{
    public class CircleQueryContext : SortableQueryContext
    {
        /// <summary>
        /// Gets or sets name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets description
        /// </summary>
        public string Description { get; set; }
    }
}
