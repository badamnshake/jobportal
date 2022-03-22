using System.Text.Json;
using JobSeeker.Infrastrucure.Helpers;
using Microsoft.AspNetCore.Http;

namespace JobSeeker.API.Extensions
{
    public static class HttpExtensions
    {
        // this class is used to add pagination to the get vacancies
        public static void AddPaginationHeader(this HttpResponse response, int currentPage, int itemsPerPage,
            int totalItems, int totalPages)
        {
            var paginationHeader = new PaginationHeader(currentPage, itemsPerPage, totalItems, totalPages);
            // making json camels case
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            // header is added
            response.Headers.Add("Pagination", JsonSerializer.Serialize(paginationHeader, options));
            response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
        }
    }
}