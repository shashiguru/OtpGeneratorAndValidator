using System.Diagnostics.CodeAnalysis;

namespace Email.OTP.WebAPI.Models.Response
{
    [ExcludeFromCodeCoverage]
    public class GenerateResponseDto
    {
        public string Email { get; set; }
        public int? Otp { get; set; }
    }
}
