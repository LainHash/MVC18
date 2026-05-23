using System.ComponentModel.DataAnnotations;

namespace MVC18.DTOs.Products.Create
{
    public class CreateCpuDTO : CreateProductDTO
    {
        [Required(ErrorMessage = "Số nhân là bắt buộc.")]
        [Range(1, 128, ErrorMessage = "Số nhân phải từ 1 đến 128.")]
        public int Cores { get; set; }

        [Required(ErrorMessage = "Số luồng là bắt buộc.")]
        [Range(1, 256, ErrorMessage = "Số luồng phải từ 1 đến 256.")]
        public int Logicals { get; set; }

        [Required(ErrorMessage = "TDP là bắt buộc.")]
        [Range(1f, 1000f, ErrorMessage = "TDP phải từ 1 đến 1000 W.")]
        public float Tdp { get; set; }

        [Required(ErrorMessage = "Socket là bắt buộc.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Socket phải từ 2 đến 50 ký tự.")]
        public string Socket { get; set; } = null!;

        [Required(ErrorMessage = "Tốc độ xung nhịp là bắt buộc.")]
        [Range(100, 10000, ErrorMessage = "Tốc độ xung nhịp phải từ 100 đến 10000 MHz.")]
        public int Speed { get; set; }

        [Required(ErrorMessage = "Tốc độ Turbo là bắt buộc.")]
        [Range(100, 10000, ErrorMessage = "Tốc độ Turbo phải từ 100 đến 10000 MHz.")]
        public int Turbo { get; set; }
    }
}
