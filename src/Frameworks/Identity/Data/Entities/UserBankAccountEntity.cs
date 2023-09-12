using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Framework.Data.Entities {
    [Table("UserBankAccount", Schema = "dbo")]
    public class UserBankAccountEntity {
        [Key]
        public int BankAccountId { get; set; }
        public string AccountNumber { get; set; }
        public string Currency { get; set; }
        public int UserId { get; set; }
        public UserCredentialEntity UserCredential { get; set; }
    }
}
