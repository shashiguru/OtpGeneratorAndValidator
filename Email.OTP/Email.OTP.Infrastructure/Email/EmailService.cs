using Email.OTP.Application.Interfaces;
using Email.OTP.Application.Models;

namespace Email.OTP.Infrastructure.Email
{
    public class EmailService: IEmailService
    {
        /// <summary>
        /// SendEmail
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<bool> SendEmail(EmailData email)
        {
            //This service will send email with Otp
            await Task.Delay(1000);
            return true;
        }
    }
}
