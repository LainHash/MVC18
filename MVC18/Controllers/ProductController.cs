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

            switch (result.Product.CategoryName)
            {
                case CategoryConstants.Laptop:
                    var laptopDetails = await _productService.GetLaptopDetailsAsync(id);
                    return RedirectToAction("LaptopDetails", new { id });
                case CategoryConstants.Cpu:
                    var cpuDetails = await _productService.GetCpuDetailsAsync(id);
                    return RedirectToAction("CpuDetails", new { id });
                case CategoryConstants.Gpu:
                    var gpuDetails = await _productService.GetGpuDetailsAsync(id);
                    return RedirectToAction("GpuDetails", new { id });
                case CategoryConstants.Ram:
                    var ramDetails = await _productService.GetRamDetailsAsync(id);
                    return RedirectToAction("RamDetails", new { id });
                case CategoryConstants.Storage:
                    var storageDetails = await _productService.GetStorageDetailsAsync(id);
                    return RedirectToAction("StorageDetails", new { id });
            }

            return View(result.Product);
        }

        public async Task<IActionResult> LaptopDetails(Guid id)
        {
            var result = await _productService.GetLaptopDetailsAsync(id);
            if (!result.Success)
            {
                return NotFound();
            }
            return View(result.Laptop);
        }

        public async Task<IActionResult> CpuDetails(Guid id)
        {
            var result = await _productService.GetCpuDetailsAsync(id);
            if (!result.Success)
            {
                return NotFound();
            }
            return View(result.Cpu);
        }

        public async Task<IActionResult> GpuDetails(Guid id)
        {
            var result = await _productService.GetGpuDetailsAsync(id);
            if (!result.Success)
            {
                return NotFound();
            }
            return View(result.Gpu);
        }

        public async Task<IActionResult> RamDetails(Guid id)
        {
            var result = await _productService.GetRamDetailsAsync(id);
            if (!result.Success)
            {
                return NotFound();
            }
            return View(result.Ram);
        }

        public async Task<IActionResult> StorageDetails(Guid id)
        {
            var result = await _productService.GetStorageDetailsAsync(id);
            if (!result.Success)
            {
                return NotFound();
            }
            return View(result.Storage);
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
