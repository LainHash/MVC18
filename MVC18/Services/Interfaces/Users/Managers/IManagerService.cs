using MVC18.ResultModels.Products;
using MVC18.ResultModels.Users.Customers;
using MVC18.ResultModels.Users.Employees;

namespace MVC18.Services.Interfaces.Users.Managers
{
    public interface IManagerService
    {
        Task<ProductResult> GetAllProductsAsync();
        Task<CustomerResult> GetAllCustomersAsync();
        Task<EmployeeResult> GetAllEmployeesAsync();
    }
}
