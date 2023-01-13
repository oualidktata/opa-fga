using System.Security.Principal;

namespace user_service.OptionsSetup
{
    public class JwtOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
