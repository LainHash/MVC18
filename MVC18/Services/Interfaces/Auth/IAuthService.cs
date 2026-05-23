using MVC18.DTOs.Auth;
using MVC18.Models;
using MVC18.ResultModels.Auth;

namespace MVC18.Services.Interfaces.Auth
{
    public interface IAuthService
    {
        Task<LoginResult> LoginAsync(LoginDTO dto);
        Task<RegisterResult> RegisterAsync(RegisterDTO dto);
        string GenerateJwtToken(User user);
        void Logout();
        void RefeshToken();
    }
}
