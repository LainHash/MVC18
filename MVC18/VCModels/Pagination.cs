using Microsoft.AspNetCore.Mvc;

namespace MVC18.VCModels
{
    public class Pagination : ViewComponent
    {
        public IViewComponentResult Invoke(int currentPage, int totalPages, string categoryName, string companyName, string sortBy)
        {
            var model = new ViewModels.PaginateVM
            {
                Page = currentPage,
                Total = totalPages,
                CategoryName = categoryName,
                CompanyName = companyName,
                SortBy = sortBy
            };
            return View(model);
        }
    }
}
