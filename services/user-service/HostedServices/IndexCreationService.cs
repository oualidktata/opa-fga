using Microsoft.Extensions.Hosting;
using Redis.OM;
using user_service_core.Entities;

namespace user_service.HostedServices
{
    public class IndexCreationService : IHostedService
    {

        private readonly RedisConnectionProvider _provider;
        public IndexCreationService(RedisConnectionProvider provider)
        {
            _provider = provider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var info = (await _provider.Connection.ExecuteAsync(command: "FT._LIST")).ToArray().Select(x => x.ToString());
            if (info.All(x => x != "userentity-idx"))
            {
                await _provider.Connection.CreateIndexAsync(typeof(UserEntity));

            }

        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
