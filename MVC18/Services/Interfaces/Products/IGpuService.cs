using Microsoft.AspNetCore.Mvc.Rendering;
using MVC18.DTOs.Products.Create;
using MVC18.DTOs.Products.Update;
using MVC18.ResultModels.Products;

namespace MVC18.Services.Interfaces.Products
{
    public interface IGpuService
    {
        Task<GpuResult> GetAllAsync();
        Task<GpuResult> GetOneAsync(Guid id);
        Task<GpuResult> CreateAsync(CreateGpuDTO dto);
        Task<GpuResult> UpdateAsync(Guid id, UpdateGpuDTO dto);

        SelectList SelectGpus();
    }
}
