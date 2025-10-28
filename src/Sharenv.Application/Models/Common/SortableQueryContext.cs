
namespace Sharenv.Application.Models.Common
{
    public class SortableQueryContext : QueryContext
    {
        /// <summary>
        /// Gets or sets sortBy, must be property name
        /// </summary>
        public string? SortBy { get; set; } = "Id";

        /// <summary>
        /// Gets or sets isAscending
        /// </summary>
        public bool IsAscending { get; set; } = false;
    }
}
