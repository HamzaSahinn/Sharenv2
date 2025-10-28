
namespace Sharenv.Application.Models
{
    public class QueryContext
    {
        /// <summary>
        /// Gets or sets enablePagination
        /// </summary>
        public bool EnablePagination { get; set; } = true;

        /// <summary>
        /// Gets or sets pageIndex
        /// </summary>
        public int PageIndex { get; set; } = 0;

        /// <summary>
        /// Gets or sets pageSize
        /// </summary>
        public int PageSize { get; set; } = 20;
    }
}
