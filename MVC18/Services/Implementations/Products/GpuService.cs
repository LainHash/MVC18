using MVC18.DTOs.Products.Create;
using MVC18.DTOs.Results.Products;
using MVC18.Services.Interfaces.Products;

namespace MVC18.Services.Implementations.Products
{
    public class GpuService : IGpuService
    {
        public Task<GpuResult> CreateAsync(CreateGpuDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task<GpuResult> GetOneAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
