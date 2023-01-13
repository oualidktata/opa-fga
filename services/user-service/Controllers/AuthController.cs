using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using user_service.Auth;

namespace users_api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthController : ControllerBase
    {
        private static readonly string[] Fleets = new[]
        {
        "Fleet1", "Fleet2", "Fleet3", "Fleet4", "Fleet5"
    };

        private readonly ILogger<AuthController> _logger;
        private readonly ICustomAuthenticationManager _authenticationManager;

        public AuthController(ILogger<AuthController> logger,ICustomAuthenticationManager authenticationManager)
        {
            _logger = logger;
            _authenticationManager = authenticationManager;
        }

        [HttpGet(Name = "GetClaims")]
        public IEnumerable<Claim> Get()
        {
            var claims = new List<Claim>();


            return (IEnumerable<Claim>)Ok(claims);
        }

        [HttpPost()]
        public IActionResult Authenticate() {
            var token=_authenticationManager.Authenticate("oualid");
            return Ok(token);
        }
        
        [HttpGet(Name = "login")]
        public IResult login()
        {
            var claims = new List<Claim>();


            return Results.Challenge(authenticationSchemes:new List<string>() { "okta","github"});
        }
        [HttpGet(Name = "user")]
        public IActionResult user(HttpContext ctx)
        {
            var claims = new List<Claim>();


            return Ok(ctx.User.Claims.Select(x=>new {x.Type,x.Value}).ToList());
        }
    }
}