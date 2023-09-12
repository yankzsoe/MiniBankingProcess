using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Framework.Domain {
    public class UserCredentialWithProfileResult {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public DateOnly Dob { get; set; }
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
    }
}
