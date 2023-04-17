using Email.OTP.Application.Models;

namespace Email.OTP.Application.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendEmail(EmailData email);
    }
}
