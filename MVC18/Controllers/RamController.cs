using Microsoft.AspNetCore.Mvc;
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
    }
}
