using API.BusinessLogic.Interface.Customer;
using API.BusinessLogic.Management.Customer;

namespace API.ServiceRegister
{
    public static class RegisteredServices
    {
        public static void Register(WebApplicationBuilder builder)
        {
            //Service Register
            builder.Services.AddScoped<ICustomerServices, CustomerMgt>();
            //builder.Services.AddTransient<AppDb>(_ => new AppDb(Configuration["ConnectionStrings:DefaultConnection"]));

        }
    }
}
