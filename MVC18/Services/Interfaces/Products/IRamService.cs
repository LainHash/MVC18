using MVC18.DTOs.Products.Create;
using MVC18.DTOs.Results.Products;

namespace MVC18.Services.Interfaces.Products
{
    public interface IRamService
    {
        Task<RamResult> GetOneAsync(Guid id);
        Task<RamResult> CreateAsync(CreateRamDTO dto);
    }
}
