using Identity.Framework.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Framework.Services.Interface {
    public interface IUserCredentialService {
        Task<UserCredentialWithProfileResult> CreateUserAsync(UserCredentialWithProfile userCredentialWithProfile);
        Task<Models.SecurityToken> LoginAsync(string username, string password);
    }
}
