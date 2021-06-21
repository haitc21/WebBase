using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using WebBase.Common;
using WebBase.Data.Entities;

namespace WebBase.Data.Configurations
{
    public class AppUserConfig : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            throw new NotImplementedException();
        }

        void IEntityTypeConfiguration<AppUser>.Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.ToTable("AppUsers");
            builder.Property(x => x.FirstName).HasMaxLength(250).IsRequired();
            builder.Property(x => x.LastName).HasMaxLength(250).IsRequired();
            builder.Property(x => x.Dob).IsRequired();
            builder.Property(x => x.Status).IsRequired().HasDefaultValue(Enums.Status.UnActived);
        }
    }
}