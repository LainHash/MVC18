using MVC18.DTOs.Users.Customers;

namespace MVC18.ResultModels.Users.Customers
{
    public class CustomerResult : BaseResult
    {
        public CustomerDTO? Customer { get; set; }
        public List<CustomerDTO>? Customers { get; set; }
    }
}
