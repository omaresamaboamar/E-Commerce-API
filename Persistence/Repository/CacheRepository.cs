using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using StackExchange.Redis;

namespace Persistence.Repository
{
    public class CacheRepository(IConnectionMultiplexer connection) : ICacheRepository
    {
        readonly IDatabase _database = connection.GetDatabase();
        public async Task<string?> GetAsync(string Cachekey)
        {
            var cacheValue =await _database.StringGetAsync(Cachekey);
            return cacheValue.IsNullOrEmpty ? null : cacheValue.ToString();
        }

        public async Task SetAsync(string Cachekey, string value, TimeSpan timeSpan)
        {
            await _database.StringSetAsync(Cachekey, value, timeSpan);     
        }
    }
}
