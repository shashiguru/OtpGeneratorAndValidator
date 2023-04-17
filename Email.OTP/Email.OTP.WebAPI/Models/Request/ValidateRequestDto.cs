using System.Diagnostics.CodeAnalysis;

namespace Email.OTP.WebAPI.Models.Request
{
    [ExcludeFromCodeCoverage]
    public class ValidateRequestDto
    {
        public string Email { get; set; }
        public int Otp { get; set; }
    }
}
