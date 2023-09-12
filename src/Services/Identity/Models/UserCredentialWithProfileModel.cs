using System;

namespace Identity.WebApi.Models {
    //public class UserCredentialWithProfileModel {
    //    public string Username { get; set; }
    //    public string FullName { get; set; }
    //    public DateOnly Dob { get; set; }
    //    public string Password { get; set; }
    //}

    public class UserCredentialWithProfileModel {
        public string FullName { get; set; }
        public DateOnly Dob { get; set; }
        public UserAccount UserAccount { get; set; }
        public BankAccount BankAccount { get; set; }

    }

    public record UserAccount {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public record BankAccount {
        public string AccountNumber { get; set; }
        public string Currency { get; set; }
        public decimal Deposit { get; set; }
    }
}
