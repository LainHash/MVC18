using Microsoft.AspNetCore.Mvc;
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
    }
}
