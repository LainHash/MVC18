using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MVC18.Data;
using MVC18.DTOs.Products;
using MVC18.DTOs.Products.Create;
using MVC18.DTOs.Results.Products;
using MVC18.Models;
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

        public async Task<StorageResult> CreateAsync(CreateStorageDTO dto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var image = new Image
                {
                    ImageUrl = dto.ImageUrl
                };
                _context.Images.Add(image);
                await _context.SaveChangesAsync();

                var product = new Product
                {
                    ProductName = dto.ProductName,
                    Description = dto.Description,
                    CategoryId  = dto.CategoryId,
                    SupplierId  = dto.CompanyId,
                    ImageId     = image.ImageId,
                    IsDeleted   = false,
                    CreatedAt   = DateTime.Now,
                    UpdatedAt   = DateTime.Now
                };
                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                var sku = new ProductSku
                {
                    ProductId    = product.ProductId,
                    UnitPrice    = dto.UnitPrice,
                    UnitsInStock = dto.UnitsInStock,
                    Discontinued = false,
                    IsDeleted    = false
                };
                _context.ProductSkus.Add(sku);
                await _context.SaveChangesAsync();

                var storage = new Storage
                {
                    Capacity      = dto.Capacity,
                    MemoryType    = dto.MemoryType,
                    InterfaceType = dto.InterfaceType,
                    ReadSpeed     = dto.ReadSpeed,
                    WriteSpeed    = dto.WriteSpeed,
                    ProductSkuId  = sku.ProductSkuId
                };
                _context.Storages.Add(storage);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                var created = await _context.VwdStorageDetails
                    .FirstOrDefaultAsync(s => s.ProductUuid == product.ProductUuid);

                return new StorageResult
                {
                    Success = true,
                    Message = "Tạo Storage thành công.",
                    Storage = _mapper.Map<StorageDTO>(created)
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return new StorageResult
                {
                    Success = false,
                    Message = $"Tạo Storage thất bại: {ex.Message}"
                };
            }
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
