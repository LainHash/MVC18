using System.ComponentModel.DataAnnotations;

namespace MVC18.DTOs.Users.Update
{
    public class ChangeEmailDTO
    {
        [Required(ErrorMessage = "Email cũ không được để trống.")]
        [EmailAddress(ErrorMessage = "Email cũ không hợp lệ.")]
        [StringLength(100, ErrorMessage = "Email không vượt quá 100 ký tự.")]
        public string OldEmail { get; set; } = null!;

        [Required(ErrorMessage = "Email mới không được để trống.")]
        [EmailAddress(ErrorMessage = "Email mới không hợp lệ.")]
        [StringLength(100, ErrorMessage = "Email không vượt quá 100 ký tự.")]
        public string NewEmail { get; set; } = null!;
    }
}
