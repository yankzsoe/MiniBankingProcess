using AutoMapper;
using Identity.Framework.Domain;
using Identity.Framework.Services;
using Identity.Framework.Services.Interface;
using Identity.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Identity.WebApi.Controllers {
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase {
        //private IUserService _userService;
        private IUserCredentialService _userCredentialService;
        private readonly ISendMessageQueueService _sendMessageQueueService;
        private IMapper _mapper;

        public UserController(IUserCredentialService userCredentialService, IMapper mapper, ISendMessageQueueService sendMessageQueueService) {
            //_userService = userService;
            _userCredentialService = userCredentialService;
            _mapper = mapper;
            _sendMessageQueueService = sendMessageQueueService;
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> CreateUser([FromBody] UserCredentialWithProfileModel userParam) {
            var userCredential = _mapper.Map<UserCredentialWithProfile>(userParam);
            var result = await _userCredentialService.CreateUserAsync(userCredential);
            var modelRegister = new RegisterAccountSummaryModel() {
                AccountNumber = userParam.BankAccount.AccountNumber,
                Balance = userParam.BankAccount.Deposit,
                Currency = userParam.BankAccount.Currency,
            };
            _ = _sendMessageQueueService.SendMessageAsync(modelRegister);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] Login loginParam) {
            var token = await _userCredentialService.LoginAsync(loginParam.Username, loginParam.Password);
            return Ok(token);
        }
    }
}
