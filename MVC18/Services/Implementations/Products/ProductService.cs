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

        public async Task<ProductResult> CreateAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ProductResult> UpdateAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ProductResult> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<LaptopResult> GetLaptopDetailsAsync(Guid id)
        {
            var laptop = await _context.VwdLaptopDetails
                .Where(p => !p.IsDeleted)
                .FirstOrDefaultAsync(p => p.ProductUuid == id);
            if (laptop == null)
            {
                return new LaptopResult
                {
                    Success = false,
                    Message = "Sản phẩm không tồn tại."
                };
            }

            return new LaptopResult
            {
                Success = true,
                Message = "Lấy chi tiết laptop thành công.",
                Laptop = _mapper.Map<LaptopDTO>(laptop)
            };
        }

        public async Task<CpuResult> GetCpuDetailsAsync(Guid id)
        {
            var cpu = await _context.VwdCpuDetails
                .Where(p => !p.IsDeleted)
                .FirstOrDefaultAsync(p => p.ProductUuid == id);
            if (cpu == null)
            {
                return new CpuResult
                {
                    Success = false,
                    Message = "Sản phẩm không tồn tại."
                };
            }

            return new CpuResult
            {
                Success = true,
                Message = "Lấy chi tiết CPU thành công.",
                Cpu = _mapper.Map<CpuDTO>(cpu)
            };
        } 

        public async Task<GpuResult> GetGpuDetailsAsync(Guid id)
        {
            var gpu = await _context.VwdGpuDetails
                .Where(p => !p.IsDeleted)
                .FirstOrDefaultAsync(p => p.ProductUuid == id);
            if (gpu == null)
            {
                return new GpuResult
                {
                    Success = false,
                    Message = "Sản phẩm không tồn tại."
                };
            }

            return new GpuResult
            {
                Success = true,
                Message = "Lấy chi tiết GPU thành công.",
                Gpu = _mapper.Map<GpuDTO>(gpu)
            };
        }

        public async Task<RamResult> GetRamDetailsAsync(Guid id)
        {
            var ram = await _context.VwdRamDetails
                .Where(p => !p.IsDeleted)
                .FirstOrDefaultAsync(p => p.ProductUuid == id);
            if (ram == null)
            {
                return new RamResult
                {
                    Success = false,
                    Message = "Sản phẩm không tồn tại."
                };
            }

            return new RamResult
            {
                Success = true,
                Message = "Lấy chi tiết RAM thành công.",
                Ram = _mapper.Map<RamDTO>(ram)
            };
        }

        public async Task<StorageResult> GetStorageDetailsAsync(Guid id)
        {
            var storage = await _context.VwdStorageDetails
                .Where(p => !p.IsDeleted)
                .FirstOrDefaultAsync(p => p.ProductUuid == id);
            if (storage == null)
            {
                return new StorageResult
                {
                    Success = false,
                    Message = "Sản phẩm không tồn tại."
                };
            }

            return new StorageResult
            {
                Success = true,
                Message = "Lấy chi tiết lưu trữ thành công.",
                Storage = _mapper.Map<StorageDTO>(storage)
            };
        }
    }
}
