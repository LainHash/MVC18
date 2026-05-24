using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using MVC18.Data;
using MVC18.DTOs.Auth;
using MVC18.Models;
using MVC18.ResultModels;
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
        private readonly IMemoryCache _cache;

        public AuthService(IConfiguration config, LaptopWebDb06Context context, IEmailService emailService, IMapper mapper, IMemoryCache cache)
        {
            _config = config;
            _context = context;
            _emailService = emailService;
            _mapper = mapper;
            _cache = cache;
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

            if (!user.IsActive)
            {
                throw new UnauthorizedAccessException("Tài khoản chưa được xác thực email. Vui lòng kiểm tra email của bạn.");
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

        public async Task<BaseResult> VerifyEmailAsync(VerifyEmailDTO dto)
        {
            var cacheKey = $"VerifyEmail_{dto.Email}";
            if (_cache.TryGetValue(cacheKey, out string? expectedCode))
            {
                if (expectedCode == dto.Code)
                {
                    var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
                    if (user != null)
                    {
                        user.IsActive = true;
                        await _context.SaveChangesAsync();
                        _cache.Remove(cacheKey); // Remove code after successful verification

                        return new BaseResult { Success = true, Message = "Xác thực email thành công." };
                    }
                }
                else
                {
                    return new BaseResult { Success = false, Message = "Mã xác nhận không chính xác." };
                }
            }

            return new BaseResult { Success = false, Message = "Mã xác nhận đã hết hạn hoặc không tồn tại." };
        }

        public async Task<RegisterResult> RegisterAsync(RegisterDTO dto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
                {
                    return new RegisterResult { Success = false, Message = "Email đã được sử dụng." };
                }

                if (await _context.Users.AnyAsync(u => u.Username == dto.Username))
                {
                    return new RegisterResult { Success = false, Message = "Tên đăng nhập đã tồn tại." };
                }

                var customerRole = await _context.Roles.FirstOrDefaultAsync(r => r.RoleName == "Customer");
                if (customerRole == null)
                {
                    return new RegisterResult { Success = false, Message = "Lỗi hệ thống: Không tìm thấy quyền Customer." };
                }

                var user = new User
                {
                    Username = dto.Username,
                    Email = dto.Email,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                    RoleId = customerRole.RoleId,
                    IsActive = false,
                    Balance = 0,
                    UserUuid = Guid.NewGuid()
                };
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                var pi = new PersonalInformation
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Gender = dto.Gender,
                    Dob = dto.Dob,
                    City = dto.City,
                    Country = dto.Country,
                    Address = dto.Address,
                    Phone = dto.Phone,
                    CitizenIdentityCard = dto.CitizenIdentityCard
                };
                _context.PersonalInformations.Add(pi);
                await _context.SaveChangesAsync();

                var customer = new Customer
                {
                    UserId = user.UserId,
                    Piid = pi.Piid,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    IsDeleted = false
                };
                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                // Generate 6-digit code
                Random random = new Random();
                string code = random.Next(100000, 999999).ToString();
                
                // Store in cache for 15 minutes
                _cache.Set($"VerifyEmail_{dto.Email}", code, TimeSpan.FromMinutes(15));

                // Send welcome email
                try
                {
                    string subject = "Xác thực email đăng ký tài khoản";
                    string body = $"<h2>Chào {dto.FirstName} {dto.LastName},</h2>" +
                                  $"<p>Tài khoản {dto.Username} của bạn đã được tạo thành công tại hệ thống của chúng tôi.</p>" +
                                  $"<p>Mã xác thực email của bạn là: <strong>{code}</strong></p>" +
                                  $"<p>Mã này sẽ hết hạn sau 15 phút.</p>" +
                                  $"<p>Cảm ơn bạn đã đồng hành cùng chúng tôi!</p>";
                    await _emailService.SendEmailAsync(dto.Email, subject, body);
                }
                catch (Exception)
                {
                    // Ignore email sending error
                }

                return new RegisterResult
                {
                    Success = true,
                    Message = "Đăng ký thành công. Vui lòng kiểm tra email để lấy mã xác thực."
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return new RegisterResult { Success = false, Message = $"Đăng ký thất bại: {ex.Message}" };
            }
        }
    }
}
