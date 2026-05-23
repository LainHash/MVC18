using MVC18.Helpers.Constants.Misc;
using MVC18.Models;

namespace MVC18.DTOs.Misc
{
    public class ProductQuery
    {
        public string? Keyword { get; set; }

        public string? CategoryName { get; set; }

        public string? CompanyName { get; set; }

        public string? SortBy { get; set; } = SortByConstants.CreatedAtDesc;

        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 10;
    }
}
