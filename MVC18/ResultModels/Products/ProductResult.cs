using MVC18.DTOs.Products;
using MVC18.ResultModels;

namespace MVC18.ResultModels.Products
{
    public class ProductResult : BaseResult
    {
        public ProductDTO? Product { get; set; }
        public List<ProductDTO>? Products { get; set; }
    }
}
