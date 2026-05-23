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
    }
}
