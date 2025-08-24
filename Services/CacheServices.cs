using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Contracts;
using ServicesAbstractions;

namespace Services
{
    public class CacheServices(ICacheRepository cacheRepository) : ICacheServices
    {
        public Task<string?> GetAsync(string Cachekey)
        {
            return cacheRepository.GetAsync(Cachekey);
        }

        public async Task SetAsync(string Cachekey, object CacheValue, TimeSpan timeSpan)
        {
            var value = JsonSerializer.Serialize(CacheValue);
            await cacheRepository.SetAsync(Cachekey, value, timeSpan);
        }
    }
}
