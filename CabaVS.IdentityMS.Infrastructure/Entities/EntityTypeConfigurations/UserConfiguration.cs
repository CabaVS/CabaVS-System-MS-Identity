using CabaVS.IdentityMS.Core.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CabaVS.IdentityMS.Infrastructure.Entities.EntityTypeConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");

            builder.HasIndex(x => x.Email).IsUnique();
            builder.HasIndex(x => x.Username).IsUnique();

            builder.Property(x => x.Email).HasMaxLength(MaxLengthConstraints.User.Email).IsRequired();
            builder.Property(x => x.Username).HasMaxLength(MaxLengthConstraints.User.Username).IsRequired();
            builder.Property(x => x.Password).HasMaxLength(MaxLengthConstraints.User.Password).IsRequired();
        }
    }
}