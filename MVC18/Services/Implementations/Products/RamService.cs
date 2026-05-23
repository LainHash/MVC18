using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MVC18.Data;
using MVC18.DTOs.Products;
using MVC18.DTOs.Products.Create;
using MVC18.DTOs.Results.Products;
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


        public Task<RamResult> CreateAsync(CreateRamDTO dto)
        {
            throw new NotImplementedException();
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
