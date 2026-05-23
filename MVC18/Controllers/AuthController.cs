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

        public async Task<IActionResult> Login(LoginDTO dto)
        {
            var result = await _authService.LoginAsync(dto);
            if(!result.Success)
            {
                return BadRequest(new
                {
                    success = false,
                    message = result.Message
                });
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
