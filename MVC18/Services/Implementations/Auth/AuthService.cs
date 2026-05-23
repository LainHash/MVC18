using AutoMapper;
using Azure;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MVC18.Data;
using MVC18.DTOs.Auth;
using MVC18.Models;
using MVC18.ResultModels.Auth;
using MVC18.Services.Interfaces.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MVC18.Services.Implementations.Auth
{
    public class AuthService : IAuthService
    {
        private readonly LaptopWebDb06Context _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly IEmailService _emailService;
        public AuthService(IConfiguration config, LaptopWebDb06Context context, IEmailService emailService, IMapper mapper)
        {
            _config = config;
            _context = context;
            _emailService = emailService;
            _mapper = mapper;
        }
        public string GenerateJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role.RoleName)
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<LoginResult> LoginAsync(LoginDTO dto)
        {
            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            {
                throw new UnauthorizedAccessException("Sai mật khẩu hoặc email.");
            }
            var token = GenerateJwtToken(user);

            return new LoginResult
            {
                Success = true,
                Message = "Đăng nhập thành công.",
                Email = user.Email,
                Username = user.Username,
                Role = user.Role.RoleName,
                Token = token
            };
        }

        public void Logout()
        {
            
        }

        public void RefeshToken()
        {
            
        }

        public Task<RegisterResult> RegisterAsync(RegisterDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
