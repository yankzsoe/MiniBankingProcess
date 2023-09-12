namespace Transaction.Framework.Data.EntityConfigurations {
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Microsoft.EntityFrameworkCore.Metadata.Internal;
    using Transaction.Framework.Data.Entities;

    public class AccountSummaryEntityConfiguration : IEntityTypeConfiguration<AccountSummaryEntity> {

        public void Configure(EntityTypeBuilder<AccountSummaryEntity> builder) {
            builder.HasKey(t => t.AccountNumber);
            builder.Property(t => t.AccountNumber).ValueGeneratedNever();
            builder.Property(t => t.Balance).IsConcurrencyToken().IsRequired();
            builder.Property(t => t.Currency).IsRequired();
        }
    }
}
