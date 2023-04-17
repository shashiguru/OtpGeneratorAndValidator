using System.Diagnostics.CodeAnalysis;

namespace Email.OTP.WebAPI.Models.Request
{
    [ExcludeFromCodeCoverage]
    public class GenerateRequestDto
    {
        public string Email { get; set; }
    }
}
