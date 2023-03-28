using API.BusinessLogic.Interface.Customer;
using API.BusinessLogic.Management.Login;
using API.Data.ViewModels.Customers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
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
            object result = null;
            try
            {
                var userAgent = Request.Headers["User-Agent"];
                var RemoteIpAddress = HttpContext.Connection.RemoteIpAddress.ToString();

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
    }
}
