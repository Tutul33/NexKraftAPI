using API.BusinessLogic.Interface.Customer;
using API.BusinessLogic.Management.Customer;
using API.ServiceRegister;
using API.Settings;

var builder = WebApplication.CreateBuilder(args);

//Access AppSettings.JSON
var configBuilder = new ConfigurationBuilder()
                       .SetBasePath(Directory.GetCurrentDirectory())
                       .AddJsonFile("appSettings.json", optional: true, reloadOnChange: true);
IConfiguration _configuration = configBuilder.Build();
StaticInfos.MsSqlConnectionString = _configuration.GetValue<string>("MsSqlConnectionString");
StaticInfos.MySqlConnectionString = _configuration.GetValue<string>("MySqlConnectionString");
StaticInfos.PostgreSqlConnectionString = _configuration.GetValue<string>("PostGreSqlConnectionString");
StaticInfos.IsMsSQL = _configuration.GetValue<bool>("IsMsSQL");
StaticInfos.IsMySQL = _configuration.GetValue<bool>("IsMySQL");
StaticInfos.IsPostgreSQL = _configuration.GetValue<bool>("IsPostgreSQL");

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Register All services
RegisteredServices.Register(builder);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
