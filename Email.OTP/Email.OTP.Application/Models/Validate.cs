using System.Diagnostics.CodeAnalysis;

namespace Email.OTP.Domain.Models
{
    [ExcludeFromCodeCoverage]
    public class Validate
    {
        public string Email { get; set; }
        public int Otp { get; set; }
    }
}
