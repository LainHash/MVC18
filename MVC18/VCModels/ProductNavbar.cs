using Microsoft.AspNetCore.Mvc;
using MVC18.Data;

namespace MVC18.VCModels
{
    public class ProductNavbar : ViewComponent
    {
        private readonly LaptopWebDb06Context _context;
        public ProductNavbar(LaptopWebDb06Context context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var categories = _context.Categories
                .Select(c => new ViewModels.CategoryMenu
                {
                    Name = c.CategoryName,
                    Count = c.Products.Count(p => !p.IsDeleted)
                })
                .ToList();
            return View(categories);
        }
    }
}
