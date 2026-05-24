using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC18.DTOs.Products.Create;
using MVC18.DTOs.Products.Update;
using MVC18.Services.Interfaces.Products;

namespace MVC18.Controllers
{
    public class CpuController : Controller
    {
        private readonly ICpuService _cpuService;

        public CpuController(ICpuService cpuService)
        {
            _cpuService = cpuService;
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var result = await _cpuService.GetOneAsync(id);
            if (!result.Success)
            {
                return NotFound();
            }
            return View(result.Cpu);
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
        public async Task<IActionResult> Create(CreateCpuDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            var result = await _cpuService.CreateAsync(dto);
            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, result.Message ?? "Tạo CPU thất bại.");
                return View(dto);
            }

            TempData["SuccessMessage"] = result.Message;
            return RedirectToAction(nameof(Details), new { id = result.Cpu!.ProductUuid });
        }

        [Authorize(Policy = "Manager")]
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var result = await _cpuService.GetOneAsync(id);
            if (!result.Success)
                return NotFound();

            var dto = _cpuService.GetUpdateAsync(result.Cpu!);
            return View(dto);
        }

        [Authorize(Policy = "Manager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, UpdateCpuDTO dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var result = await _cpuService.UpdateAsync(id, dto);
            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, result.Message ?? "Cập nhật CPU thất bại.");
                return View(dto);
            }

            TempData["SuccessMessage"] = result.Message;
            return RedirectToAction(nameof(Details), new { id });
        }
    }
}
