
namespace Sharenv.Application.Models
{
    public class PagedData<T> 
    {
        /// <summary>
        /// Gets or sets items
        /// </summary>
        public IEnumerable<T> Items { get; set; }

        /// <summary>
        /// Gets or sets totalCount
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// Gets or sets totalPages
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// Gets or sets pageSize
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Gets or sets pageIndex
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// Get hasPreviousPage
        /// </summary>
        public bool HasPreviousPage => PageIndex > 1;

        /// <summary>
        /// Get hasNextpage
        /// </summary>
        public bool HasNextPage => PageIndex < TotalPages;

        /// <summary>
        /// Default ctor
        /// </summary>
        public PagedData() { }

        /// <summary>
        /// Ctor with enumerable 
        /// </summary>
        /// <param name="items"></param>
        /// <param name="count"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        public PagedData(IEnumerable<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = count;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            Items = items;
        }
    }
}
