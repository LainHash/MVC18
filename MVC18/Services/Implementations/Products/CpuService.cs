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
    public class CpuService : ICpuService
    {
        private readonly LaptopWebDb06Context _context;
        private readonly IMapper _mapper;

        public CpuService(LaptopWebDb06Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CpuResult> CreateAsync(CreateCpuDTO dto)
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

                var cpu = new Cpu
                {
                    Cores = dto.Cores,
                    Logicals = dto.Logicals,
                    Tdp = dto.Tdp,
                    Socket = dto.Socket,
                    Speed = dto.Speed,
                    Turbo = dto.Turbo,
                    ProductSkuId = sku.ProductSkuId
                };
                _context.Cpus.Add(cpu);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                var created = await _context.VwdCpuDetails
                    .FirstOrDefaultAsync(c => c.ProductUuid == product.ProductUuid);

                return new CpuResult
                {
                    Success = true,
                    Message = "Tạo CPU thành công.",
                    Cpu = _mapper.Map<CpuDTO>(created)
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return new CpuResult
                {
                    Success = false,
                    Message = $"Tạo CPU thất bại: {ex.Message}"
                };
            }
        }

        public async Task<CpuResult> GetAllAsync()
        {
            var cpus = await _context.VwdCpuDetails
                .ToListAsync();
            return new CpuResult
            {
                Success = true,
                Message = "Lấy danh sách CPU thành công.",
                Cpus = _mapper.Map<List<CpuDTO>>(cpus)
            };
        }

        public async Task<CpuResult> GetOneAsync(Guid id)
        {
            var cpu = await _context.VwdCpuDetails
                .FirstOrDefaultAsync(c => c.ProductUuid == id);
            if (cpu == null)
            {
                return new CpuResult
                {
                    Success = false,
                    Message = "CPU không tồn tại."
                };
            }
            return new CpuResult
            {
                Success = true,
                Message = "Lấy chi tiết CPU thành công.",
                Cpu = _mapper.Map<CpuDTO>(cpu)
            };
        }

        public async Task<CpuResult> GetUpdateAsync(Guid id)
        {
            var cpu = await _context.VwdCpuDetails
                .FirstOrDefaultAsync(c => c.ProductUuid == id);
            if (cpu == null)
            {
                return new CpuResult
                {
                    Success = false,
                    Message = "CPU không tồn tại."
                };
            }
            var updateDTO = _mapper.Map<UpdateCpuDTO>(cpu);
            return new CpuResult
            {
                Success = true,
                Message = "Lấy dữ liệu CPU để cập nhật thành công.",
                CpuUpdate = updateDTO
            };
        }

        public SelectList SelectCpus()
        {
            var cpus = _context.VwdCpuDetails
                .Select(c => new
                {
                    c.CpuId,
                    c.ProductName
                })
                .ToList();
            var selectList = new SelectList(cpus, "CpuId", "ProductName");
            return selectList;
        }

        public async Task<CpuResult> UpdateAsync(Guid id, UpdateCpuDTO dto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var product = await _context.Products
                    .Include(p => p.Image)
                    .Include(p => p.ProductSku)
                        .ThenInclude(s => s!.Cpu)
                    .FirstOrDefaultAsync(p => p.ProductUuid == id && !p.IsDeleted);

                if (product == null)
                {
                    return new CpuResult
                    {
                        Success = false,
                        Message = "CPU không tồn tại."
                    };
                }

                var sku = product.ProductSku;
                if (sku == null)
                {
                    return new CpuResult
                    {
                        Success = false,
                        Message = "Không tìm thấy SKU của CPU."
                    };
                }

                var cpu = sku.Cpu;
                if (cpu == null)
                {
                    return new CpuResult
                    {
                        Success = false,
                        Message = "Không tìm thấy dữ liệu CPU."
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

                cpu.Cores = dto.Cores;
                cpu.Logicals = dto.Logicals;
                cpu.Tdp = dto.Tdp;
                cpu.Socket = dto.Socket;
                cpu.Speed = dto.Speed;
                cpu.Turbo = dto.Turbo;

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                var updated = await _context.VwdCpuDetails
                    .FirstOrDefaultAsync(c => c.ProductUuid == id);

                return new CpuResult
                {
                    Success = true,
                    Message = "Cập nhật CPU thành công.",
                    Cpu = _mapper.Map<CpuDTO>(updated)
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return new CpuResult
                {
                    Success = false,
                    Message = $"Cập nhật CPU thất bại: {ex.Message}"
                };
            }
        }
    }
}
