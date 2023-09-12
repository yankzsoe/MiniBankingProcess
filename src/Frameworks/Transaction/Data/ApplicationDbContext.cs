using Microsoft.EntityFrameworkCore;
using Transaction.Framework.Data.EntityConfigurations;
using Transaction.Framework.Data.Seeds;

namespace Transaction.Framework.Data {
    public class ApplicationDbContext : DbContext {
       
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.ApplyConfiguration(new AccountSummaryEntityConfiguration());
            modelBuilder.ApplyConfiguration(new AccountTransactionEntityConfiguration());

            modelBuilder.SeedAccountSummary();

            base.OnModelCreating(modelBuilder);
        }
    }
}
