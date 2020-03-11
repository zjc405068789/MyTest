using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.App.Models
{
    public class CacheHelper1
    {
        private IMemoryCache _memoryCache;
        public CacheHelper(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="cacheKey">缓存的key值</param>
        /// <param name="values">要缓存的值</param>
        /// <param name="minutes">要缓存的时间</param>
        /// <returns></returns>
        public bool SetCache(string cacheKey, string values, int minutes = 5)
        {
            try
            {
                _memoryCache.Set(cacheKey, values, new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(minutes)));
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="cacheKey">缓存的key</param>
        /// <returns></returns>
        public object GetCache(string cacheKey)
        {
            return _memoryCache.Get(cacheKey);
        }
    }
}
