using Transaction.Framework.Types;
namespace Transaction.Framework.Domain {
    public class AccountSummary {
        public string AccountNumber { get; set; }
        public Money Balance { get; set; }
    }
}
