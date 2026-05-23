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
    public class RamService : IRamService
    {
        private readonly LaptopWebDb06Context _context;
        private readonly IMapper _mapper;

        public RamService(LaptopWebDb06Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<RamResult> CreateAsync(CreateRamDTO dto)
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

                var ram = new Ram
                {
                    Capacity     = dto.Capacity,
                    Gen          = dto.Gen,
                    Speed        = dto.Speed,
                    Kit          = dto.Kit,
                    ProductSkuId = sku.ProductSkuId
                };
                _context.Rams.Add(ram);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                var created = await _context.VwdRamDetails
                    .FirstOrDefaultAsync(r => r.ProductUuid == product.ProductUuid);

                return new RamResult
                {
                    Success = true,
                    Message = "Tạo RAM thành công.",
                    Ram     = _mapper.Map<RamDTO>(created)
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return new RamResult
                {
                    Success = false,
                    Message = $"Tạo RAM thất bại: {ex.Message}"
                };
            }
        }

        public async Task<RamResult> GetOneAsync(Guid id)
        {
            var ram = await _context.VwdRamDetails
                .FirstOrDefaultAsync(r => r.ProductUuid == id);
            if (ram == null)
            {
                return new RamResult
                {
                    Success = false,
                    Message = "Ram không tồn tại."
                };
            }

            return new RamResult
            {
                Success = true,
                Message = "Lấy chi tiết Ram thành công.",
                Ram = _mapper.Map<RamDTO>(ram)
            };
        }
    }
}
