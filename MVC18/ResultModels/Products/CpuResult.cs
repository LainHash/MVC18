using MVC18.DTOs.Products;
using MVC18.DTOs.Products.Update;
using MVC18.ResultModels;

namespace MVC18.ResultModels.Products
{
    public class CpuResult : BaseResult
    {
        public CpuDTO? Cpu { get; set; }
        public List<CpuDTO>? Cpus { get; set; }

        public UpdateCpuDTO? CpuUpdate { get; set; }
    }
}
