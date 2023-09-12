using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transaction.Framework.Data.Interface {
    public interface IUnitOfWork : IAsyncDisposable{
        IAccountSummaryRepository AccountSummaryRepository { get; }
        IAccountTransactionRepository AccountTransactionRepository { get; }

        Task<int> CompleteAsync();
    }
}
