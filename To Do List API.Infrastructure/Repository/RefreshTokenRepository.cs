﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NuGet.Common;
using System.Security.Cryptography;
using To_Do_List_API.DTO;
using To_Do_List_API.Helpers;
using To_Do_List_API.Infrastructure.Repository;
using To_Do_List_API.Models;
using To_Do_List_API.Repository.abstraction_layer;

namespace To_Do_List_API.Repository
{
    public class RefreshTokenRepository : BaseRepository<RefreshToken> ,  IRefreshTokenRepository
    {

        AppDbContext context;
        private readonly UserManager<User> userManager;

        public RefreshTokenRepository(AppDbContext _context , UserManager<User> userManager) : base(_context)
        {
            context = _context;
            this.userManager = userManager;
        }

        public async Task<QueryResultDto<RefreshToken>> GetActiveRefreshTokenAsync(IUserBase user)
        {
            if (user is null) new QueryResultDto<RefreshToken> { IsCompleteSuccessfully = false, ErrorMessages = ErrorMessageUserConst.IncorrectInput };

            var result = new RefreshToken();
            if (user.RefreshTokens is not null && user.RefreshTokens.Any(t => t.IsActive))
            {
                var activeRefreshToken = user.RefreshTokens.FirstOrDefault(t => t.IsActive);
                result.Token = activeRefreshToken.Token;
                result.ExpiresOn = activeRefreshToken.ExpiresOn;

            }
            else
            {
                var refreshToken = GenerateRefreshToken();
                result.Token = refreshToken.Token;
                result.ExpiresOn = refreshToken.ExpiresOn;
                user.RefreshTokens.Add(refreshToken);
                await userManager.UpdateAsync((User)user);
            }

            return new QueryResultDto<RefreshToken>
            {
                IsCompleteSuccessfully = true,
                Result = result
            };


        }

        public async Task<QueryResultDto<IUserBase>> RevokeRefreshTokenAsync(string token)
        {
            var user = await userManager.Users.SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == token));

            if (user == null)
                return new QueryResultDto<IUserBase>() { IsCompleteSuccessfully = false, ErrorMessages = ErrorMessageUserConst.InvalidToken };


            var refreshToken = user.RefreshTokens.Single(t => t.Token == token);
           
            if (!refreshToken.IsActive)
                return new QueryResultDto<IUserBase>() { IsCompleteSuccessfully = false, ErrorMessages = ErrorMessageUserConst.InvalidToken };


            refreshToken.RevokedOn = DateTime.UtcNow;

            try
            {
                await userManager.UpdateAsync(user);
            }
            catch (Exception)
            {
                return new QueryResultDto<IUserBase>() { IsCompleteSuccessfully = false, ErrorMessages = ErrorMessageUserConst.Unexpected};

            }

            return new QueryResultDto<IUserBase>() { IsCompleteSuccessfully = true , Result = user };

        }


        private RefreshToken GenerateRefreshToken()
        {
            var randomNumber = new byte[32];

            using var generator = new RNGCryptoServiceProvider();

            generator.GetBytes(randomNumber);

            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomNumber),
                ExpiresOn = DateTime.UtcNow.AddDays(10),
                CreatedOn = DateTime.UtcNow
            };
        }

       
    }
}



