using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CabaVS.IdentityMS.Infrastructure.Entities.EntityTypeConfigurations
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshTokenEntity>
    {
        public void Configure(EntityTypeBuilder<RefreshTokenEntity> builder)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));

            builder.HasKey(x => new {x.UserId, x.RefreshToken});

            builder
                .HasOne<UserEntity>()
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}