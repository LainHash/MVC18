using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC18.Data;
using MVC18.DTOs.Products;
using MVC18.DTOs.Products.Create;
using MVC18.DTOs.Products.Update;
using MVC18.Models;
using MVC18.ResultModels.Products;
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
                    CategoryId = dto.CategoryId,
                    SupplierId = dto.CompanyId,
                    ImageId = image.ImageId,
                    IsDeleted = false,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                var sku = new ProductSku
                {
                    ProductId = product.ProductId,
                    UnitPrice = dto.UnitPrice,
                    UnitsInStock = dto.UnitsInStock,
                    Discontinued = false,
                    IsDeleted = false
                };
                _context.ProductSkus.Add(sku);
                await _context.SaveChangesAsync();

                var gpu = new Gpu
                {
                    MemorySize = dto.MemorySize,
                    MemoryType = dto.MemoryType,
                    Clock = dto.Clock,
                    UnifiedShader = dto.UnifiedShader,
                    Tmu = dto.Tmu,
                    Rop = dto.Rop,
                    Bus = dto.Bus,
                    Igpu = dto.Igpu,
                    ProductSkuId = sku.ProductSkuId
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
                    Gpu = _mapper.Map<GpuDTO>(created)
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

        public async Task<GpuResult> GetAllAsync()
        {
            var gpus = await _context.VwdGpuDetails
                .ToListAsync();
            return new GpuResult
            {
                Success = true,
                Message = "Lấy danh sách GPU thành công.",
                Gpus = _mapper.Map<List<GpuDTO>>(gpus)
            };
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

        public async Task<GpuResult> GetUpdateAsync(Guid id)
        {
            var gpu = await _context.VwdGpuDetails
                .FirstOrDefaultAsync(g => g.ProductUuid == id);
            if (gpu == null)
            {
                return new GpuResult
                {
                    Success = false,
                    Message = "GPU không tồn tại."
                };
            }
            var updateDTO = _mapper.Map<UpdateGpuDTO>(gpu);
            return new GpuResult
            {
                Success = true,
                Message = "Lấy dữ liệu Gpu để cập nhật thành công.",
                GpuUpdate = updateDTO
            };
        }

        public SelectList SelectGpus()
        {
            var gpus = _context.VwdGpuDetails
                .Select(g => new
                {
                    g.GpuId,
                    g.ProductName
                })
                .ToList();
            var selectList = new SelectList(gpus, "GpuId", "ProductName");
            return selectList;
        }

        public async Task<GpuResult> UpdateAsync(Guid id, UpdateGpuDTO dto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var product = await _context.Products
                    .Include(p => p.Image)
                    .Include(p => p.ProductSku)
                        .ThenInclude(s => s!.Gpu)
                    .FirstOrDefaultAsync(p => p.ProductUuid == id && !p.IsDeleted);

                if (product == null)
                {
                    return new GpuResult
                    {
                        Success = false,
                        Message = "GPU không tồn tại."
                    };
                }


                var sku = product.ProductSku;
                if (sku == null)
                {
                    return new GpuResult
                    {
                        Success = false,
                        Message = "Không tìm thấy SKU của GPU."
                    };
                }

                var gpu = sku.Gpu;
                if (gpu == null)
                {
                    return new GpuResult
                    {
                        Success = false,
                        Message = "Không tìm thấy dữ liệu GPU."
                    };
                }

                product.Image.ImageUrl = dto.ImageUrl;

                product.ProductName = dto.ProductName;
                product.Description = dto.Description;
                product.CategoryId = dto.CategoryId;
                product.SupplierId = dto.CompanyId;
                product.UpdatedAt = DateTime.Now;

                sku.UnitPrice = dto.UnitPrice;
                sku.UnitsInStock = dto.UnitsInStock;

                gpu.MemorySize = dto.MemorySize;
                gpu.MemoryType = dto.MemoryType;
                gpu.Clock = dto.Clock;
                gpu.UnifiedShader = dto.UnifiedShader;
                gpu.Tmu = dto.Tmu;
                gpu.Rop = dto.Rop;
                gpu.Bus = dto.Bus;
                gpu.Igpu = dto.Igpu;

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                var updated = await _context.VwdGpuDetails
                    .FirstOrDefaultAsync(g => g.ProductUuid == id);

                return new GpuResult
                {
                    Success = true,
                    Message = "Cập nhật GPU thành công.",
                    Gpu = _mapper.Map<GpuDTO>(updated)
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return new GpuResult
                {
                    Success = false,
                    Message = $"Cập nhật GPU thất bại: {ex.Message}"
                };
            }
        }
    }
}
