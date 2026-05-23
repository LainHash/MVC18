using MVC18.DTOs.Misc;
using MVC18.DTOs.Products;
using MVC18.ResultModels.Misc;
using MVC18.ResultModels.Products;

namespace MVC18.Services.Interfaces.Products
{
    public interface IProductService
    {
        Task<ProductResult> GetAllAsync();
        Task<PagedResult<ProductDTO>> GetAllAsync(ProductQuery query);
        Task<ProductResult> GetOneAsync(Guid id);
        Task<ProductResult> DeleteAsync(Guid id);
    }
}
