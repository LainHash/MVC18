using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MVC18.Data;
using MVC18.DTOs.Products;
using MVC18.DTOs.Products.Create;
using MVC18.DTOs.Results.Products;
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


        public Task<GpuResult> CreateAsync(CreateGpuDTO dto)
        {
            throw new NotImplementedException();
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
