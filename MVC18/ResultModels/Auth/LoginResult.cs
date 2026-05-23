namespace MVC18.ResultModels.Auth
{
    public class LoginResult : BaseResult
    {
        public string Email { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Role { get; set; } = null!;
        public string Token { get; set; } = null!;
    }
}
