using AutoMapper;
using MVC18.DTOs.Users.Customers;
using MVC18.DTOs.Users.Employees;
using MVC18.Models;

namespace MVC18.Mappings
{
    public class UserMP : Profile
    {
        public UserMP()
        {
            CreateMap<VwpCustomerProfile, CustomerDTO>();
            CreateMap<VwpEmployeeProfile, EmployeeDTO>();
        }
    }
}
