using System.ComponentModel.DataAnnotations;

namespace MVC18.DTOs.Products.Create
{
    public class CreateStorageDTO : CreateProductDTO
    {
        [Required(ErrorMessage = "Dung lượng là bắt buộc.")]
        [Range(1, 100000, ErrorMessage = "Dung lượng ổ cứng phải từ 1 đến 100000 GB.")]
        public int Capacity { get; set; }

        [Required(ErrorMessage = "Loại bộ nhớ là bắt buộc.")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Loại bộ nhớ phải từ 2 đến 20 ký tự.")]
        public string MemoryType { get; set; } = null!;

        [Required(ErrorMessage = "Loại giao tiếp là bắt buộc.")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Loại giao tiếp phải từ 2 đến 20 ký tự.")]
        public string InterfaceType { get; set; } = null!;

        [Required(ErrorMessage = "Tốc độ đọc là bắt buộc.")]
        [Range(1, 20000, ErrorMessage = "Tốc độ đọc phải từ 1 đến 20000 MB/s.")]
        public int ReadSpeed { get; set; }

        [Required(ErrorMessage = "Tốc độ ghi là bắt buộc.")]
        [Range(1, 20000, ErrorMessage = "Tốc độ ghi phải từ 1 đến 20000 MB/s.")]
        public int WriteSpeed { get; set; }
    }
}
