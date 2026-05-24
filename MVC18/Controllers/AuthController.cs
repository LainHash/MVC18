using Microsoft.AspNetCore.Mvc;
using MVC18.DTOs.Auth;
using MVC18.Services.Interfaces.Auth;

namespace MVC18.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Details(string role)
        {
            switch (role)
            {
                case "Customer":
                    return RedirectToAction("Details", "Customer");
                case "Employee":
                    return RedirectToAction("Details", "Employee");
                default:
                    return NotFound();
            }
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            try
            {
                var result = await _authService.LoginAsync(dto);
                if (!result.Success)
                {
                    ModelState.AddModelError(string.Empty, result.Message ?? "Đăng nhập thất bại.");
                    return View(dto);
                }

                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.UtcNow.AddMinutes(120)
                };
                Response.Cookies.Append("jwt", result.Token, cookieOptions);

                TempData["SuccessMessage"] = result.Message;
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(dto);
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterDTO dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var result = await _authService.RegisterAsync(dto);
            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, result.Message ?? "Đăng ký thất bại.");
                return View(dto);
            }

            TempData["SuccessMessage"] = result.Message;
            return RedirectToAction(nameof(VerifyEmail), new { email = dto.Email });
        }

        [HttpGet]
        public IActionResult VerifyEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction(nameof(Login));
            }

            var dto = new VerifyEmailDTO { Email = email };
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VerifyEmail(VerifyEmailDTO dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var result = await _authService.VerifyEmailAsync(dto);
            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, result.Message ?? "Xác thực thất bại.");
                return View(dto);
            }

            TempData["SuccessMessage"] = result.Message;
            return RedirectToAction(nameof(Login));
        }

        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");
            TempData["SuccessMessage"] = "Đăng xuất thành công.";
            return RedirectToAction("Index", "Home");
        }
    }
}
