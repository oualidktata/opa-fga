using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace user_service.Auth
{
    public class CustomAuthOptions : AuthenticationSchemeOptions
    {

    }
    public class CustomAuthenticationHandler : AuthenticationHandler<CustomAuthOptions>
    {
        private readonly ICustomAuthenticationManager _authManager;
        public CustomAuthenticationHandler(IOptionsMonitor<CustomAuthOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            ICustomAuthenticationManager authManager) : base(options, logger, encoder, clock)
        {
            _authManager = authManager;

        }
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return AuthenticateResult.Fail("Sorry No authz header");
            }
            string authHeader = Request.Headers["Authorization"];
            if (authHeader.IsNullOrEmpty())
            {
                return AuthenticateResult.Fail("No token found");
            }
            if (!authHeader.StartsWith("Bearer", StringComparison.OrdinalIgnoreCase))
            {
                return AuthenticateResult.Fail("No token found");
            }
            
            var token = authHeader.Substring("Bearer".Length).Trim();
            if (token.IsNullOrEmpty())
            {
                return AuthenticateResult.Fail("No token found");
            }

            try
            {
                return ValidateToken(token);
            }
            catch (Exception ex)
            {

                return AuthenticateResult.Fail(ex.Message);
            }
        }
        private AuthenticateResult ValidateToken(string token)
        {

            var validatedToken = _authManager.Tokens
                var identity= new ClaimsIdentity(claims,Scheme.Name)
                return AuthenticateResult.Success(tickets)
        }

    }
}
