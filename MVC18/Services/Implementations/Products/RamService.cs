using MVC18.DTOs.Products.Create;
using MVC18.DTOs.Results.Products;
using MVC18.Services.Interfaces.Products;

namespace MVC18.Services.Implementations.Products
{
    public class RamService : IRamService
    {
        public Task<RamResult> CreateAsync(CreateRamDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task<RamResult> GetOneAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
