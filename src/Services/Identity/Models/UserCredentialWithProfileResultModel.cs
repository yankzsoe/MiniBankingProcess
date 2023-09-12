using System;

namespace Identity.WebApi.Models {
    public class UserCredentialWithProfileResultModel {
        public string Username { get; set; }
        public string FullName { get; set; }
        public DateOnly Dob { get; set; }
        public bool IsSuccessfully { get; set; }
        public string Message { get; set; }

    }
}
