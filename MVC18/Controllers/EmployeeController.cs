using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC18.DTOs.Users.Update;
using MVC18.Services.Interfaces.Users.Employees;
using System.Security.Claims;

namespace MVC18.Controllers
{
    [Authorize(Policy = "Manager")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        private Guid GetUserId()
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (Guid.TryParse(userIdStr, out Guid userId))
            {
                return userId;
            }
            throw new UnauthorizedAccessException("Không tìm thấy thông tin người dùng.");
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Details()
        {
            var id = GetUserId();
            var result = await _employeeService.GetOneAsync(id);
            if (!result.Success)
            {
                return NotFound();
            }
            return View(result.Employee);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProfile()
        {
            var id = GetUserId();
            var result = await _employeeService.GetOneAsync(id);
            if (!result.Success || result.Employee == null)
            {
                return NotFound();
            }
            
            var dto = new UpdateProfileDTO
            {
                Username = result.Employee.Username,
                FirstName = result.Employee.FirstName,
                LastName = result.Employee.LastName,
                Gender = result.Employee.Gender,
                Dob = result.Employee.Dob,
                City = result.Employee.City,
                Country = result.Employee.Country,
                Address = result.Employee.Address,
                Phone = result.Employee.Phone,
                CitizenIdentityCard = result.Employee.CitizenIdentityCard
            };
            
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProfile(UpdateProfileDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            
            var id = GetUserId();
            var result = await _employeeService.UpdateProfileAsync(id, dto);
            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, result.Message ?? "Cập nhật thất bại.");
                return View(dto);
            }
            
            TempData["SuccessMessage"] = result.Message;
            return RedirectToAction(nameof(Details));
        }

        [HttpGet]
        public IActionResult ChangeEmail()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeEmail(ChangeEmailDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            var id = GetUserId();
            var result = await _employeeService.ChangeEmailAsync(id, dto);
            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, result.Message ?? "Thay đổi email thất bại.");
                return View(dto);
            }

            TempData["SuccessMessage"] = result.Message;
            return RedirectToAction(nameof(Details));
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            var id = GetUserId();
            var result = await _employeeService.ChangePasswordAsync(id, dto);
            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, result.Message ?? "Thay đổi mật khẩu thất bại.");
                return View(dto);
            }

            TempData["SuccessMessage"] = result.Message;
            return RedirectToAction(nameof(Details));
        }
    }
}
