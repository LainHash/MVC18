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
    public class GpuService : IGpuService
    {
        private readonly LaptopWebDb06Context _context;
        private readonly IMapper _mapper;

        public GpuService(LaptopWebDb06Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GpuResult> CreateAsync(CreateGpuDTO dto)
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

                var gpu = new Gpu
                {
                    MemorySize    = dto.MemorySize,
                    MemoryType    = dto.MemoryType,
                    Clock         = dto.Clock,
                    UnifiedShader = dto.UnifiedShader,
                    Tmu           = dto.Tmu,
                    Rop           = dto.Rop,
                    Bus           = dto.Bus,
                    Igpu          = dto.Igpu,
                    ProductSkuId  = sku.ProductSkuId
                };
                _context.Gpus.Add(gpu);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                var created = await _context.VwdGpuDetails
                    .FirstOrDefaultAsync(g => g.ProductUuid == product.ProductUuid);

                return new GpuResult
                {
                    Success = true,
                    Message = "Tạo GPU thành công.",
                    Gpu     = _mapper.Map<GpuDTO>(created)
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return new GpuResult
                {
                    Success = false,
                    Message = $"Tạo GPU thất bại: {ex.Message}"
                };
            }
        }

        public async Task<GpuResult> GetOneAsync(Guid id)
        {
            var gpu = await _context.VwdGpuDetails
                .FirstOrDefaultAsync(g => g.ProductUuid == id);
            if (gpu == null)
            {
                return new GpuResult
                {
                    Success = false,
                    Message = "Gpu không tồn tại."
                };
            }

            return new GpuResult
            {
                Success = true,
                Message = "Lấy chi tiết Gpu thành công.",
                Gpu = _mapper.Map<GpuDTO>(gpu)
            };
        }
    }
}
