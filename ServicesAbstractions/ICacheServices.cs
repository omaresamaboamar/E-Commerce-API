using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstractions
{
    public interface ICacheServices
    {
        Task<string?> GetAsync(string Cachekey);
        Task SetAsync(string Cachekey, object value, TimeSpan timeSpan);
    }
}
