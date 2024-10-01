using StackExchange.Redis;
using System.Text.Json;

namespace Store.Service.CasheServices
{
    internal class CasheService : ICasheService
    {
        private readonly IDatabase _database;
        public CasheService(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task<string> GetCasheResponseAsync(string key)
        {
            var cashedResponse = await _database.StringGetAsync(key);
            if (cashedResponse.IsNullOrEmpty)
                return null;

            return cashedResponse.ToString();
        }

        public async Task SetCasheResponseAsync(string Key, object response, TimeSpan TimeToLive)
        {
            if (response == null)
                return;

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var SerilaizedResponse = JsonSerializer.Serialize(response, options);
            await _database.StringSetAsync(Key, SerilaizedResponse, TimeToLive);
        }
    }
}
