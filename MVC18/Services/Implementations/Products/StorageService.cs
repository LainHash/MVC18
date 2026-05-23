using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MVC18.Data;
using MVC18.DTOs.Products;
using MVC18.DTOs.Products.Create;
using MVC18.DTOs.Results.Products;
using MVC18.Services.Interfaces.Products;

namespace MVC18.Services.Implementations.Products
{
    public class StorageService : IStorageService
    {
        private readonly LaptopWebDb06Context _context;
        private readonly IMapper _mapper;

        public StorageService(LaptopWebDb06Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<StorageResult> CreateAsync(CreateStorageDTO dto)
        {
            throw new NotImplementedException();
        }

        public async Task<StorageResult> GetOneAsync(Guid id)
        {
            var storage = await _context.VwdStorageDetails
                .FirstOrDefaultAsync(s => s.ProductUuid == id);
            if (storage == null)
            {
                return new StorageResult
                {
                    Success = false,
                    Message = "Storage không tồn tại."
                };
            }

            return new StorageResult
            {
                Success = true,
                Message = "Lấy chi tiết Storage thành công.",
                Storage = _mapper.Map<StorageDTO>(storage)
            };
        }
    }
}
