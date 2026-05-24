using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC18.Data;
using MVC18.DTOs.Misc;
using MVC18.Helpers.Constants.Misc;
using MVC18.ResultModels.Misc;
using MVC18.Services.Interfaces.Commons;

namespace MVC18.Services.Implementations.Commons
{
    public class CommonService : ICommonService
    {
        private readonly LaptopWebDb06Context _context;

        public CommonService(LaptopWebDb06Context context)
        {
            _context = context;
        }

        public CategoryResult GetAllCategories()
        {
            var categories = _context.Categories
                .Select(c => new CategoryDTO
                {
                    CategoryName = c.CategoryName,
                    Count = c.Products.Count()
                })
                .OrderByDescending(c => c.Count)
                .ToList();

            return new CategoryResult
            {
                Categories = categories
            };
        }

        public List<SelectListItem> GetAllSortByOptions()
        {
            var list = new List<SelectListItem>
            {
                new SelectListItem { Text = "Tên (A-Z)", Value = SortByConstants.NameAsc },
                new SelectListItem { Text = "Tên (Z-A)", Value = SortByConstants.NameDesc },
                new SelectListItem { Text = "Giá (Tăng dần)", Value = SortByConstants.PriceAsc },
                new SelectListItem { Text = "Giá (Giảm dần)", Value = SortByConstants.PriceDesc },
                new SelectListItem { Text = "Ngày tạo (Cũ nhất)", Value = SortByConstants.CreatedAtAsc },
                new SelectListItem { Text = "Ngày tạo (Mới nhất)", Value = SortByConstants.CreatedAtDesc }
            };
            return list;
        }

        public SupplierResult GetAllSuppliers(string? categoryName)
        {
            var suppliers = _context.Suppliers
                .Where(s => string.IsNullOrEmpty(categoryName) || s.Products.Any(p => p.Category.CategoryName == categoryName))
                .Select(s => new SupplierDTO
                {
                    CompanyName = s.CompanyName,
                    Count = s.Products.Count()
                })
                .OrderByDescending(s => s.Count)
                .ToList();

            return new SupplierResult
            {
                Suppliers = suppliers
            };
        }

        public List<SelectListItem> GetCategoriesForCreate()
        {
            var categories = _context.Categories
                .Select(c => new SelectListItem
                {
                    Text = c.CategoryName,
                    Value = c.CategoryId.ToString()
                })
                .ToList();
            return categories;
        }

        public List<SelectListItem> GetSuppliersForCreate()
        {
            var suppliers = _context.Suppliers
                .Select(s => new SelectListItem
                {
                    Text = s.CompanyName,
                    Value = s.SupplierId.ToString()
                })
                .ToList();
            return suppliers;
        }
    }
}
