using MimeKit;
using OnlineStore.Requests;
using MimeKit.Text;
using MailKit.Net.Smtp;
using MailKit.Security;
namespace OnlineStore.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private string Server;
        private string UserName;
        private string Password;
        IConfiguration _configuration;
        public EmailService(IConfiguration _config) {
            _configuration = _config;
            Server = _configuration.GetSection("EmailCredentials:EmailServer").Value;
            UserName = _configuration.GetSection("EmailCredentials:UserName").Value;
            Password = _configuration.GetSection("EmailCredentials:EmailPassword").Value;
        }


        public void SendEmail(EmailRequest request)
        {
            var email = new MimeMessage();

            email.From.Add(MailboxAddress.Parse(UserName));

            email.To.Add(MailboxAddress.Parse(request.To));

            email.Subject = request.Subject;
            email.Body = new TextPart(TextFormat.Html)
            {
                Text = request.Body
            };

            using var smtp = new SmtpClient();

            smtp.Connect(Server, 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(UserName,Password);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
