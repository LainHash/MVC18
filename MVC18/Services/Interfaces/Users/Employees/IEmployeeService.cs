using MVC18.DTOs.Users.Update;
using MVC18.ResultModels.Users.Employees;

namespace MVC18.Services.Interfaces.Users.Employees
{
    public interface IEmployeeService
    {
        Task<EmployeeResult> GetOneAsync(Guid id);
        Task<EmployeeResult> UpdateProfileAsync(Guid id, UpdateProfileDTO dto);
        Task<EmployeeResult> ChangeEmailAsync(Guid id, ChangeEmailDTO dto);
        Task<EmployeeResult> ChangePasswordAsync(Guid id, ChangePasswordDTO dto);
    }
}
