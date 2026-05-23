using System.ComponentModel.DataAnnotations;

namespace MVC18.DTOs.Products.Create
{
    public class CreateGpuDTO : CreateProductDTO
    {
        [Required(ErrorMessage = "Dung lượng bộ nhớ là bắt buộc.")]
        [Range(0.5f, 128f, ErrorMessage = "Dung lượng bộ nhớ phải từ 0.5 đến 128 GB.")]
        public float MemorySize { get; set; }

        [Required(ErrorMessage = "Loại bộ nhớ là bắt buộc.")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Loại bộ nhớ phải từ 2 đến 20 ký tự.")]
        public string MemoryType { get; set; } = null!;

        [Required(ErrorMessage = "Xung nhịp là bắt buộc.")]
        [Range(100, 5000, ErrorMessage = "Xung nhịp phải từ 100 đến 5000 MHz.")]
        public int Clock { get; set; }

        [Required(ErrorMessage = "Số Unified Shader là bắt buộc.")]
        [Range(1, 100000, ErrorMessage = "Số Unified Shader phải từ 1 đến 100000.")]
        public int UnifiedShader { get; set; }

        [Required(ErrorMessage = "Số TMU là bắt buộc.")]
        [Range(1, 10000, ErrorMessage = "Số TMU phải từ 1 đến 10000.")]
        public int Tmu { get; set; }

        [Required(ErrorMessage = "Số ROP là bắt buộc.")]
        [Range(1, 1000, ErrorMessage = "Số ROP phải từ 1 đến 1000.")]
        public int Rop { get; set; }

        [Required(ErrorMessage = "Bus là bắt buộc.")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Bus phải từ 2 đến 20 ký tự.")]
        public string Bus { get; set; } = null!;

        public bool? Igpu { get; set; }
    }
}
