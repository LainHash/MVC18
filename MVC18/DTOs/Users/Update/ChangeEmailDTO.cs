namespace MVC18.DTOs.Users.Update
{
    public class ChangeEmailDTO
    {
        public string OldEmail { get; set; } = null!;
        public string NewEmail { get; set; } = null!;
    }
}
