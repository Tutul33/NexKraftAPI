using API.RepositoryManagement.UnityOfWork.Interfaces;
using API.RepositoryManagement.UnityOfWork;
using API.Data.ADO.NET;
using API.Data.MySQL;
using API.Data.ORM.MsSQLDataModels;
using API.Data.PostGreSQL;
using API.ServiceRegister;
using API.Settings;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

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
StaticInfos.JwtKey = _configuration.GetValue<string>("Jwt:Key");
StaticInfos.JwtIssuer = _configuration.GetValue<string>("Jwt:Issuer");
StaticInfos.JwtAudience = _configuration.GetValue<string>("Jwt:Audience");

//With a transient service, a new instance is provided every time an instance is requested
//whether it is in the scope of same http request or across different http requests.
builder.Services.AddTransient(_ => new MySqlDbConnection(StaticInfos.MySqlConnectionString));
builder.Services.AddTransient(_ => new MsSqlDbConnection(StaticInfos.MsSqlConnectionString));
builder.Services.AddTransient(_ => new PostGreSqlDbConnection(StaticInfos.PostgreSqlConnectionString));
builder.Services.AddDbContext<NexKraftDbContext>(options => options.UseSqlServer(StaticInfos.MsSqlConnectionString));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Configure In Memory Cache
builder.Services.AddMemoryCache();

//Configure Response Caching
builder.Services.AddResponseCaching(options =>
{
    //Cache responses with a body size smaller than or equal to 1,024 bytes.
    options.MaximumBodySize = 1024;
    //Store the responses by case-sensitive paths.
    //For example, /page1 and /Page1 are stored separately.
    options.UseCaseSensitivePaths = true;
});

//Register All services
RegisteredServices.Register(builder);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();

//UseCors must be called before UseResponseCaching
app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.UseResponseCaching();
app.Use(async (context, next) =>
{
    context.Response.GetTypedHeaders().CacheControl =
        new Microsoft.Net.Http.Headers.CacheControlHeaderValue()
        {
            Public = true,
            MaxAge = TimeSpan.FromSeconds(10)
        };
    context.Response.Headers[Microsoft.Net.Http.Headers.HeaderNames.Vary] =
        new string[] { "Accept-Encoding" };

    await next();
});

app.UseAuthentication();
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
