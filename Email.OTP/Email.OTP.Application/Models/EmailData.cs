using System.Diagnostics.CodeAnalysis;

namespace Email.OTP.Application.Models
{
    [ExcludeFromCodeCoverage]
    public class EmailData
    {
        public string FromEmail { get; set; }
        public string ToEmail { get; set; }
        public string Content { get; set; }
    }
}
