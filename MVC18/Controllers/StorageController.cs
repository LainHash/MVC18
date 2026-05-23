using Microsoft.AspNetCore.Mvc;
using MVC18.DTOs.Products.Create;
using MVC18.DTOs.Products.Update;
using MVC18.Services.Interfaces.Products;

namespace MVC18.Controllers
{
    public class StorageController : Controller
    {
        private readonly IStorageService _storageService;

        public StorageController(IStorageService storageService)
        {
            _storageService = storageService;
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var result = await _storageService.GetOneAsync(id);
            if (!result.Success)
            {
                return NotFound();
            }
            return View(result.Storage);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateStorageDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            var result = await _storageService.CreateAsync(dto);
            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, result.Message ?? "Tạo Storage thất bại.");
                return View(dto);
            }

            TempData["SuccessMessage"] = result.Message;
            return RedirectToAction(nameof(Details), new { id = result.Storage!.ProductUuid });
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var result = await _storageService.GetOneAsync(id);
            if (!result.Success)
                return NotFound();

            var dto = new UpdateStorageDTO
            {
                ProductName   = result.Storage!.ProductName,
                ImageUrl      = result.Storage.ImageUrl ?? string.Empty,
                Description   = result.Storage.Description,
                UnitPrice     = result.Storage.UnitPrice,
                UnitsInStock  = result.Storage.UnitsInStock,
                Capacity      = result.Storage.Capacity,
                MemoryType    = result.Storage.MemoryType,
                InterfaceType = result.Storage.InterfaceType,
                ReadSpeed     = result.Storage.ReadSpeed,
                WriteSpeed    = result.Storage.WriteSpeed
            };
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, UpdateStorageDTO dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var result = await _storageService.UpdateAsync(id, dto);
            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, result.Message ?? "Cập nhật Storage thất bại.");
                return View(dto);
            }

            TempData["SuccessMessage"] = result.Message;
            return RedirectToAction(nameof(Details), new { id });
        }
    }
}
