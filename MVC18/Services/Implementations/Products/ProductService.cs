using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MVC18.Data;
using MVC18.DTOs.Misc;
using MVC18.DTOs.Products;
using MVC18.Helpers.Constants.Misc;
using MVC18.ResultModels.Misc;
using MVC18.ResultModels.Products;
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

        public async Task<PagedResult<ProductDTO>> GetAllAsync(ProductQuery query)
        {
            var list = _context.VwProducts
                .Where(p => !p.IsDeleted)
                .AsQueryable();

            if(!string.IsNullOrEmpty(query.Keyword))
            {
                list = list.Where(p => p.ProductName.Contains(query.Keyword));
            }

            if(!string.IsNullOrEmpty(query.CategoryName))
            {
                list = list.Where(p => p.CategoryName == query.CategoryName);
            }

            if(!string.IsNullOrEmpty(query.CompanyName))
            {
                list = list.Where(p => p.CompanyName == query.CompanyName);
            }

            if(!string.IsNullOrEmpty(query.SortBy))
            {
                switch (query.SortBy)
                {
                    case SortByConstants.CreatedAtAsc:
                        list = list.OrderBy(p => p.CreatedAt);
                        break;
                    case SortByConstants.CreatedAtDesc:
                        list = list.OrderByDescending(p => p.CreatedAt);
                        break;
                    case SortByConstants.NameAsc:
                        list = list.OrderBy(p => p.ProductName);
                        break;
                    case SortByConstants.NameDesc:
                        list = list.OrderByDescending(p => p.ProductName);
                        break;
                    case SortByConstants.PriceAsc:
                        list = list.OrderBy(p => p.UnitPrice);
                        break;
                    case SortByConstants.PriceDesc:
                        list = list.OrderByDescending(p => p.UnitPrice);
                        break;
                }  
            }
            var totalItems = await list.CountAsync();
            var items = await list.Skip((query.Page - 1) * query.PageSize)
                                  .Take(query.PageSize)
                                  .ToListAsync();

            return new PagedResult<ProductDTO>
            {
                Success = true,
                Message = "Lấy danh sách sản phẩm thành công.",
                Items = _mapper.Map<List<ProductDTO>>(items),
                TotalItems = totalItems,
                Page = query.Page,
                PageSize = query.PageSize
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
