namespace Transaction.WebApi.Controllers {
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using Transaction.Framework.Domain;
    using Transaction.Framework.Services.Interface;
    using Transaction.Framework.Types;
    using Transaction.WebApi.Models;
    using Transaction.WebApi.Services;

    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase {
        private readonly ITransactionService _transactionService;
        private readonly IIdentityService _identityService;
        private readonly IMapper _mapper;
        private readonly IReceiveMessageService _receiveMessageService;

        public AccountController(ITransactionService transactionService, IIdentityService identityService, IMapper mapper, IReceiveMessageService receiveMessageService) {
            _transactionService = transactionService;
            _identityService = identityService;
            _mapper = mapper;
            _receiveMessageService = receiveMessageService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel registerModel) {
            var accountSummary = _mapper.Map<AccountSummary>(registerModel);
            var transactionResult = await _transactionService.Register(accountSummary);
            return Ok(_mapper.Map<TransactionResultModel>(transactionResult));
        }

        [HttpGet("balance")]
        public async Task<IActionResult> Balance() {
            var accountNumber = _identityService.GetIdentity().AccountNumber;
            var transactionResult = await _transactionService.Balance(accountNumber);
            return Ok(_mapper.Map<TransactionResultModel>(transactionResult));
        }

        [HttpPost("deposit")]
        public async Task<IActionResult> Deposit([FromBody] TransactionModel accountTransactionModel) {
            var accountTransaction = _mapper.Map<AccountTransaction>(accountTransactionModel);
            accountTransaction.TransactionType = TransactionType.Deposit;
            var result = await _transactionService.Deposit(accountTransaction);
            return Created(string.Empty, _mapper.Map<TransactionResultModel>(result));
        }

        [HttpPost("withdraw")]
        public async Task<IActionResult> Withdraw([FromBody] TransactionModel accountTransactionModel) {
            var accountTransaction = _mapper.Map<AccountTransaction>(accountTransactionModel);
            accountTransaction.TransactionType = TransactionType.Withdrawal;
            var result = await _transactionService.Withdraw(accountTransaction);
            return Created(string.Empty, _mapper.Map<TransactionResultModel>(result));
        }
    }
}
