using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MVC18.Data;
using MVC18.DTOs.Products;
using MVC18.DTOs.Products.Create;
using MVC18.DTOs.Results.Products;
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

        public Task<LaptopResult> CreateAsync(CreateLaptopDTO dto)
        {
            throw new NotImplementedException();
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
