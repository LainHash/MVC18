using System.ComponentModel.DataAnnotations;

namespace MVC18.DTOs.Auth
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Email không được để trống.")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Mật khẩu không được để trống.")]
        public string Password { get; set; } = null!;
    }
}
