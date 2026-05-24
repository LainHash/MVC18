using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MVC18.Data;
using MVC18.DTOs.Users.Customers;
using MVC18.DTOs.Users.Update;
using MVC18.ResultModels.Users.Customers;
using MVC18.Services.Interfaces.Users.Customers;

namespace MVC18.Services.Implementations.Users.Customers
{
    public class CustomerService : ICustomerService
    {
        private readonly LaptopWebDb06Context _context;
        private readonly IMapper _mapper;

        public CustomerService(LaptopWebDb06Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CustomerResult> GetOneAsync(Guid id)
        {
            var customer = await _context.VwpCustomerProfiles
                .FirstOrDefaultAsync(c => c.UserUuid == id);
            if(customer == null)
            {
                return new CustomerResult
                {
                    Success = false,
                    Message = "Khách hàng không tồn tại."
                };
            }
            return new CustomerResult
            {
                Success = true,
                Message = "Lấy thông tin khách hàng thành công.",
                Customer = _mapper.Map<CustomerDTO>(customer)
            };
        }

        public async Task<CustomerResult> UpdateProfileAsync(Guid id, UpdateProfileDTO dto)
        {
            var user = await _context.Users
                .Include(u => u.Customer)
                .ThenInclude(c => c.Pi)
                .FirstOrDefaultAsync(u => u.UserUuid == id);

            if (user == null || user.Customer == null || user.Customer.Pi == null)
            {
                return new CustomerResult { Success = false, Message = "Không tìm thấy thông tin khách hàng." };
            }

            if (user.Username != dto.Username && await _context.Users.AnyAsync(u => u.Username == dto.Username && u.UserUuid != id))
            {
                return new CustomerResult { Success = false, Message = "Tên đăng nhập đã tồn tại." };
            }

            user.Username = dto.Username;
            user.Customer.Pi.FirstName = dto.FirstName;
            user.Customer.Pi.LastName = dto.LastName;
            user.Customer.Pi.Gender = dto.Gender;
            user.Customer.Pi.Dob = dto.Dob;
            user.Customer.Pi.City = dto.City;
            user.Customer.Pi.Country = dto.Country;
            user.Customer.Pi.Address = dto.Address;
            user.Customer.Pi.Phone = dto.Phone;
            user.Customer.Pi.CitizenIdentityCard = dto.CitizenIdentityCard;
            user.Customer.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return new CustomerResult { Success = true, Message = "Cập nhật thông tin thành công." };
        }

        public async Task<CustomerResult> ChangeEmailAsync(Guid id, ChangeEmailDTO dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserUuid == id);
            if (user == null)
            {
                return new CustomerResult { Success = false, Message = "Không tìm thấy thông tin người dùng." };
            }

            if (user.Email != dto.OldEmail)
            {
                return new CustomerResult { Success = false, Message = "Email cũ không chính xác." };
            }

            if (await _context.Users.AnyAsync(u => u.Email == dto.NewEmail))
            {
                return new CustomerResult { Success = false, Message = "Email mới đã được sử dụng bởi tài khoản khác." };
            }

            user.Email = dto.NewEmail;
            await _context.SaveChangesAsync();

            return new CustomerResult { Success = true, Message = "Thay đổi email thành công." };
        }

        public async Task<CustomerResult> ChangePasswordAsync(Guid id, ChangePasswordDTO dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserUuid == id);
            if (user == null)
            {
                return new CustomerResult { Success = false, Message = "Không tìm thấy thông tin người dùng." };
            }

            if (!BCrypt.Net.BCrypt.Verify(dto.OldPassword, user.PasswordHash))
            {
                return new CustomerResult { Success = false, Message = "Mật khẩu cũ không chính xác." };
            }

            if (dto.NewPassword != dto.ConfirmNewPassword)
            {
                return new CustomerResult { Success = false, Message = "Mật khẩu xác nhận không khớp." };
            }

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
            await _context.SaveChangesAsync();

            return new CustomerResult { Success = true, Message = "Thay đổi mật khẩu thành công." };
        }
    }
}
