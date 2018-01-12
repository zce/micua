// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultCacheProvider.cs" company="Wedn.Net">
//   Copyright © 2014 Wedn.Net. All Rights Reserved.
// </copyright>
// <summary>
//   基于HttpRuntime的缓存供给者
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Micua.Core.Caching
{
    using System;
    using System.Text.RegularExpressions;
    using System.Web;

    using Micua.Infrastructure.Utility;

    /// <summary>
    /// 基于HttpRuntime的缓存供给者
    /// </summary>
    public class DefaultCacheProvider : ICacheProvider
    {
        /// <summary>
        /// The cache key prefix.
        /// </summary>
        private static readonly string CacheKeyPrefix = Config.GetString("cache_key_prefix");

        #region 获取缓存数据 +object Get(string key)
        /// <summary>
        /// 获取缓存数据
        /// </summary>
        /// <remarks>
        ///  2013-11-23 22:03 Created By iceStone
        /// </remarks>
        /// <param name="key">键</param>
        /// <returns>System.Object.</returns>
        public object Get(string key)
        {
            key = CacheKeyPrefix + key;
            var cache = HttpRuntime.Cache;
            return cache[key];
        }
        #endregion

        #region 获取缓存数据 +T Get<T>(string key) where T : class
        /// <summary>
        /// 获取缓存数据
        /// </summary>
        /// <remarks>
        ///  2013-11-23 22:03 Created By iceStone
        /// </remarks>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="key">键</param>
        /// <returns>指定数据类型的对象</returns>
        public T Get<T>(string key) where T : class
        {
            var obj = Get(key);
            return obj as T;
        }
        #endregion

        #region 设置数据缓存 +void Set(string key, object value, DateTime absoluteExpiration)
        /// <summary>
        /// 设置数据缓存
        /// </summary>
        /// <remarks>
        ///  2013-11-23 22:03 Created By iceStone
        /// </remarks>
        /// <param name="key">键</param>
        /// <param name="value">缓存对象</param>
        /// <param name="absoluteExpiration">绝对过期时间</param>
        public void Set(string key, object value, DateTime absoluteExpiration)
        {
            key = CacheKeyPrefix + key;
            var cache = HttpRuntime.Cache;
            cache.Insert(key, value, null, absoluteExpiration, System.Web.Caching.Cache.NoSlidingExpiration);
            //TimeSpan.Zero);
        }
        #endregion

        #region 设置数据缓存 +void Set(string key, object value, TimeSpan slidingExpiration)
        /// <summary>
        /// 设置数据缓存
        /// </summary>
        /// <remarks>
        ///  2013-11-23 22:03 Created By iceStone
        /// </remarks>
        /// <param name="key">键</param>
        /// <param name="value">缓存对象</param>
        /// <param name="slidingExpiration">最后一次访问所插入对象时与该对象到期时之间的时间间隔</param>
        public void Set(string key, object value, TimeSpan slidingExpiration)
        {
            key = CacheKeyPrefix + key;
            var cache = HttpRuntime.Cache;
            cache.Insert(key, value, null, DateTime.MaxValue, slidingExpiration);
        }
        #endregion

        #region 移除指定键的缓存数据 +void Remove(string key)
        /// <summary>
        /// 移除指定键的缓存数据
        /// </summary>
        /// <remarks>
        ///  2013-11-23 22:03 Created By iceStone
        /// </remarks>
        /// <param name="key">键</param>
        public void Remove(string key)
        {
            key = CacheKeyPrefix + key;
            var cache = HttpRuntime.Cache;
            cache.Remove(key);
        }
        #endregion

        #region 清除全部缓存数据 +void Clear()
        /// <summary>
        /// 清除全部缓存数据
        /// </summary>
        /// <remarks>
        ///  2013-11-23 22:03 Created By iceStone
        /// </remarks>
        public void Clear()
        {
            var cache = HttpRuntime.Cache;
            var cacheEnum = cache.GetEnumerator();
            while (cacheEnum.MoveNext())
            {
                cache.Remove(cacheEnum.Key.ToString());
            }
        }
        #endregion

        #region 清除通配键的缓存数据 +void Clear(string pattern)
        /// <summary>
        /// 清除通配键的缓存数据
        /// </summary>
        /// <remarks>
        ///  2013-11-23 22:03 Created By iceStone
        /// </remarks>
        /// <param name="pattern">键</param>
        public void Clear(string pattern)
        {
            var cache = HttpRuntime.Cache;
            var cacheEnum = cache.GetEnumerator();
            while (cacheEnum.MoveNext())
            {
                var key = cacheEnum.Key.ToString();
                if (Regex.IsMatch(key, CacheKeyPrefix + pattern))
                    cache.Remove(key);
            }
        }
        #endregion
    }
}
