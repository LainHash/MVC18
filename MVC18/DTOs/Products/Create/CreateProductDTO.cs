using System.ComponentModel.DataAnnotations;

namespace MVC18.DTOs.Products.Create
{
    public class CreateProductDTO
    {
        [Required(ErrorMessage = "CategoryId là bắt buộc.")]
        [Range(1, int.MaxValue, ErrorMessage = "CategoryId phải lớn hơn 0.")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "CompanyId là bắt buộc.")]
        [Range(1, int.MaxValue, ErrorMessage = "CompanyId phải lớn hơn 0.")]
        public int CompanyId { get; set; }

        [Required(ErrorMessage = "Tên sản phẩm là bắt buộc.")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "Tên sản phẩm phải từ 2 đến 200 ký tự.")]
        public string ProductName { get; set; } = null!;

        [Required(ErrorMessage = "URL hình ảnh là bắt buộc.")]
        [StringLength(500, ErrorMessage = "URL hình ảnh không được vượt quá 500 ký tự.")]
        public string ImageUrl { get; set; } = null!;

        [StringLength(2000, ErrorMessage = "Mô tả không được vượt quá 2000 ký tự.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Đơn giá là bắt buộc.")]
        [Range(0.01, 999999999.99, ErrorMessage = "Đơn giá phải lớn hơn 0.")]
        public decimal UnitPrice { get; set; }

        [Required(ErrorMessage = "Số lượng tồn kho là bắt buộc.")]
        [Range(0, int.MaxValue, ErrorMessage = "Số lượng tồn kho không được âm.")]
        public int UnitsInStock { get; set; }
    }
}
