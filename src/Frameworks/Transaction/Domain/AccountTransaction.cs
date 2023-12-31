﻿using Transaction.Framework.Types;

namespace Transaction.Framework.Domain {
    public class AccountTransaction {
        public string AccountNumber { get; set; }
        public TransactionType TransactionType { get; set; }
        public Money Amount { get; set; }
    }
}
