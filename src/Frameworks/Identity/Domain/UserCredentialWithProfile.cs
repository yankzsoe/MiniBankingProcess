using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Framework.Domain {
    public class UserCredentialWithProfile {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public DateOnly Dob { get; set; }
        public string Password { get; set; }
        public string AccountNumber { get; set; }
        public decimal Deposit { get; set; }
        public string Currency { get; set; }
    }
}
