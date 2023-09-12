using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Identity.Framework.Data.Entities {
    [Table("UserProfile", Schema = "dbo")]
    public class UserProfileEntity {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public DateOnly Dob { get; set; }

        public UserCredentialEntity UsersEntity { get; set; }
    }
}
