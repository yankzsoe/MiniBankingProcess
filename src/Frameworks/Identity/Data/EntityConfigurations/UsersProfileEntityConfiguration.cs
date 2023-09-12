using Identity.Framework.Converter;
using Identity.Framework.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Framework.Data.EntityConfigurations {
    internal class UsersProfileEntityConfiguration : IEntityTypeConfiguration<UserProfileEntity> {
        public void Configure(EntityTypeBuilder<UserProfileEntity> builder) {
            builder.HasKey(f => f.UserId);
            builder.HasOne(t => t.UsersEntity).WithOne(u => u.UserProfile).HasForeignKey<UserProfileEntity>(up => up.UserId);
            builder.Property(f => f.FullName).IsRequired();
            builder.Property(f => f.Dob).IsRequired().HasConversion<DateOnlyConverter, DateOnlyComparer>();
        }
    }
}
