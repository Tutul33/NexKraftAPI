﻿using API.BusinessLogic.Interface.Customer;
using API.Data.ORM.DataModels;
using API.Data.ViewModels.Customers;
using API.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API.BusinessLogic.Management.Login
{
    public class LoginServices : ILoginServices
    {
        Data.ORM.DataModels.NexKraftDbContext _ctx;
        public LoginServices() { }
        public async Task<object> LoginUser(LoginCredential credential,string userAgent,string remoteIpAddress)
        {
            bool resstate = false; string token = string.Empty;
            try
            {
                using (_ctx = new Data.ORM.DataModels.NexKraftDbContext())
                {
                    UserLogin loggedUser = await _ctx.UserLogins.Where(u => u.UserName == credential.UserNmae && u.Password == credential.Password).FirstOrDefaultAsync();
                    if (loggedUser != null)
                    {
                        LoginModel loginModel = new LoginModel()
                        {
                            UserNmae = loggedUser.UserName,
                            Password = loggedUser.Password,
                            Email = (await _ctx.Customers.Where(x => x.CustomerId == loggedUser.CustomerId).FirstOrDefaultAsync())?.Email,
                            MachineName=userAgent,
                            RemoteIpAddress= remoteIpAddress,
                            UserID= Convert.ToInt32(loggedUser.CustomerId)
                        };
                        token = await GenerateJSONWebToken(loginModel);
                        resstate = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return new { jwtToken = token, isSuccess = resstate };
        }
        private async Task<string> GenerateJSONWebToken(LoginModel userInfo)
        {
            await Task.Yield();
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(StaticInfos.JwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
             {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.UserNmae),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString(), ClaimValueTypes.Integer64),
                new Claim(JwtRegisteredClaimNames.Email, userInfo.Email, ClaimValueTypes.String),
                new Claim("ipaddress", userInfo.RemoteIpAddress, ClaimValueTypes.String),
                new Claim("machinename", userInfo.MachineName, ClaimValueTypes.String),
                new Claim("userId", Convert.ToString(userInfo.UserID), ClaimValueTypes.String)
            };

            // Create the JWT and write it to a string
            var jwt = new JwtSecurityToken(
                issuer: StaticInfos.JwtIssuer,
                audience: StaticInfos.JwtAudience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(45),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

    }
}
