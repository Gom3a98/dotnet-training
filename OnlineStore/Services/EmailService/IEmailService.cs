using OnlineStore.Requests;

namespace OnlineStore.Services.EmailService
{
    public interface IEmailService
    {
        void SendEmail(EmailRequest request);
    }
}
