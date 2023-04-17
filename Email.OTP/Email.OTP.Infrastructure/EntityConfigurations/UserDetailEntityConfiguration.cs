using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Email.OTP.Domain.Models;

namespace Email.OTP.Infrastructure.EntityConfigurations
{
    public class UserDetailEntityConfiguration : IEntityTypeConfiguration<UserDetail>
    {
        /// <summary>
        /// Configure
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<UserDetail> builder)
        {
            builder.HasKey(_ => _.Id);
            builder.Property(_ => _.Email).HasMaxLength(50);
            builder.Property(_ => _.Otp);
            builder.Property(_ => _.CreatedDateTime);
            builder.Property(_ => _.UpdatedDateTime);
            builder.Property(_ => _.TryCount);
        }
    }
}
