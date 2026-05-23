using Microsoft.AspNetCore.Mvc;
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
    }
}
