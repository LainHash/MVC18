using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MVC18.Data;
using MVC18.DTOs.Products;
using MVC18.DTOs.Results.Products;
using MVC18.Services.Interfaces.Products;

namespace MVC18.Services.Implementations.Products
{
    public class ProductService : IProductService
    {
        private readonly LaptopWebDb06Context _context;
        private readonly IMapper _mapper;

        public ProductService(LaptopWebDb06Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<ProductResult> GetAllAsync()
        {
            var list = await _context.VwProducts
                .ToListAsync();
            return new ProductResult
            {
                Success = true,
                Message = "Lấy danh sách sản phẩm thành công.",
                Products = _mapper.Map<List<ProductDTO>>(list)
            };
        }

        public async Task<ProductResult> GetOneAsync(Guid id)
        {
            var product = await _context.VwProducts
                .Where(p => !p.IsDeleted)
                .FirstOrDefaultAsync(p => p.ProductUuid == id);
            if (product == null)
            {
                return new ProductResult
                {
                    Success = false,
                    Message = "Sản phẩm không tồn tại."
                };
            }
            return new ProductResult
            {
                Success = true,
                Message = "Lấy sản phẩm thành công.",
                Product = _mapper.Map<ProductDTO>(product)
            };
        }

        public async Task<ProductResult> DeleteAsync(Guid id)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.ProductUuid == id);
            if (product == null)
            {
                return new ProductResult
                {
                    Success = false,
                    Message = "Sản phẩm không tồn tại."
                };
            }
            if (product.IsDeleted)
            {
                return new ProductResult
                {
                    Success = false,
                    Message = "Sản phẩm đã bị xóa."
                };
            }

            product.IsDeleted = true;
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return new ProductResult
            {
                Success = true,
                Message = "Xóa sản phẩm thành công."

            };
        }

    }
}
