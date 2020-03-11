using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace Cms.App.Controllers
{
    public class CacheHelperController : Controller
    {
        private IMemoryCache _memoryCache;
        public CacheHelperController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public bool SetCache(string cacheKey, string values, int minutes = 5)
        {
            try
            {
                _memoryCache.Set(cacheKey, values, new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromMinutes(minutes)));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string GetCache(string cacheKey)
        {
            var obj = _memoryCache.Get(cacheKey);
            return JsonConvert.SerializeObject(obj);
        }

        public string GetOrCreate(string cacheKey, int minutes = 5)
        {
            try
            {
                var result = _memoryCache.GetOrCreate(cacheKey, (entry) =>
                {
                    entry.SlidingExpiration = TimeSpan.FromMinutes(minutes);
                    return DateTime.Now.ToString();
                });
                return JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}