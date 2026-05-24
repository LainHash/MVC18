using Microsoft.AspNetCore.Mvc.Rendering;
using MVC18.DTOs.Products.Create;
using MVC18.DTOs.Products.Update;
using MVC18.ResultModels.Products;

namespace MVC18.Services.Interfaces.Products
{
    public interface ILaptopService
    {
        Task<LaptopResult> GetAllAsync();
        Task<LaptopResult> GetOneAsync(Guid id);
        Task<LaptopResult> CreateAsync(CreateLaptopDTO dto);
        Task<LaptopResult> UpdateAsync(Guid id, UpdateLaptopDTO dto);

        SelectList SelectLaptops();
    }
}
