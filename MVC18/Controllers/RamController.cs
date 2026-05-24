using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC18.DTOs.Products.Create;
using MVC18.DTOs.Products.Update;
using MVC18.Services.Interfaces.Products;

namespace MVC18.Controllers
{
    public class RamController : Controller
    {
        private readonly IRamService _ramService;

        public RamController(IRamService ramService)
        {
            _ramService = ramService;
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var result = await _ramService.GetOneAsync(id);
            if (!result.Success)
            {
                return NotFound();
            }
            return View(result.Ram);
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
        public async Task<IActionResult> Create(CreateRamDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            var result = await _ramService.CreateAsync(dto);
            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, result.Message ?? "Tạo RAM thất bại.");
                return View(dto);
            }

            TempData["SuccessMessage"] = result.Message;
            return RedirectToAction(nameof(Details), new { id = result.Ram!.ProductUuid });
        }

        [Authorize(Policy = "Manager")]
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var result = await _ramService.GetUpdateAsync(id);
            if (!result.Success)
                return NotFound();

            return View(result.RamUpdate);
        }

        [Authorize(Policy = "Manager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, UpdateRamDTO dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var result = await _ramService.UpdateAsync(id, dto);
            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, result.Message ?? "Cập nhật RAM thất bại.");
                return View(dto);
            }

            TempData["SuccessMessage"] = result.Message;
            return RedirectToAction(nameof(Details), new { id });
        }
    }
}
