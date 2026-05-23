using System.ComponentModel.DataAnnotations;

namespace MVC18.DTOs.Products.Create
{
    public class CreateRamDTO : CreateProductDTO
    {
        [Required(ErrorMessage = "Dung lượng là bắt buộc.")]
        [Range(1, 2048, ErrorMessage = "Dung lượng RAM phải từ 1 đến 2048 GB.")]
        public int Capacity { get; set; }

        [Required(ErrorMessage = "Thế hệ RAM là bắt buộc.")]
        [StringLength(10, MinimumLength = 2, ErrorMessage = "Thế hệ RAM phải từ 2 đến 10 ký tự.")]
        public string Gen { get; set; } = null!;

        [Required(ErrorMessage = "Tốc độ là bắt buộc.")]
        [Range(400, 20000, ErrorMessage = "Tốc độ RAM phải từ 400 đến 20000 MHz.")]
        public int Speed { get; set; }

        [Required(ErrorMessage = "Kit là bắt buộc.")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Kit phải từ 1 đến 20 ký tự.")]
        public string Kit { get; set; } = null!;
    }
}
