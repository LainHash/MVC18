namespace MVC18.Services.Interfaces.Auth
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string body);
    }
}
