using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using back_sistema_de_eventos.Services.IService.IUser;

namespace back_sistema_de_eventos.Services.Service.User
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var smtpClient = new SmtpClient(_configuration["Email:SmtpServer"])
            {
                Port = int.Parse(_configuration["Email:Port"]),
                Credentials = new NetworkCredential(_configuration["Email:Username"], _configuration["Email:Password"]),
                EnableSsl = true,
            };
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_configuration["Email:From"]),
                Subject = subject,
                Body = message,
                IsBodyHtml = true,
            };

            mailMessage.To.Add(email);

            await smtpClient.SendMailAsync(mailMessage);
        }

    }
    
}
