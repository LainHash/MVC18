namespace MVC18.DTOs.Products.Create
{
    public class CreateLaptopDTO : CreateProductDTO
    {
        public string LaptopType { get; set; } = null!;

        public string Os { get; set; } = null!;

        public string ScreenResolution { get; set; } = null!;

        public float Length { get; set; }

        public float Weight { get; set; }

        public int CpuId { get; set; }
        public int GpuId { get; set; }
        public int RamId { get; set; }
        public int StorageId { get; set; }
    }
}
