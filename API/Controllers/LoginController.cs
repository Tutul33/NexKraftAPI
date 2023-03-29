using API.BusinessLogic.Interface.Customer;
using API.BusinessLogic.Management.Login;
using API.Data.ViewModels.Customers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]"), Produces("application/json"), EnableCors]
    [ApiController]
    public class LoginController : ControllerBase
    {
        ILoginServices loginServices;
        public LoginController(ILoginServices _loginServices)
        {
            loginServices = _loginServices;
        }
        [HttpPost("[action]")]
        public async Task<object> UserLogin(LoginCredential credential)
        {
            object result = new object();
            try
            {
                string userAgent = Request.Headers["User-Agent"].ToString();
                string? RemoteIpAddress = HttpContext.Connection.RemoteIpAddress.ToString();

                result = await loginServices.LoginUser(credential, userAgent,RemoteIpAddress);
                if (result == null)
                {
                    result = new { message = "User not foud.", resstate = false };
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return new { result };
        }
        [HttpPost("[action]")]
        public async Task<object> GenerateNewToken(LoginModel model)
        {
            string token = string.Empty;object result = new object();
            try
            {
                string userAgent = Request.Headers["User-Agent"].ToString();
                string RemoteIpAddress = HttpContext.Connection.RemoteIpAddress.ToString();

                token = await loginServices.GenerateNewToken(model,userAgent,RemoteIpAddress);
                if (string.IsNullOrEmpty(token))
                {
                    result = new { message = "User is not exist.", resstate = false };
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return new { token };
        }
    }
}
