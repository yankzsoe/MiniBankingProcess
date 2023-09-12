using System.Threading.Tasks;
using Transaction.Framework.Data.Entities;

namespace Transaction.Framework.Data.Interface {
    public interface IAccountSummaryRepository {
        Task<AccountSummaryEntity> Read(string accountNumber);
        Task<int> Insert(AccountSummaryEntity accountSummary);
    }
}
