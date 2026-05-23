using MVC18.DTOs.Products.Create;
using MVC18.DTOs.Results.Products;
using MVC18.Services.Interfaces.Products;

namespace MVC18.Services.Implementations.Products
{
    public class StorageService : IStorageService
    {
        public Task<StorageResult> CreateAsync(CreateStorageDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task<StorageResult> GetOneAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
