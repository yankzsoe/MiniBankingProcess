namespace Transaction.WebApi.Models {
    public class TransactionResultModel {
        public string AccountNumber { get; set; }
        public bool IsSuccessful { get; set; }
        public decimal? Balance { get; set; }
        public string Currency { get; set; }
        public string Message { get; set; }
    }
}
