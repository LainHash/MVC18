using Microsoft.AspNetCore.Mvc;
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
    }
}
