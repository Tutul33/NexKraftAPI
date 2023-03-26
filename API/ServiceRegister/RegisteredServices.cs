using API.BusinessLogic.Interface.Customer;
using API.BusinessLogic.Management.Customer;

namespace API.ServiceRegister
{
    public static class RegisteredServices
    {
        public static void Register(WebApplicationBuilder builder)
        {
            //With a scoped service we get the same instance within the scope of a given http request
            //but a new instance across different http requests.
            builder.Services.AddScoped<ICustomerServices, CustomerMgt>();

        }
    }
}
