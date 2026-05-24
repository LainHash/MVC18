using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC18.DTOs.Misc;
using MVC18.Helpers.Constants.Misc;
using MVC18.Services.Interfaces.Products;
using System.Diagnostics;

namespace MVC18.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index([FromQuery] ProductQuery query)
        {
            var result = await _productService.GetAllAsync(query);

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_ProductGrid", result);
            }

            return View(result);
        }

        [Authorize(Policy = "Manager")]
        public async Task<IActionResult> Create(string category)
        {
            switch (category)
            {
                case CategoryConstants.Laptop:
                    return RedirectToAction("Create", "Laptop");
                case CategoryConstants.Cpu:
                    return RedirectToAction("Create", "Cpu");
                case CategoryConstants.Gpu:
                    return RedirectToAction("Create", "Gpu");
                case CategoryConstants.Ram:
                    return RedirectToAction("Create", "Ram");
                case CategoryConstants.Storage:
                    return RedirectToAction("Create", "Storage");
            }
            return NotFound();
        }

        public async Task<IActionResult> Details(string category, Guid id)
        {

            switch (category)
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

            return NotFound();
        }

        [Authorize(Policy = "Manager")]
        public async Task<IActionResult> Edit(string category, Guid id)
        {
            switch (category)
            {
                case CategoryConstants.Laptop:
                    return RedirectToAction("Edit", "Laptop", new { id });
                case CategoryConstants.Cpu:
                    return RedirectToAction("Edit", "Cpu", new { id });
                case CategoryConstants.Gpu:
                    return RedirectToAction("Edit", "Gpu", new { id });
                case CategoryConstants.Ram:
                    return RedirectToAction("Edit", "Ram", new { id });
                case CategoryConstants.Storage:
                    return RedirectToAction("Edit", "Storage", new { id });
            }
            return NotFound();
        }

        [Authorize(Policy = "Manager")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _productService.DeleteAsync(id);
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
                message = result.Message
            });
        }

    }
}
