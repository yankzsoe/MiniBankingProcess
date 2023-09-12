using Identity.Framework.Data.Entities;
using Identity.Framework.Data.Interface;
using Identity.Framework.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Framework.Data.Repositories {
    public class UserProfileRepository : IUserProfileRepository {
        public Task<UserProfileEntity> CretaeUserProfile(UserCredentialWithProfile userCredentialWithProfile) {
            throw new NotImplementedException();
        }

        public Task<UserProfileEntity> GetByUsernameAsync(string username) {
            throw new NotImplementedException();
        }
    }
}
