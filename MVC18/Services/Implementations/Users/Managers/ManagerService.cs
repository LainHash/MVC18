using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MVC18.Data;
using MVC18.DTOs.Products;
using MVC18.DTOs.Users.Customers;
using MVC18.DTOs.Users.Employees;
using MVC18.ResultModels.Products;
using MVC18.ResultModels.Users.Customers;
using MVC18.ResultModels.Users.Employees;
using MVC18.Services.Interfaces.Users.Managers;

namespace MVC18.Services.Implementations.Users.Managers
{
    public class ManagerService : IManagerService
    {
        private readonly LaptopWebDb06Context _context;
        private readonly IMapper _mapper;

        public ManagerService(LaptopWebDb06Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CustomerResult> GetAllCustomersAsync()
        {
            var customer = await _context.VwpCustomerProfiles.ToListAsync();
            return new CustomerResult
            {
                Success = true,
                Message = "Lấy danh sách khách hàng thành công.",
                Customers = _mapper.Map<List<CustomerDTO>>(customer)
            };
        }

        public async Task<EmployeeResult> GetAllEmployeesAsync()
        {
            var employee = await _context.VwpEmployeeProfiles.ToListAsync();
            return new EmployeeResult
            {
                Success = true,
                Message = "Lấy danh sách nhân viên thành công.",
                Employees = _mapper.Map<List<EmployeeDTO>>(employee)
            };
        }

        public async Task<ProductResult> GetAllProductsAsync()
        {
            var products = await _context.VwProducts.ToListAsync();
            return new ProductResult
            {
                Success = true,
                Message = "Lấy danh sách sản phẩm thành công.",
                Products = _mapper.Map<List<ProductDTO>>(products)
            };
        }
    }
}
