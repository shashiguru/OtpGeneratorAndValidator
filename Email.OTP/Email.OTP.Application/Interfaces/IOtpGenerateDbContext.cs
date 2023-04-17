using Email.OTP.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Email.OTP.Infrastructure.Interfaces
{
    public interface IOtpGenerateDbContext
    {
        DbSet<UserDetail> UserDetail { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
    }
}
