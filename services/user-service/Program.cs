using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using MongoDB.Driver;
using Redis.OM;
using Redis.OM.Contracts;
using Redis.OM.Modeling;
using StackExchange.Redis;
using user_service.Auth.OptionsSetup;
using user_service.Handlers;
using user_service.HostedServices;
using user_service.OptionsSetup;
using user_service_core;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddStackExchangeRedisCache(options =>
//   {
//      options.Configuration = "127.0.0.1:6379"; // redis is the container name of the redis service. 6379 is the default port
//      options.InstanceName = "SampleInstance";
//   });
//TypeAdapterConfig<AddUserCommandHandler.AddUserCommand, UserDTO>.NewConfig()
//    .Ignore(dest => dest.Id)
//    .Compile();
builder.Services.AddMediatR(typeof(Program).Assembly);
//builder.Services.AddTransient<IRequestHandler<AddUserCommandHandler.AddUserCommand, AddUserCommandHandler.AddUserResponse>, AddUserCommandHandler>();

builder.Services.AddHostedService<IndexCreationService>();
builder.Services.AddSingleton(new RedisConnectionProvider(builder.Configuration.GetConnectionString("Redis-Stack")));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    // .AddCookie("cookie")
    .AddJwtBearer();
//Clean by extracting Jwtoptions and jwtBearersetup
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));
builder.Services.ConfigureOptions<JwtOptionsSetup>();
builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("can-list-users", policy =>
    policy
    .RequireAuthenticatedUser()
    .RequireRole("admin")
    .RequireClaim("user-api"));

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
