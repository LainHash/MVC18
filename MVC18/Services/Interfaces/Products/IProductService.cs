using MVC18.DTOs.Results.Products;

namespace MVC18.Services.Interfaces.Products
{
    public interface IProductService
    {
        Task<ProductResult> GetAllAsync();
        Task<ProductResult> GetOneAsync(Guid id);
        Task<ProductResult> DeleteAsync(Guid id);
    }
}
