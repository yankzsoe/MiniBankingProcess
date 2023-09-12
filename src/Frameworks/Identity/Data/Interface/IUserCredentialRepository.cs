using Identity.Framework.Data.Entities;
using System.Threading.Tasks;

namespace Identity.Framework.Data.Interface {
    public interface IUserCredentialRepository {
        Task<(int, int)> CreateAsync(UserCredentialEntity userCredential, UserProfileEntity userProfile, UserBankAccountEntity userBankAccount);
        Task<UserCredentialEntity> GetAsync(string username, string password);
        Task<bool> CheckUsernameAsync(string username);
        Task<UserCredentialEntity> UpdateAsync(string username, string oldPassword, string newPassword);
    }
}
