using Microsoft.AspNetCore.Authentication.JwtBearer;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddStackExchangeRedisCache(options =>
//   {
//      options.Configuration = "127.0.0.1:6379"; // redis is the container name of the redis service. 6379 is the default port
//      options.InstanceName = "SampleInstance";
//   });


builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
ConnectionMultiplexer.Connect(new ConfigurationOptions
{
    EndPoints = { "redis:6379" },
    Ssl = false,
    //ResolveDns = true
}));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddCookie("cookie")
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters.RoleClaimType = "role";
        options.TokenValidationParameters.NameClaimType = "name";
    });

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("can-read-fleet", policy =>
    policy
    .RequireAuthenticatedUser()
    .RequireRole("admin")
    .RequireClaim("greeting_api"));

//builder.Services.AddAuthorizationBuilder()
//    .AddPolicy("admin_greetings",PolicyServiceCollect)
//{
//    options.Enable = true;
//    options.BaseAddress = "http://localhost:8181";
//    options.PolicyPath = "/v1/data/ads";
//    options.AllowOnFailure = false;
//    options.Timeout = 5;
//});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
