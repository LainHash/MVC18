using Microsoft.AspNetCore.Mvc;
using MVC18.Helpers.Constants.Misc;
using MVC18.Services.Interfaces.Products;

namespace MVC18.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _productService.GetAllAsync();
            return View(result.Products);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var result = await _productService.GetOneAsync(id);
            if (!result.Success)
            {
                return NotFound();
            }

            switch (result.Product!.CategoryName)
            {
                case CategoryConstants.Laptop:
                    return RedirectToAction("Details", "Laptop", new { id });
                case CategoryConstants.Cpu:
                    return RedirectToAction("Details", "Cpu", new { id });
                case CategoryConstants.Gpu:
                    return RedirectToAction("Details", "Gpu", new { id });
                case CategoryConstants.Ram:
                    return RedirectToAction("Details", "Ram", new { id });
                case CategoryConstants.Storage:
                    return RedirectToAction("Details", "Storage", new { id });
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        public async Task<IActionResult> Edit()
        {
            return View();
        }

        public async Task<IActionResult> Delete()
        {
            return View();
        }

        public async Task<IActionResult> GetAll()
        {
            var result = await _productService.GetAllAsync();

            return Ok(new
            {
                success = true,
                message = result.Message,
                products = result.Products
            });
        }

        public async Task<IActionResult> GetOne(Guid id)
        {
            var result = await _productService.GetOneAsync(id);
            if (!result.Success)
            {
                return NotFound(new
                {
                    success = false,
                    message = result.Message
                });
            }
            return Ok(new
            {
                success = true,
                message = result.Message,
                product = result.Product
            });
        }
    }
}
