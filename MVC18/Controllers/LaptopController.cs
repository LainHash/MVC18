using Microsoft.AspNetCore.Mvc;
using MVC18.DTOs.Products.Create;
using MVC18.DTOs.Products.Update;
using MVC18.Services.Interfaces.Products;

namespace MVC18.Controllers
{
    public class LaptopController : Controller
    {
        private readonly ILaptopService _laptopService;

        public LaptopController(ILaptopService laptopService)
        {
            _laptopService = laptopService;
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var result = await _laptopService.GetOneAsync(id);
            if (!result.Success)
            {
                return NotFound();
            }
            return View(result.Laptop);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateLaptopDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            var result = await _laptopService.CreateAsync(dto);
            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, result.Message ?? "Tạo Laptop thất bại.");
                return View(dto);
            }

            TempData["SuccessMessage"] = result.Message;
            return RedirectToAction(nameof(Details), new { id = result.Laptop!.ProductUuid });
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var result = await _laptopService.GetOneAsync(id);
            if (!result.Success)
                return NotFound();

            var l = result.Laptop!;
            var dto = new UpdateLaptopDTO
            {
                ProductName      = l.ProductName,
                ImageUrl         = l.ImageUrl ?? string.Empty,
                Description      = l.Description,
                UnitPrice        = l.UnitPrice,
                UnitsInStock     = l.UnitsInStock,
                LaptopType       = l.LaptopType,
                Os               = l.Os,
                ScreenResolution = l.ScreenResolution,
                Length           = l.Length,
                Weight           = l.Weight
            };
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, UpdateLaptopDTO dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var result = await _laptopService.UpdateAsync(id, dto);
            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, result.Message ?? "Cập nhật Laptop thất bại.");
                return View(dto);
            }

            TempData["SuccessMessage"] = result.Message;
            return RedirectToAction(nameof(Details), new { id });
        }
    }
}
