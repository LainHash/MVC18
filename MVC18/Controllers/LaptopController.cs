using Microsoft.AspNetCore.Mvc;
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
    }
}
