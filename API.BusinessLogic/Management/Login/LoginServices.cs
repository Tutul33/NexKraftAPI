using API.BusinessLogic.Base;
using API.BusinessLogic.Interface.Customer;
using API.Data.ORM.DataModels;
using API.Data.ViewModels.Customers;
using API.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
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
    public class LoginServices : BaseRepository<UserLogin>, ILoginServices
    {
        private ICustomerServices service; Data.ORM.DataModels.NexKraftDbContext? _ctx = null;
        private NexKraftDbContext AppDbContext => _dbContext as NexKraftDbContext;
        //public LoginServices(ICustomerServices _service)
        //{
        //    service = _service;
        //}
        public LoginServices(NexKraftDbContext dbContext) : base(dbContext)
        {

        }
        //public async Task<object> LoginUser(LoginCredential credential, string userAgent, string remoteIpAddress)
        //{
        //    bool resstate = false; string token = string.Empty;
        //    try
        //    {
        //        using (_ctx = new Data.ORM.DataModels.NexKraftDbContext())
        //        {
        //            UserLogin? loggedUser = await _ctx.UserLogins.Where(u => u.UserName == credential.UserName && u.Password == credential.Password).FirstOrDefaultAsync();
        //            if (loggedUser != null)
        //            {
        //                LoginModel loginModel = new LoginModel()
        //                {
        //                    UserName = loggedUser.UserName,
        //                    Password = loggedUser.Password,
        //                    Email = (await _ctx.Customers.Where(x => x.CustomerId == loggedUser.CustomerId).FirstOrDefaultAsync())?.Email,
        //                    MachineName = userAgent,
        //                    RemoteIpAddress = remoteIpAddress,
        //                    CustomerID = Convert.ToInt32(loggedUser.CustomerId)
        //                };
        //                token = await GenerateJSONWebToken(loginModel);
        //                resstate = true;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.ToString();
        //    }
        //    return new { jwtToken = token, isSuccess = resstate };
        //}

        //public async Task<string> GenerateNewToken(LoginModel userInfo, string userAgent, string remoteIpAddress)
        //{
        //    string generatedToken = ""; _ctx = new Data.ORM.DataModels.NexKraftDbContext();
        //    try
        //    {
        //        UserLogin? loggedUser = await _ctx.UserLogins.Where(u => u.CustomerId == userInfo.CustomerID).FirstOrDefaultAsync();
        //        if (loggedUser != null)
        //        {
        //            LoginModel loginModel = new LoginModel()
        //            {
        //                UserName = loggedUser.UserName,
        //                Password = loggedUser.Password,
        //                Email = (await _ctx.Customers.Where(x => x.CustomerId == loggedUser.CustomerId).FirstOrDefaultAsync())?.Email,
        //                MachineName = userAgent,
        //                RemoteIpAddress = remoteIpAddress,
        //                CustomerID = Convert.ToInt32(loggedUser.CustomerId)
        //            };
        //            generatedToken = await GenerateJSONWebToken(userInfo);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.ToString();
        //    }
        //    return generatedToken;
        //}

        private async Task<string> GenerateJSONWebToken(LoginModel userInfo)
        {
            await Task.Yield();
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(StaticInfos.JwtKey));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            Claim[] claims = new[]
             {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString(), ClaimValueTypes.Integer64),
                new Claim(JwtRegisteredClaimNames.Email, userInfo.Email, ClaimValueTypes.String),
                new Claim("ipaddress", userInfo.RemoteIpAddress, ClaimValueTypes.String),
                new Claim("machinename", userInfo.MachineName, ClaimValueTypes.String),
                new Claim("userId", Convert.ToString(userInfo.CustomerID), ClaimValueTypes.String)
            };

            // Create the JWT and write it to a string
            JwtSecurityToken jwt = new JwtSecurityToken(
                issuer: StaticInfos.JwtIssuer,
                audience: StaticInfos.JwtAudience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(45),//This token will expire after 45 minutes
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        public async Task<object> LoginUser(LoginCredential credential, string userAgent, string remoteIpAddress)
        {
            bool resstate = false; string token = string.Empty;
            try
            {

                //UserLogin? loggedUser = await _ctx.UserLogins.Where(u => u.UserName == credential.UserName && u.Password == credential.Password).FirstOrDefaultAsync();
                IEnumerable<UserLogin>? loggedUsers = await GetManyAsync(filter: u => u.UserName == credential.UserName && u.Password == credential.Password);
                var loggedUser=loggedUsers.FirstOrDefault();
                if (loggedUser != null)
                {
                    LoginModel loginModel = new LoginModel()
                    {
                        UserName = loggedUser.UserName,
                        Password = loggedUser.Password,
                        Email = "",//(await _ctx.Customers.Where(x => x.CustomerId == loggedUser.CustomerId).FirstOrDefaultAsync())?.Email,
                        MachineName = userAgent,
                        RemoteIpAddress = remoteIpAddress,
                        CustomerID = Convert.ToInt32(loggedUser.CustomerId)
                    };
                    token = await GenerateJSONWebToken(loginModel);
                    resstate = true;
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return new { jwtToken = token, isSuccess = resstate };
        }

        public Task<string> GenerateNewToken(LoginModel userInfo, string userAgent, string remoteIpAddress)
        {
            throw new NotImplementedException();
        }
    }
}
