using Identity.Framework.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Framework.Data.EntityConfigurations {
    public class UsersBankAccountEntityConfiguration : IEntityTypeConfiguration<UserBankAccountEntity> {
        public void Configure(EntityTypeBuilder<UserBankAccountEntity> builder) {
            builder.HasKey(e => e.BankAccountId);
            builder.Property(f => f.BankAccountId).ValueGeneratedOnAdd();
            builder.Property(f => f.AccountNumber).HasMaxLength(20);
            builder.HasOne(e => e.UserCredential).WithMany(e => e.UserBankAccounts).HasForeignKey(e => e.UserId);
        }
    }
}
