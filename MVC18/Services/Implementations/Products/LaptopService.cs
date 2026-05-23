using MVC18.DTOs.Products.Create;
using MVC18.DTOs.Results.Products;
using MVC18.Services.Interfaces.Products;

namespace MVC18.Services.Implementations.Products
{
    public class LaptopService : ILaptopService
    {
        public Task<LaptopResult> CreateAsync(CreateLaptopDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task<LaptopResult> GetOneAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
