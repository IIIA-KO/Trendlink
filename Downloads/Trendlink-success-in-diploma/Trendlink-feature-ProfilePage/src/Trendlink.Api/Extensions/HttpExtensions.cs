using System.Text.Json;

namespace Trendlink.Api.Extensions
{
    public static class HttpExtensions
    {
        public static void AddPaginationHeader(
            this HttpResponse response,
            int currentPage,
            int itemsPerPage,
            int totalItems,
            int totalPages,
            bool hasNextPage,
            bool hasPrevoiusPage
        )
        {
            var paginationHeader = new
            {
                currentPage,
                itemsPerPage,
                totalItems,
                totalPages,
                hasNextPage,
                hasPrevoiusPage
            };

            response.Headers["Pagination"] = JsonSerializer.Serialize(paginationHeader);
            response.Headers["Access-Control-Expose-Headers"] = "Pagination";
        }
    }
}
