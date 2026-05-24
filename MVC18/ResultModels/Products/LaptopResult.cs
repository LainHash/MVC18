using MVC18.DTOs.Products;
using MVC18.ResultModels;

namespace MVC18.ResultModels.Products
{
    public class LaptopResult : BaseResult
    {
        public LaptopDTO? Laptop { get; set; }
        public List<LaptopDTO>? Laptops { get; set; }
    }
}
