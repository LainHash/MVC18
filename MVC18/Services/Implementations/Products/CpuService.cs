using MVC18.DTOs.Products.Create;
using MVC18.DTOs.Results.Products;
using MVC18.Services.Interfaces.Products;

namespace MVC18.Services.Implementations.Products
{
    public class CpuService : ICpuService
    {
        public Task<CpuResult> CreateAsync(CreateCpuDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task<CpuResult> GetOneAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
