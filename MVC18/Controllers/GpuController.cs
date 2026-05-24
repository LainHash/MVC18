using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC18.DTOs.Products.Create;
using MVC18.DTOs.Products.Update;
using MVC18.Services.Interfaces.Products;

namespace MVC18.Controllers
{
    public class GpuController : Controller
    {
        private readonly IGpuService _gpuService;

        public GpuController(IGpuService gpuService)
        {
            _gpuService = gpuService;
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var result = await _gpuService.GetOneAsync(id);
            if (!result.Success)
            {
                return NotFound();
            }
            return View(result.Gpu);
        }

        [Authorize(Policy = "Manager")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Policy = "Manager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateGpuDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            var result = await _gpuService.CreateAsync(dto);
            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, result.Message ?? "Tạo GPU thất bại.");
                return View(dto);
            }

            TempData["SuccessMessage"] = result.Message;
            return RedirectToAction(nameof(Details), new { id = result.Gpu!.ProductUuid });
        }

        [Authorize(Policy = "Manager")]
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var result = await _gpuService.GetOneAsync(id);
            if (!result.Success)
                return NotFound();

            var dto = _gpuService.GetUpdateAsync(result.Gpu!);
            return View(dto);
        }

        [Authorize(Policy = "Manager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, UpdateGpuDTO dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var result = await _gpuService.UpdateAsync(id, dto);
            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, result.Message ?? "Cập nhật GPU thất bại.");
                return View(dto);
            }

            TempData["SuccessMessage"] = result.Message;
            return RedirectToAction(nameof(Details), new { id });
        }
    }
}
