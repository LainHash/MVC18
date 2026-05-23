using System.ComponentModel.DataAnnotations;

namespace MVC18.DTOs.Products.Create
{
    public class CreateLaptopDTO : CreateProductDTO
    {
        [Required(ErrorMessage = "Loại Laptop là bắt buộc.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Loại Laptop phải từ 2 đến 50 ký tự.")]
        public string LaptopType { get; set; } = null!;

        [Required(ErrorMessage = "Hệ điều hành là bắt buộc.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Hệ điều hành phải từ 2 đến 50 ký tự.")]
        public string Os { get; set; } = null!;

        [Required(ErrorMessage = "Độ phân giải màn hình là bắt buộc.")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Độ phân giải phải từ 3 đến 30 ký tự.")]
        public string ScreenResolution { get; set; } = null!;

        [Required(ErrorMessage = "Chiều dài là bắt buộc.")]
        [Range(0.1f, 100f, ErrorMessage = "Chiều dài phải từ 0.1 đến 100 inch.")]
        public float Length { get; set; }

        [Required(ErrorMessage = "Trọng lượng là bắt buộc.")]
        [Range(0.1f, 20f, ErrorMessage = "Trọng lượng phải từ 0.1 đến 20 kg.")]
        public float Weight { get; set; }

        [Required(ErrorMessage = "CPU là bắt buộc.")]
        [Range(1, int.MaxValue, ErrorMessage = "CpuId phải lớn hơn 0.")]
        public int CpuId { get; set; }

        [Required(ErrorMessage = "GPU là bắt buộc.")]
        [Range(1, int.MaxValue, ErrorMessage = "GpuId phải lớn hơn 0.")]
        public int GpuId { get; set; }

        [Required(ErrorMessage = "RAM là bắt buộc.")]
        [Range(1, int.MaxValue, ErrorMessage = "RamId phải lớn hơn 0.")]
        public int RamId { get; set; }

        [Required(ErrorMessage = "Storage là bắt buộc.")]
        [Range(1, int.MaxValue, ErrorMessage = "StorageId phải lớn hơn 0.")]
        public int StorageId { get; set; }
    }
}
