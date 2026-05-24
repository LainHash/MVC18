using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using MVC18.Services.Interfaces.Users.Managers;

namespace MVC18.Controllers
{
    public class ManagerController : Controller
    {
        private readonly IManagerService _managerService;

        public ManagerController(IManagerService managerService)
        {
            _managerService = managerService;
        }

        public async Task<IActionResult> Products()
        {
            var result = await _managerService.GetAllProductsAsync();
            return View(result.Products ?? new List<MVC18.DTOs.Products.ProductDTO>());
        }

        public async Task<IActionResult> Customers()
        {
            var result = await _managerService.GetAllCustomersAsync();
            return View(result);
        }

        public async Task<IActionResult> Employees()
        {
            var result = await _managerService.GetAllEmployeesAsync();
            return View(result);
        }
    }
}
