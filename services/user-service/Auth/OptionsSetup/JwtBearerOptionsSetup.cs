using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace user_service.OptionsSetup
{
    public class JwtBearerOptionsSetup : IConfigureOptions<JwtBearerOptions>
    {
        public JwtBearerOptionsSetup(IOptions<JwtOptions> jwtOptions)
        {

        }
        public void Configure(JwtBearerOptions options)
        {
                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidIssuer = "dotnet-user-jwts",
                    ValidateLifetime = true
                    //ValidAudience=""
                    // RoleClaimType="role",
                    //NameClaimType="name"
                };
        }
    }
}
