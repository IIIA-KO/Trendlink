using Microsoft.EntityFrameworkCore;

namespace Trendlink.Application.Pagination
{
    public sealed class PagedList<T> : List<T>
    {
        private PagedList(IEnumerable<T> items, int totalCount, int currentPage, int pageSize)
        {
            this.PageNumber = currentPage;
            this.TotalCount = totalCount;
            this.PageSize = pageSize;
            this.TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            this.AddRange(items);
        }

        public int PageNumber { get; }

        public int TotalPages { get; }

        public int PageSize { get; }

        public int TotalCount { get; }

        public bool HasNextPage => this.PageNumber * this.PageSize < this.TotalCount;

        public bool HasPreviousPage => this.PageNumber > 1;

        public static async Task<PagedList<T>> CreateAsync(
            IQueryable<T> query,
            int currentPage,
            int pageSize
        )
        {
            try
            {
                int totalCount = await query.CountAsync();

                List<T> items = await query
                    .Skip((currentPage - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return new PagedList<T>(items, totalCount, currentPage, pageSize);
            }
            catch (InvalidOperationException)
            {
                return new PagedList<T>([], 0, currentPage, pageSize);
            }
        }
    }
}
