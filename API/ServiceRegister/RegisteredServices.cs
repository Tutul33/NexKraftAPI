using API.BusinessLogic.Interface.Customer;
using API.BusinessLogic.Management.Customer;

namespace API.ServiceRegister
{
    public static class RegisteredServices
    {
        public static void Register(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ICustomerServices, CustomerMgt>();
        }
    }
}
