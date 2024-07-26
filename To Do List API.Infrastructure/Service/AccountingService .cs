using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using To_Do_List_API.Core.Repository_Abstraction_Layer.UnitOfWork;
using To_Do_List_API.DTO;
using To_Do_List_API.Helpers;
using To_Do_List_API.Models;
using To_Do_List_API.Repository.abstraction_layer;
using To_Do_List_API.Service.abstraction_layer;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace To_Do_List_API.Service
{
    public class AccountingService : IAccountingService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper mapper;
        private readonly IRepositoryUnitOfWork repositoryUnitOfWork;
        private readonly JWT _jwt;
        public AccountingService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IOptions<JWT> jwt,IMapper mapper , IRepositoryUnitOfWork repositoryUnitOfWork)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            this.mapper = mapper;
            this.repositoryUnitOfWork = repositoryUnitOfWork;
            _jwt = jwt.Value;
        }

        public async Task<QueryResultDto<AccountDto>> RegisterAsync(RegisterDto registerDto)
        {
            if (await _userManager.FindByEmailAsync(registerDto.Email) is not null)
                return new QueryResultDto<AccountDto>() { IsCompleteSuccessfully = false, ErrorMessages = ErrorMessageUserConst.EmailAlreadyRegistered };

            var user = mapper.Map<User>(registerDto);
            user.UserName = registerDto.Email;
            user.Image = ""; // hint
            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                var errors = string.Empty;
                foreach (var error in result.Errors) errors += $"{error.Description},";

                return new QueryResultDto<AccountDto>() { IsCompleteSuccessfully = false, ErrorMessages = ErrorMessageUserConst.Custom(400 , errors) };
            }


            return await CreateAccount(user);

            
        }

        public async Task<QueryResultDto<AccountDto>> LoginAsync(LoginDto model)
        {

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user is null || !await _userManager.CheckPasswordAsync(user, model.Password))
                return new QueryResultDto<AccountDto>() { IsCompleteSuccessfully = false, ErrorMessages = ErrorMessageUserConst.incorrectEmailOrPassword};

            return await CreateAccount(user);
        }

        public async Task<QueryResultDto<AccountDto>> RefreshTokenAsync(string token)
        {
            
            var result = await repositoryUnitOfWork.RefreshTokens.RevokeRefreshTokenAsync(token);

            if (!result.IsCompleteSuccessfully)

                return new QueryResultDto<AccountDto>()
                {
                    IsCompleteSuccessfully = result.IsCompleteSuccessfully,
                    ErrorMessages = result.ErrorMessages
                };


            var user = result.Result;
            return await CreateAccount((User)user);

        }

        public async Task<QueryResultDto<bool>> RevokeTokenAsync(string token)
        {
            var result = await repositoryUnitOfWork.RefreshTokens.RevokeRefreshTokenAsync(token);

            if (!result.IsCompleteSuccessfully)
                
                return new QueryResultDto<bool>()
                {
                    IsCompleteSuccessfully = result.IsCompleteSuccessfully,
                    ErrorMessages = result.ErrorMessages,
                    Result = result.IsCompleteSuccessfully
                };

            return new QueryResultDto<bool>()
            {
                IsCompleteSuccessfully = result.IsCompleteSuccessfully,
                ErrorMessages = result.ErrorMessages,
                Result = result.IsCompleteSuccessfully
            };
        }



        private async Task<JwtSecurityToken> CreateJwtToken(User user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Name, user.DisplayName)
            }
            .Union(userClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwt.DurationInMinutes),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }


        private async Task<QueryResultDto<AccountDto>> CreateAccount(User user)
        {
            var jwtSecurityToken = await CreateJwtToken(user);

            QueryResultDto<RefreshToken> refreshTokenResult = await repositoryUnitOfWork.RefreshTokens.GetActiveRefreshTokenAsync(user);

            if (!refreshTokenResult.IsCompleteSuccessfully)
                return new QueryResultDto<AccountDto>()
                {
                    IsCompleteSuccessfully = false,
                    ErrorMessages = refreshTokenResult.ErrorMessages
                };


            var account = new AccountDto
            {
                Email = user.Email,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                DisplayName = user.DisplayName,
                RefreshToken = refreshTokenResult.Result.Token,
                RefreshTokenExpiration = refreshTokenResult.Result.ExpiresOn,
                IsAuthenticated = true
            };


            return new QueryResultDto<AccountDto>()
            {
                IsCompleteSuccessfully = true,
                Result = account
            };
        }



    }
}
