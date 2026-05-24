using Microsoft.AspNetCore.Mvc.Rendering;
using MVC18.DTOs.Products;
using MVC18.DTOs.Products.Create;
using MVC18.DTOs.Products.Update;
using MVC18.ResultModels.Products;

namespace MVC18.Services.Interfaces.Products
{
    public interface ICpuService
    {
        Task<CpuResult> GetAllAsync();
        Task<CpuResult> GetOneAsync(Guid id);
        Task<CpuResult> CreateAsync(CreateCpuDTO dto);

        Task<CpuResult> GetUpdateAsync(Guid id);
        Task<CpuResult> UpdateAsync(Guid id, UpdateCpuDTO dto);

        SelectList SelectCpus();
    }
}
