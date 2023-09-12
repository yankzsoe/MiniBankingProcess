using Identity.Framework.Data.Entities;
using Identity.Framework.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Framework.Data.Interface {
    public interface IUserProfileRepository {
        Task<UserProfileEntity> GetByUsernameAsync(string username);
        Task<UserProfileEntity> CretaeUserProfile(UserCredentialWithProfile userCredentialWithProfile);
    }
}
