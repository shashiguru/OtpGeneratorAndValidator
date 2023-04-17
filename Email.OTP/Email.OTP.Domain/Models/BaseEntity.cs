using System.Diagnostics.CodeAnalysis;

namespace Email.OTP.Domain.Models
{
    [ExcludeFromCodeCoverage]
    public abstract class BaseEntity
    {
        public Guid Id { get; set; } = new Guid();
        public DateTimeOffset CreatedDateTime { get; set; }
        public DateTimeOffset UpdatedDateTime { get; set; }
    }
}
