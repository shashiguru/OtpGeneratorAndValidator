using Email.OTP.WebAPI.Models.Response;

namespace Email.OTP.Application.Interfaces
{
    public interface IEmailOpertions
    {
        Task<GenerateResponseDto> GenerateOtp(string email);
        Task<bool> ValidateOTP(string email, int? otp); 
    }
}
