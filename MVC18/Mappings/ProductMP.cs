using AutoMapper;
using MVC18.DTOs.Products;
using MVC18.Models;

namespace MVC18.Mappings
{
    public class ProductMP : Profile
    {
        public ProductMP()
        {
            CreateMap<VwProduct, ProductDTO>();
            CreateMap<CpuDTO, VwdCpuDetail>().ReverseMap();
            CreateMap<GpuDTO, VwdGpuDetail>().ReverseMap();
            CreateMap<LaptopDTO, VwdLaptopDetail>().ReverseMap();
            CreateMap<StorageDTO, VwdStorageDetail>().ReverseMap();
            CreateMap<RamDTO, VwdRamDetail>().ReverseMap();
        }
    }
}
