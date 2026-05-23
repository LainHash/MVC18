using MVC18.DTOs.Products;
using MVC18.ResultModels;

namespace MVC18.ResultModels.Misc
{
    public class PagedResult<T> : BaseResult
    {
        public IEnumerable<T> Items { get; set; } = [];

        public int TotalItems { get; set; }

        public int Page { get; set; }

        public int PageSize { get; set; }

        public int TotalPages =>
            (int)Math.Ceiling((double)TotalItems / PageSize);
    }
}
