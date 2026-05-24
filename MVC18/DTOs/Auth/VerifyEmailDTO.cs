using System.ComponentModel.DataAnnotations;

namespace MVC18.DTOs.Auth
{
    public class VerifyEmailDTO
    {
        [Required(ErrorMessage = "Email không được để trống.")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Mã xác nhận không được để trống.")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "Mã xác nhận phải có 6 chữ số.")]
        [RegularExpression("^[0-9]{6}$", ErrorMessage = "Mã xác nhận chỉ bao gồm số.")]
        public string Code { get; set; } = null!;
    }
}
