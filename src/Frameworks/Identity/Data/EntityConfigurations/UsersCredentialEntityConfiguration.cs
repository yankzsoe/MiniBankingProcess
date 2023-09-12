using Identity.Framework.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Framework.Data.EntityConfigurations {
    internal class UsersCredentialEntityConfiguration : IEntityTypeConfiguration<UserCredentialEntity> {
        public void Configure(EntityTypeBuilder<UserCredentialEntity> builder) {
            builder.HasKey(f => f.UserId);
            builder.Property(f => f.UserId).ValueGeneratedOnAdd();
            builder.HasIndex(f => f.UserName).IsUnique();
            builder.Property(f => f.Password).IsRequired();
            builder.HasMany(e => e.UserBankAccounts).WithOne(e => e.UserCredential).HasForeignKey(c => c.UserId);
        }
    }
}
