using System.Diagnostics.CodeAnalysis;

namespace Email.OTP.Domain.Models
{
    [ExcludeFromCodeCoverage]
    public class UserDetail: BaseEntity
    {
        public string Email { get; set; }
        public int? Otp { get; set; }
        public int TryCount { get; set; }
    }
}
