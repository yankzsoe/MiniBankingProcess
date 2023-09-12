namespace Transaction.Framework.Data.Repositories {
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;
    using Transaction.Framework.Data.Entities;
    using Transaction.Framework.Data.Interface;

    public class AccountSummaryRepository : IAccountSummaryRepository {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<AccountSummaryEntity> _accountSummaryEntity;

        public AccountSummaryRepository(ApplicationDbContext dbContext) {
            _dbContext = dbContext;
            _accountSummaryEntity = _dbContext.Set<AccountSummaryEntity>();
        }

        public async Task<int> Insert(AccountSummaryEntity accountSummary) {
            await _accountSummaryEntity.AddAsync(accountSummary);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<AccountSummaryEntity> Read(string accountNumber) {
            return await _accountSummaryEntity.AsNoTracking()
                .FirstOrDefaultAsync(e => e.AccountNumber == accountNumber);
        }
    }
}
