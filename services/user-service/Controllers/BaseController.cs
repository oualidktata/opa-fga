using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using Newtonsoft.Json;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json.Linq;

namespace user_service.Controllers
{
    public class BaseController : ControllerBase
    {
        private readonly IDatabase _cache;

        public BaseController(IConnectionMultiplexer connectionMultiplexer)
        {
            _cache = connectionMultiplexer.GetDatabase();
        }

        protected async Task<T> CacheResponseAsync<T>(string cacheKey, Func<Task<T>> retrieveData, TimeSpan expiration)
        {
            var cachedResponse = await _cache.StringGetAsync(cacheKey);
            if (cachedResponse.HasValue)
            {
                return JsonConvert.DeserializeObject<T>(cachedResponse);
                //return (T)Convert.ChangeType(cachedResponse, typeof(T));
            }
            else
            {
                T response = await retrieveData();
                await _cache.StringSetAsync(cacheKey, JsonConvert.SerializeObject(response), expiration);
                return response;
            }
        }
    }

}
