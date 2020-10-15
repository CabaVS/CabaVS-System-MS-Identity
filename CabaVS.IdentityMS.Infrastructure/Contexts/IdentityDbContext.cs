using CabaVS.IdentityMS.Infrastructure.Entities;
using CabaVS.IdentityMS.Infrastructure.Entities.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;

namespace CabaVS.IdentityMS.Infrastructure.Contexts
{
    public class IdentityDbContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<RefreshTokenEntity> RefreshTokens { get; set; }

        public IdentityDbContext(DbContextOptions<IdentityDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new RefreshTokenConfiguration());
        }
    }
}