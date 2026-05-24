namespace MVC18.DTOs.Users.Update
{
    public class ChangePasswordDTO
    {
        public string OldPassword { get; set; } = null!;

        public string NewPassword { get; set; } = null!;
        public string ConfirmNewPassword { get; set; } = null!;
    }
}
