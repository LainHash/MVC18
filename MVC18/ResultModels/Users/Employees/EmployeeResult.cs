using MVC18.DTOs.Users.Employees;

namespace MVC18.ResultModels.Users.Employees
{
    public class EmployeeResult : BaseResult
    {
        public EmployeeDTO? Employee { get; set; }
        public List<EmployeeDTO>? Employees { get; set; }
    }
}
