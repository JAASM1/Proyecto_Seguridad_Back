namespace back_sistema_de_eventos.Services.IService.IUser
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
