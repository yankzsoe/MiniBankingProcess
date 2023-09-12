using Identity.Framework.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Framework.Exceptions {
    public abstract class IdentityException : Exception {
        public IdentityException(string message)
            : base(message) { }

        public abstract int ErrorCode { get; }
    }

    public class InvalidUsernameOrPasswordException : IdentityException {
        public InvalidUsernameOrPasswordException() : base("Invalid Username or Password.") {
        }

        public override int ErrorCode => IdentityErrorCode.InvalidUsernameOrPasswordError;
    }

    public class UsernameAlreadyExistException : IdentityException {
        public UsernameAlreadyExistException() 
            : base("Username already exist.") { }
        public override int ErrorCode => IdentityErrorCode.InvalidUsernameOrPasswordError;
    }

}
