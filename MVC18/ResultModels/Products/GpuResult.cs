using MVC18.DTOs.Products;
using MVC18.ResultModels;

namespace MVC18.ResultModels.Products
{
    public class GpuResult : BaseResult
    {
        public GpuDTO? Gpu { get; set; }
        public List<GpuDTO>? Gpus { get; set; }
    }
}
