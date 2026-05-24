using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MVC18.Data;
using MVC18.DTOs.Users.Employees;
using MVC18.DTOs.Users.Update;
using MVC18.ResultModels.Users.Employees;
using MVC18.Services.Interfaces.Users.Employees;

namespace MVC18.Services.Implementations.Users.Employees
{
    public class EmployeeService : IEmployeeService
    {
        private readonly LaptopWebDb06Context _context;
        private readonly IMapper _mapper;
        public EmployeeService(LaptopWebDb06Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<EmployeeResult> GetOneAsync(Guid id)
        {
            var employee = await _context.VwpEmployeeProfiles
                .FirstOrDefaultAsync(e => e.UserUuid == id);
            if (employee == null)
            {
                return new EmployeeResult
                {
                    Success = false,
                    Message = "Nhân viên không tồn tại."
                };
            }
            return new EmployeeResult
            {
                Success = true,
                Message = "Lấy thông tin nhân viên thành công.",
                Employee = _mapper.Map<EmployeeDTO>(employee)
            };
        }

        public async Task<EmployeeResult> UpdateProfileAsync(Guid id, UpdateProfileDTO dto)
        {
            var user = await _context.Users
                .Include(u => u.Employee)
                .ThenInclude(e => e.Pi)
                .FirstOrDefaultAsync(u => u.UserUuid == id);

            if (user == null || user.Employee == null || user.Employee.Pi == null)
            {
                return new EmployeeResult { Success = false, Message = "Không tìm thấy thông tin nhân viên." };
            }

            if (user.Username != dto.Username && await _context.Users.AnyAsync(u => u.Username == dto.Username && u.UserUuid != id))
            {
                return new EmployeeResult { Success = false, Message = "Tên đăng nhập đã tồn tại." };
            }

            user.Username = dto.Username;
            user.Employee.Pi.FirstName = dto.FirstName;
            user.Employee.Pi.LastName = dto.LastName;
            user.Employee.Pi.Gender = dto.Gender;
            user.Employee.Pi.Dob = dto.Dob;
            user.Employee.Pi.City = dto.City;
            user.Employee.Pi.Country = dto.Country;
            user.Employee.Pi.Address = dto.Address;
            user.Employee.Pi.Phone = dto.Phone;
            user.Employee.Pi.CitizenIdentityCard = dto.CitizenIdentityCard;
            user.Employee.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return new EmployeeResult { Success = true, Message = "Cập nhật thông tin thành công." };
        }

        public async Task<EmployeeResult> ChangeEmailAsync(Guid id, ChangeEmailDTO dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserUuid == id);
            if (user == null)
            {
                return new EmployeeResult { Success = false, Message = "Không tìm thấy thông tin người dùng." };
            }

            if (user.Email != dto.OldEmail)
            {
                return new EmployeeResult { Success = false, Message = "Email cũ không chính xác." };
            }

            if (await _context.Users.AnyAsync(u => u.Email == dto.NewEmail))
            {
                return new EmployeeResult { Success = false, Message = "Email mới đã được sử dụng bởi tài khoản khác." };
            }

            user.Email = dto.NewEmail;
            await _context.SaveChangesAsync();

            return new EmployeeResult { Success = true, Message = "Thay đổi email thành công." };
        }

        public async Task<EmployeeResult> ChangePasswordAsync(Guid id, ChangePasswordDTO dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserUuid == id);
            if (user == null)
            {
                return new EmployeeResult { Success = false, Message = "Không tìm thấy thông tin người dùng." };
            }

            if (!BCrypt.Net.BCrypt.Verify(dto.OldPassword, user.PasswordHash))
            {
                return new EmployeeResult { Success = false, Message = "Mật khẩu cũ không chính xác." };
            }

            if (dto.NewPassword != dto.ConfirmNewPassword)
            {
                return new EmployeeResult { Success = false, Message = "Mật khẩu xác nhận không khớp." };
            }

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
            await _context.SaveChangesAsync();

            return new EmployeeResult { Success = true, Message = "Thay đổi mật khẩu thành công." };
        }
    }
}
