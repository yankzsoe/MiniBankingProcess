using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaction.Framework.Data.Entities;

namespace Transaction.Framework.Data.Seeds {
    public static class AccountSummarySeed {
        public static void SeedAccountSummary(this ModelBuilder modelBuilder) {
            modelBuilder.Entity<AccountSummaryEntity>().HasData(
                new AccountSummaryEntity {
                    AccountNumber = "1234567890",
                    Balance = 1000000,
                    Currency = "IDR"
                }
            );
        }
    }
}
