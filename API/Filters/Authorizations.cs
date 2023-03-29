using API.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text;

namespace API.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class Authorizations : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                string BearerToken = context.HttpContext.Request.Headers["Authorization"].ToString();
                if (!string.IsNullOrEmpty(BearerToken))
                {
                    string[] authorizedToken = BearerToken.Split(" ");
                    if (!ValidateToken(authorizedToken[1]) || string.IsNullOrEmpty(authorizedToken[1]))
                    {
                        context.Result = new ContentResult()
                        {
                            Content = Newtonsoft.Json.JsonConvert.SerializeObject(new { message = "Unauthorize access." }),
                            StatusCode = (int)HttpStatusCode.Unauthorized,
                            ContentType = "application/json"
                        };
                    }
                }
                else
                {
                    context.Result = new ContentResult()
                    {
                        Content = Newtonsoft.Json.JsonConvert.SerializeObject(new { message = "Invalid token." }),
                        StatusCode = (int)HttpStatusCode.Unauthorized,
                        ContentType = "application/json"
                    };
                }
            }
            catch (Exception ex)
            {
                context.Result = new ContentResult()
                {
                    Content = Newtonsoft.Json.JsonConvert.SerializeObject(new { message = "Error.Please provide valid data." }),
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    ContentType = "application/json"
                };
            }

        }
        public bool ValidateToken(string token)
        {
            bool isValid = false;
            if (token == null)
                return isValid;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(StaticInfos.JwtKey);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                //var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "userId").Value);                
                if (jwtToken.ValidTo >= DateTime.UtcNow)
                {
                    isValid = true;
                }
                else
                {
                    isValid = false;
                }
                // return true from JWT token if validation successful
                return isValid;
            }
            catch
            {
                // return null if validation fails
                return isValid;
            }
        }
    }
}
