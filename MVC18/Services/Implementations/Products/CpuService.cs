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
    }
}
