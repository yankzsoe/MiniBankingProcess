using Identity.Framework.Data.EntityConfigurations;
using Identity.Framework.Data.Seeds;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Framework.Data {
    public class ApplicationDbContext : DbContext {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.ApplyConfiguration(new UsersCredentialEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UsersProfileEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UsersBankAccountEntityConfiguration());

            modelBuilder.SeedUserAccountCredential();

            base.OnModelCreating(modelBuilder);
        }
    }
}
