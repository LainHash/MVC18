using MVC18.DTOs.Products.Create;
using MVC18.DTOs.Results.Products;

namespace MVC18.Services.Interfaces.Products
{
    public interface IGpuService
    {
        Task<GpuResult> GetOneAsync(Guid id);
        Task<GpuResult> CreateAsync(CreateGpuDTO dto);
    }
}
