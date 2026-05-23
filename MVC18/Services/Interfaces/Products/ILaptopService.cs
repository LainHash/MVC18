using MVC18.DTOs.Products.Create;
using MVC18.DTOs.Products.Update;
using MVC18.DTOs.Results.Products;

namespace MVC18.Services.Interfaces.Products
{
    public interface ILaptopService
    {
        Task<LaptopResult> GetOneAsync(Guid id);
        Task<LaptopResult> CreateAsync(CreateLaptopDTO dto);
        Task<LaptopResult> UpdateAsync(Guid id, UpdateLaptopDTO dto);
    }
}
