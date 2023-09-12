using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Framework.Data.Entities {
    [Table("UserCredential", Schema = "dbo")]
    public class UserCredentialEntity {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public UserProfileEntity UserProfile { get; set; }
        public ICollection<UserBankAccountEntity> UserBankAccounts { get; set; }
    }
}
