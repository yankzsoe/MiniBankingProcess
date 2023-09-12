using Identity.Framework.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Framework.Data.Seeds {
    public static class UserAccountCredentialSeed {
        public static void SeedUserAccountCredential(this ModelBuilder modelBuilder) {
            modelBuilder.Entity<UserCredentialEntity>().HasData(
                new UserCredentialEntity {
                    UserId = 1,
                    UserName="test",
                    Password="test",
                }
            );

            modelBuilder.Entity<UserBankAccountEntity>().HasData(
                new UserBankAccountEntity {
                    BankAccountId = 1,
                    AccountNumber = "1234567890",
                    Currency = "IDR",
                    UserId = 1,
                });

            modelBuilder.Entity<UserProfileEntity>().HasData(
                new UserProfileEntity {
                    Dob = DateOnly.Parse("1992-04-12"),
                    FullName = "fullname test",
                    UserId = 1
                });
        }
    }
}
