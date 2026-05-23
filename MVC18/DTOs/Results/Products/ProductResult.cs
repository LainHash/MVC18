using MVC18.DTOs.Products;

namespace MVC18.DTOs.Results.Products
{
    public class ProductResult : BaseResult
    {
        public ProductDTO? Product { get; set; }
        public List<ProductDTO>? Products { get; set; }
    }
}
