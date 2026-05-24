using Microsoft.AspNetCore.Mvc.Rendering;
using MVC18.DTOs.Products;
using MVC18.DTOs.Products.Create;
using MVC18.DTOs.Products.Update;
using MVC18.ResultModels.Products;

namespace MVC18.Services.Interfaces.Products
{
    public interface IRamService
    {
        Task<RamResult> GetAllAsync();
        Task<RamResult> GetOneAsync(Guid id);
        Task<RamResult> CreateAsync(CreateRamDTO dto);

        RamResult GetUpdateAsync(RamDTO dto);
        Task<RamResult> UpdateAsync(Guid id, UpdateRamDTO dto);

        SelectList SelectRams();
    }
}
