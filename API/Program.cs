using API.BusinessLogic.Interface.Customer;
using API.BusinessLogic.Management.Customer;
using API.Data.ADO.NET;
using API.Data.MySQL;
using API.Data.PostGreSQL;
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
builder.Services.AddTransient(_ => new MySqlDbConnection(StaticInfos.MySqlConnectionString));
builder.Services.AddTransient(_ => new MsSqlDbConnection(StaticInfos.MsSqlConnectionString));
builder.Services.AddTransient(_ => new PostGreSqlDbConnection(StaticInfos.PostgreSqlConnectionString));

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

//Uncomment below for index.html
//DefaultFilesOptions options = new DefaultFilesOptions();
//options.DefaultFileNames.Clear();
//options.DefaultFileNames.Add("/index.html");
//app.UseDefaultFiles(options);

//Uncomment below code for-- static files, such as HTML, CSS, images, and JavaScript, are assets an ASP.NET Core app serves directly to clients by default.
//app.UseStaticFiles();

//Uncomment below code for-- Enable all static file middleware (except directory browsing) for the current request path in the current directory.
//app.UseFileServer(enableDirectoryBrowsing: false);

app.Run();
