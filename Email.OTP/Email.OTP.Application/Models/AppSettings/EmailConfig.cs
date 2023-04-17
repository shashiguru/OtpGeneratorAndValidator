using System.Diagnostics.CodeAnalysis;

namespace Email.OTP.Application.Models.AppSettings
{
    [ExcludeFromCodeCoverage]
    public class EmailConfig
    {
        public string FromEmail { get; set; }
    }
}
