using API.BusinessLogic.Interface.Customer;
using API.BusinessLogic.Management.Customer;
using API.BusinessLogic.Management.Login;

namespace API.ServiceRegister
{
    public static class RegisteredServices
    {
        public static void Register(WebApplicationBuilder builder)
        {
            //With a scoped service we get the same instance within the scope of a given http request
            //but a new instance across different http requests.
            //Dependency Injection of Services
            //Injecting DA(Data Access) layer into the BL(Business Logic) layer.It will set Loosely coupling and increase Testability.
            builder.Services.AddScoped<ICustomerServices, CustomerMgt>();
            builder.Services.AddScoped<ILoginServices, LoginServices>();

        }
    }
}
