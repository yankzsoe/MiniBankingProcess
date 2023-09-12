namespace Transaction.WebApi.Models {
    public class RegisterModel {
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public string Currency { get; set; }
    }
}
