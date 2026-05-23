using MVC18.DTOs.Products.Create;
using MVC18.DTOs.Products.Update;
using MVC18.ResultModels.Products;

namespace MVC18.Services.Interfaces.Products
{
    public interface IStorageService
    {
        Task<StorageResult> GetOneAsync(Guid id);
        Task<StorageResult> CreateAsync(CreateStorageDTO dto);
        Task<StorageResult> UpdateAsync(Guid id, UpdateStorageDTO dto);
    }
}
