using System.ComponentModel.DataAnnotations;

namespace MVC18.DTOs.Auth
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "Tên đăng nhập không được để trống.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Tên đăng nhập phải từ 3 đến 50 ký tự.")]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = "Email không được để trống.")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
        [StringLength(100, ErrorMessage = "Email không vượt quá 100 ký tự.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Mật khẩu không được để trống.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Mật khẩu phải từ 6 đến 100 ký tự.")]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "Vui lòng nhập lại mật khẩu.")]
        [Compare(nameof(Password), ErrorMessage = "Mật khẩu xác nhận không khớp.")]
        public string ConfirmPassword { get; set; } = null!;

        [Required(ErrorMessage = "Họ không được để trống.")]
        [StringLength(50, ErrorMessage = "Họ không vượt quá 50 ký tự.")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "Tên không được để trống.")]
        [StringLength(50, ErrorMessage = "Tên không vượt quá 50 ký tự.")]
        public string LastName { get; set; } = null!;

        public bool Gender { get; set; }

        [Required(ErrorMessage = "Ngày sinh không được để trống.")]
        public DateOnly Dob { get; set; }

        [Required(ErrorMessage = "Thành phố không được để trống.")]
        [StringLength(100, ErrorMessage = "Thành phố không vượt quá 100 ký tự.")]
        public string City { get; set; } = null!;

        [Required(ErrorMessage = "Quốc gia không được để trống.")]
        [StringLength(100, ErrorMessage = "Quốc gia không vượt quá 100 ký tự.")]
        public string Country { get; set; } = null!;

        [Required(ErrorMessage = "Địa chỉ không được để trống.")]
        [StringLength(200, ErrorMessage = "Địa chỉ không vượt quá 200 ký tự.")]
        public string Address { get; set; } = null!;

        [Required(ErrorMessage = "Số điện thoại không được để trống.")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ.")]
        [StringLength(15, ErrorMessage = "Số điện thoại không vượt quá 15 ký tự.")]
        public string Phone { get; set; } = null!;

        [Required(ErrorMessage = "Số CCCD không được để trống.")]
        [StringLength(20, ErrorMessage = "Số CCCD không vượt quá 20 ký tự.")]
        public string CitizenIdentityCard { get; set; } = null!;
    }
}
