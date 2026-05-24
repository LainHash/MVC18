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
    public class LaptopService : ILaptopService
    {
        private readonly LaptopWebDb06Context _context;
        private readonly IMapper _mapper;

        public LaptopService(LaptopWebDb06Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<LaptopResult> CreateAsync(CreateLaptopDTO dto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var cpuExists     = await _context.Cpus.AnyAsync(c => c.CpuId == dto.CpuId);
                var gpuExists     = await _context.Gpus.AnyAsync(g => g.GpuId == dto.GpuId);
                var ramExists     = await _context.Rams.AnyAsync(r => r.RamId == dto.RamId);
                var storageExists = await _context.Storages.AnyAsync(s => s.StorageId == dto.StorageId);

                if (!cpuExists)
                    return new LaptopResult { Success = false, Message = $"CPU với Id={dto.CpuId} không tồn tại." };
                if (!gpuExists)
                    return new LaptopResult { Success = false, Message = $"GPU với Id={dto.GpuId} không tồn tại." };
                if (!ramExists)
                    return new LaptopResult { Success = false, Message = $"RAM với Id={dto.RamId} không tồn tại." };
                if (!storageExists)
                    return new LaptopResult { Success = false, Message = $"Storage với Id={dto.StorageId} không tồn tại." };

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

                var laptopComponent = new LaptopComponent
                {
                    CpuId     = dto.CpuId,
                    GpuId     = dto.GpuId,
                    RamId     = dto.RamId,
                    StorageId = dto.StorageId
                };
                _context.LaptopComponents.Add(laptopComponent);
                await _context.SaveChangesAsync();

                var laptop = new Laptop
                {
                    LaptopType        = dto.LaptopType,
                    Os                = dto.Os,
                    ScreenResolution  = dto.ScreenResolution,
                    Length            = dto.Length,
                    Weight            = dto.Weight,
                    ProductSkuId      = sku.ProductSkuId,
                    LaptopComponentId = laptopComponent.LaptopComponentId
                };
                _context.Laptops.Add(laptop);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                var created = await _context.VwdLaptopDetails
                    .FirstOrDefaultAsync(l => l.ProductUuid == product.ProductUuid);

                return new LaptopResult
                {
                    Success = true,
                    Message = "Tạo Laptop thành công.",
                    Laptop  = _mapper.Map<LaptopDTO>(created)
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return new LaptopResult
                {
                    Success = false,
                    Message = $"Tạo Laptop thất bại: {ex.Message}"
                };
            }
        }

        public async Task<LaptopResult> GetAllAsync()
        {
            var laptops = await _context.VwdLaptopDetails
                .ToListAsync();
            return new LaptopResult
            {
                Success = true,
                Message = "Lấy danh sách Laptop thành công.",
                Laptops = _mapper.Map<List<LaptopDTO>>(laptops)
            };
        }

        public async Task<LaptopResult> GetOneAsync(Guid id)
        {
            var laptop = await _context.VwdLaptopDetails
                .FirstOrDefaultAsync(l => l.ProductUuid == id);
            if (laptop == null)
            {
                return new LaptopResult
                {
                    Success = false,
                    Message = "Laptop không tồn tại."
                };
            }

            return new LaptopResult
            {
                Success = true,
                Message = "Lấy chi tiết Laptop thành công.",
                Laptop = _mapper.Map<LaptopDTO>(laptop)
            };
        }

        public SelectList SelectLaptops()
        {
            var laptops = _context.VwdLaptopDetails
                .Select(l => new
                {
                    l.LaptopId,
                    l.ProductName
                })
                .ToList();
            var selectList = new SelectList(laptops, "LaptopId", "ProductName");
            return selectList;
        }

        public async Task<LaptopResult> UpdateAsync(Guid id, UpdateLaptopDTO dto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Kiểm tra các component tồn tại
                var cpuExists     = await _context.Cpus.AnyAsync(c => c.CpuId == dto.CpuId);
                var gpuExists     = await _context.Gpus.AnyAsync(g => g.GpuId == dto.GpuId);
                var ramExists     = await _context.Rams.AnyAsync(r => r.RamId == dto.RamId);
                var storageExists = await _context.Storages.AnyAsync(s => s.StorageId == dto.StorageId);

                if (!cpuExists)
                    return new LaptopResult { Success = false, Message = $"CPU với Id={dto.CpuId} không tồn tại." };
                if (!gpuExists)
                    return new LaptopResult { Success = false, Message = $"GPU với Id={dto.GpuId} không tồn tại." };
                if (!ramExists)
                    return new LaptopResult { Success = false, Message = $"RAM với Id={dto.RamId} không tồn tại." };
                if (!storageExists)
                    return new LaptopResult { Success = false, Message = $"Storage với Id={dto.StorageId} không tồn tại." };

                var product = await _context.Products
                    .Include(p => p.Image)
                    .Include(p => p.ProductSku)
                        .ThenInclude(s => s!.Laptop)
                            .ThenInclude(l => l!.LaptopComponent)
                    .FirstOrDefaultAsync(p => p.ProductUuid == id && !p.IsDeleted);

                if (product == null)
                    return new LaptopResult { Success = false, Message = "Laptop không tồn tại." };

                var sku = product.ProductSku;
                if (sku == null)
                    return new LaptopResult { Success = false, Message = "Không tìm thấy SKU của Laptop." };

                var laptop = sku.Laptop;
                if (laptop == null)
                    return new LaptopResult { Success = false, Message = "Không tìm thấy dữ liệu Laptop." };

                var laptopComponent = laptop.LaptopComponent;
                if (laptopComponent == null)
                    return new LaptopResult { Success = false, Message = "Không tìm thấy LaptopComponent." };

                // Cập nhật Image
                product.Image.ImageUrl = dto.ImageUrl;

                // Cập nhật Product (gán tay)
                product.ProductName = dto.ProductName;
                product.Description = dto.Description;
                product.CategoryId  = dto.CategoryId;
                product.SupplierId  = dto.CompanyId;
                product.UpdatedAt   = DateTime.Now;

                // Cập nhật ProductSku (gán tay)
                sku.UnitPrice    = dto.UnitPrice;
                sku.UnitsInStock = dto.UnitsInStock;

                // Cập nhật LaptopComponent (gán tay)
                laptopComponent.CpuId     = dto.CpuId;
                laptopComponent.GpuId     = dto.GpuId;
                laptopComponent.RamId     = dto.RamId;
                laptopComponent.StorageId = dto.StorageId;

                // Cập nhật Laptop (gán tay)
                laptop.LaptopType       = dto.LaptopType;
                laptop.Os               = dto.Os;
                laptop.ScreenResolution = dto.ScreenResolution;
                laptop.Length           = dto.Length;
                laptop.Weight           = dto.Weight;

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                var updated = await _context.VwdLaptopDetails
                    .FirstOrDefaultAsync(l => l.ProductUuid == id);

                return new LaptopResult
                {
                    Success = true,
                    Message = "Cập nhật Laptop thành công.",
                    Laptop  = _mapper.Map<LaptopDTO>(updated)
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return new LaptopResult
                {
                    Success = false,
                    Message = $"Cập nhật Laptop thất bại: {ex.Message}"
                };
            }
        }
    }
}
