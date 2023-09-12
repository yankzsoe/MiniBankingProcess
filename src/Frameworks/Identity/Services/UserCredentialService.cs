using AutoMapper;
using Identity.Framework.Data.Entities;
using Identity.Framework.Data.Interface;
using Identity.Framework.Data.Repositories;
using Identity.Framework.Domain;
using Identity.Framework.Exceptions;
using Identity.Framework.Helpers;
using Identity.Framework.Services.Interface;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Framework.Services {
    public class UserCredentialService : IUserCredentialService {
        private readonly IUserCredentialRepository _userCredentialRepository;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;

        public UserCredentialService(IUserCredentialRepository userCredentialRepository, IMapper mapper, IOptions<AppSettings> appSettings) {
            _userCredentialRepository = userCredentialRepository ?? throw new ArgumentNullException(nameof(userCredentialRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _appSettings = appSettings.Value;
        }

        public async Task<UserCredentialWithProfileResult> CreateUserAsync(UserCredentialWithProfile userCredentialWithProfile) {
            var result = await CreateUserCredentialWithProfile(userCredentialWithProfile);
            return result;
        }

        public async Task<Models.SecurityToken> LoginAsync(string username, string password) {
            var user = await _userCredentialRepository.GetAsync(username, password);
            if (user == null) {
                throw new InvalidUsernameOrPasswordException();
            }
            var userLogin = _mapper.Map<UserLogin>(user);
            var token = GenerateToken(userLogin);
            return token;
        }



        #region helper
        private async Task<UserCredentialWithProfileResult> CreateUserCredentialWithProfile(UserCredentialWithProfile userCredentialWithProfile) {
            var userCredential = _mapper.Map<UserCredentialEntity>(userCredentialWithProfile);
            var userProfile = _mapper.Map<UserProfileEntity>(userCredentialWithProfile);
            var userBankAccount = _mapper.Map<UserBankAccountEntity>(userCredentialWithProfile);
            var username = userCredential.UserName;
            if (await _userCredentialRepository.CheckUsernameAsync(username)) {
                throw new UsernameAlreadyExistException();
            }
            var result = await _userCredentialRepository.CreateAsync(userCredential, userProfile, userBankAccount);
            userCredentialWithProfile.UserId = result.Item2;
            return _mapper.Map<UserCredentialWithProfileResult>(userCredentialWithProfile);
        }

        public Models.SecurityToken GenerateToken(UserLogin user) {
            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("accountnumber", user.AccountNumber.ToString()),
                    new Claim("currency", user.Currency),
                    new Claim("name", user.FullName)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtSecurityToken = tokenHandler.WriteToken(token);

            return new Models.SecurityToken() { auth_token = jwtSecurityToken };
        }
        #endregion
    }
}
