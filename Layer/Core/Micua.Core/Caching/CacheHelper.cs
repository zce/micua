// ***********************************************************************
// Project          : Micua.Infrastructure
// Assembly         : Micua.Core.Caching
// Author           : iceStone
// Created          : 2014-01-05 3:02 PM
// 
// Last Modified By : iceStone
// Last Modified On : 2014-01-05 3:02 PM
// ***********************************************************************
// <copyright file="CacheHelper.cs" company="Wedn.Net">
//     Copyright © 2014 Wedn.Net. All Rights Reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using Micua.Core.IoC;

namespace Micua.Core.Caching
{
    /// <summary>
    /// 缓存处理助手类
    /// </summary>
    /// <remarks>
    ///  2013-11-23 22:03 Created By iceStone
    /// </remarks>
    public static class CacheHelper
    {
        #region 缓存供给者对象 +static ICacheProvider CacheProvider
        private static ICacheProvider _cacheProvider;
        /// <summary>
        /// 缓存供给者对象
        /// </summary>
        public static ICacheProvider CacheProvider
        {
            get
            {
                if (_cacheProvider != null)
                    return _cacheProvider;
                lock ("CacheProvider")
                {
                    if (_cacheProvider != null)
                        return _cacheProvider;
                    return (_cacheProvider = IoCHelper.Resolve<ICacheProvider>());
                }
            }
        } 
        #endregion

        #region 获取缓存数据 +static object Get(string key)
        /// <summary>
        /// 获取缓存数据
        /// </summary>
        /// <remarks>
        ///  2013-11-23 22:03 Created By iceStone
        /// </remarks>
        /// <param name="key">键</param>
        /// <returns>System.Object.</returns>
        public static object Get(string key)
        {
            return CacheProvider.Get(key);
        }
        #endregion

        #region 获取缓存数据 +static T Get<T>(string key) where T : class
        /// <summary>
        /// 获取缓存数据
        /// </summary>
        /// <remarks>
        ///  2013-11-23 22:03 Created By iceStone
        /// </remarks>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="key">键</param>
        /// <returns>指定数据类型的对象</returns>
        public static T Get<T>(string key) where T : class
        {
            return CacheProvider.Get<T>(key);
        }
        #endregion

        #region 设置数据缓存 +static void Set(string key, object value, DateTime absoluteExpiration)
        /// <summary>
        /// 设置数据缓存
        /// </summary>
        /// <remarks>
        ///  2013-11-23 22:03 Created By iceStone
        /// </remarks>
        /// <param name="key">键</param>
        /// <param name="value">缓存对象</param>
        /// <param name="absoluteExpiration">绝对过期时间</param>
        public static void Set(string key, object value, DateTime absoluteExpiration)
        {
            CacheProvider.Set(key, value, absoluteExpiration);
        }
        #endregion

        #region 设置数据缓存 +static void Set(string key, object value, TimeSpan slidingExpiration)
        /// <summary>
        /// 设置数据缓存
        /// </summary>
        /// <remarks>
        ///  2013-11-23 22:03 Created By iceStone
        /// </remarks>
        /// <param name="key">键</param>
        /// <param name="value">缓存对象</param>
        /// <param name="slidingExpiration">最后一次访问所插入对象时与该对象到期时之间的时间间隔</param>
        public static void Set(string key, object value, TimeSpan slidingExpiration)
        {
            CacheProvider.Set(key, value, slidingExpiration);
        }
        #endregion

        #region 移除指定键的缓存数据 +static void Remove(string key)
        /// <summary>
        /// 移除指定键的缓存数据
        /// </summary>
        /// <remarks>
        ///  2013-11-23 22:03 Created By iceStone
        /// </remarks>
        /// <param name="key">键</param>
        public static void Remove(string key)
        {
            CacheProvider.Remove(key);
        }
        #endregion

        #region 清除全部缓存数据 +static void Clear()
        /// <summary>
        /// 清除全部缓存数据
        /// </summary>
        /// <remarks>
        ///  2013-11-23 22:03 Created By iceStone
        /// </remarks>
        public static void Clear()
        {
            CacheProvider.Clear();
        }
        #endregion

        #region static void Clear(string pattern)
        /// <summary>
        /// 清除通配键的缓存数据
        /// </summary>
        /// <remarks>
        ///  2013-11-23 22:03 Created By iceStone
        /// </remarks>
        /// <param name="pattern">键</param>
        public static void Clear(string pattern)
        {
            CacheProvider.Clear(pattern);
        }
        #endregion
    }
}