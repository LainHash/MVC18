using MVC18.DTOs.Results.Products;

namespace MVC18.Services.Interfaces.Products
{
    public interface IProductService
    {
        Task<ProductResult> GetAllAsync();
        Task<ProductResult> GetOneAsync(Guid id);
        Task<ProductResult> CreateAsync();
        Task<ProductResult> UpdateAsync();
        Task<ProductResult> DeleteAsync(Guid id);

        Task<LaptopResult> GetLaptopDetailsAsync(Guid id);
        Task<CpuResult> GetCpuDetailsAsync(Guid id);
        Task<GpuResult> GetGpuDetailsAsync(Guid id);
        Task<RamResult> GetRamDetailsAsync(Guid id);
        Task<StorageResult> GetStorageDetailsAsync(Guid id);
    }
}
