using MVC18.DTOs.Users.Update;
using MVC18.ResultModels.Users.Customers;

namespace MVC18.Services.Interfaces.Users.Customers
{
    public interface ICustomerService
    {
        Task<CustomerResult> GetOneAsync(Guid id);
        Task<CustomerResult> UpdateProfileAsync(Guid id, UpdateProfileDTO dto);
        Task<CustomerResult> ChangeEmailAsync(Guid id, ChangeEmailDTO dto);
        Task<CustomerResult> ChangePasswordAsync(Guid id, ChangePasswordDTO dto);
    }
}
