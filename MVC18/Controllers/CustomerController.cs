using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC18.DTOs.Users.Update;
using MVC18.Services.Interfaces.Users.Customers;
using System.Security.Claims;

namespace MVC18.Controllers
{
    [Authorize(Policy = "Customer")]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
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
            var result = await _customerService.GetOneAsync(id);
            if (!result.Success)
            {
                return NotFound();
            }
            return View(result.Customer);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProfile()
        {
            var id = GetUserId();
            var result = await _customerService.GetOneAsync(id);
            if (!result.Success || result.Customer == null)
            {
                return NotFound();
            }
            
            var dto = new UpdateProfileDTO
            {
                Username = result.Customer.Username,
                FirstName = result.Customer.FirstName,
                LastName = result.Customer.LastName,
                Gender = result.Customer.Gender,
                Dob = result.Customer.Dob,
                City = result.Customer.City,
                Country = result.Customer.Country,
                Address = result.Customer.Address,
                Phone = result.Customer.Phone,
                CitizenIdentityCard = result.Customer.CitizenIdentityCard
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
            var result = await _customerService.UpdateProfileAsync(id, dto);
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
            var result = await _customerService.ChangeEmailAsync(id, dto);
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
            var result = await _customerService.ChangePasswordAsync(id, dto);
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
