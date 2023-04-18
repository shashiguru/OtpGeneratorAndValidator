using Email.OTP.Application;
using Email.OTP.Domain.Models;
using Email.OTP.Infrastructure.EntityConfigurations;
using Email.OTP.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Email.OTP.Infrastructure
{
    public class OtpGenerateDbContext : DbContext, IOtpGenerateDbContext
    {
        public OtpGenerateDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<UserDetail> UserDetail { get; set; } = null!;

        /// <summary>
        /// OnModelCreating
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserDetailEntityConfiguration());
            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// SaveChangesAsync
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {

            foreach (EntityEntry<BaseEntity> entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDateTime = DateTime.Now.ToSgt();
                        break;
                }
            }

            var result = await base.SaveChangesAsync(cancellationToken);
            return result;
        }
    }
}
