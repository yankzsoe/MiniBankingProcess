using Identity.Framework.Data.Entities;
using Identity.Framework.Data.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Framework.Data.Repositories {
    public class UserCredentialRepository : IUserCredentialRepository {
        public readonly ApplicationDbContext _dbContext;
        public readonly DbSet<UserCredentialEntity> _userCredentialEntity;
        public readonly ILogger<UserCredentialRepository> _logger;

        public UserCredentialRepository(ApplicationDbContext dbContext, ILogger<UserCredentialRepository> logger) {
            _dbContext = dbContext;
            _userCredentialEntity = _dbContext.Set<UserCredentialEntity>();
            _logger = logger;
        }

        public async Task<bool> CheckUsernameAsync(string username) {
            var result = await _userCredentialEntity.AsNoTracking()
                .FirstOrDefaultAsync(e => e.UserName.ToLower() == username.ToLower());
            return result is not null;
        }

        public async Task<(int, int)> CreateAsync(UserCredentialEntity userCredential, UserProfileEntity userProfile, UserBankAccountEntity userBankAccount) {
            using (var transactions = _dbContext.Database.BeginTransaction()) {
                try {
                    userCredential.UserProfile = userProfile;
                    userCredential.UserBankAccounts = new List<UserBankAccountEntity>() { userBankAccount };
                    _userCredentialEntity.Add(userCredential);
                    var result = await _dbContext.SaveChangesAsync();
                    transactions.Commit();
                    return await Task.FromResult((result, userCredential.UserId));
                } catch (Exception ex) {
                    _logger.LogError("An Error Occured: {0}", ex.Message);
                    transactions.Rollback();
                    throw;
                }
            }
        }

        public async Task<UserCredentialEntity> GetAsync(string username, string password) {
            return await _userCredentialEntity
                .Include(p => p.UserBankAccounts)
                .Include(p => p.UserProfile)
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.UserName == username && e.Password == password);
        }

        public Task<UserCredentialEntity> UpdateAsync(string username, string oldPassword, string newPassword) {
            throw new NotImplementedException();
        }
    }
}
