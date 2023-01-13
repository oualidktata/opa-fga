using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;

namespace user_service.Auth
{
    public interface ICustomAuthenticationManager
    {
        public IDictionary<string, string> Tokens;
        public string Authenticate(string user);

        
    }

    public class CustomAuthenticationManager : ICustomAuthenticationManager
    {
        private readonly IDictionary<string, string> _tokens = new Dictionary<string, string>();

        public IDictionary<string, string> Tokens;

        public string Authenticate(string user)
        {
            var tokenOualid = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6Im91YWxpZGt0YXRhIiwic3ViIjoib3VhbGlka3RhdGEiLCJqdGkiOiJlNjY1NzkzOCIsInNjb3BlIjoidXNlci1hcGkiLCJyb2xlIjoiYWRtaW4iLCJhdWQiOiJodHRwczovL2xvY2FsaG9zdDo1MDAxIiwibmJmIjoxNjczNTU3NTMwLCJleHAiOjE2ODEzMzM1MzAsImlhdCI6MTY3MzU1NzUzMSwiaXNzIjoiZG90bmV0LXVzZXItand0cyJ9.JHlZYHfHTqbrvgGOfJcAiEaK3SJRg01QLA4A919MFJw";
            var tokenWalter = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6Im91YWxpZGt0YXRhIiwic3ViIjoib3VhbGlka3RhdGEiLCJqdGkiOiJlNjY1NzkzOCIsInNjb3BlIjoidXNlci1hcGkiLCJyb2xlIjoiYWRtaW4iLCJhdWQiOiJodHRwczovL2xvY2FsaG9zdDo1MDAxIiwibmJmIjoxNjczNTU3NTMwLCJleHAiOjE2ODEzMzM1MzAsImlhdCI6MTY3MzU1NzUzMSwiaXNzIjoiZG90bmV0LXVzZXItand0cyJ9.JHlZYHfHTqbrvgGOfJcAiEaK3SJRg01QLA4A919MFJw";
            
            if (user.ToLowerInvariant() == "oualid".ToLower())
            {
                if (_tokens.ContainsKey(user))
                {
                    _tokens.Add(user, tokenOualid);
                }
                
                return _tokens.Where(x => x.Key == user).FirstOrDefault().Value;
            }

            if(user.ToLowerInvariant() == "walter".ToLower())
            {
                if (_tokens.ContainsKey(user))
                {
                    _tokens.Add(user, tokenWalter);
                }

                return _tokens.Where(x => x.Key == user).FirstOrDefault().Value;
            }
            return null;
        }
    }
}