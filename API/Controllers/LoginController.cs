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
        public async Task<object> LoginUser(LoginCredential credential)
        {
            object result = null;
            try
            {
                var login = await loginServices.LoginUser(credential);
                if (login != null)
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
