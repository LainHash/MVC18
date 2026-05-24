using AutoMapper;
using MVC18.DTOs.Products;
using MVC18.DTOs.Products.Update;
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

            CreateMap<VwdCpuDetail, UpdateCpuDTO>();
            CreateMap<VwdGpuDetail, UpdateGpuDTO>();
            CreateMap<VwdLaptopDetail, UpdateLaptopDTO>();
            CreateMap<VwdStorageDetail, UpdateStorageDTO>();
            CreateMap<VwdRamDetail, UpdateRamDTO>();
        }
    }
}
