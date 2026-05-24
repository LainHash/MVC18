using MVC18.DTOs.Products;
using MVC18.DTOs.Products.Update;
using MVC18.ResultModels;

namespace MVC18.ResultModels.Products
{
    public class RamResult : BaseResult
    {
        public RamDTO? Ram { get; set; }
        public List<RamDTO>? Rams { get; set; }

        public UpdateRamDTO? RamUpdate { get; set; }
    }
}
