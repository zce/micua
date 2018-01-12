// ***********************************************************************
// Project          : Micua.Infrastructure
// Assembly         : Micua.Core.Session
// Author           : Administrator
// Created          : 2014-01-11 11:41 AM
// 
// Last Modified By : Administrator
// Last Modified On : 2014-01-11 11:41 AM
// ***********************************************************************
// <copyright file="RuntimeSessionProvider.cs" company="Wedn.Net">
//     Copyright © 2014 Wedn.Net. All Rights Reserved.
// </copyright>
// <summary>基于HttpRuntime和Cookie的自定义Session供给者</summary>
// ***********************************************************************

namespace Micua.Core.Session
{
    using System;
    using System.Text.RegularExpressions;
    using System.Web;

    using Micua.Infrastructure.Utility;

    /// <summary>
    /// 基于HttpRuntime和Cookie的自定义Session供给者
    /// </summary>
    public class RuntimeSessionProvider : ISessionProvider
    {
        /// <summary>
        /// Key前缀,防止冲突
        /// </summary>
        private readonly static string SessionKeyPrefix = Config.GetString("session_key_prefix");
        /// <summary>
        /// Session模块是否启用
        /// </summary>
        private readonly static int Timeout;

        #region 构造函数 +DefaultSessionProvider()
        /// <summary>
        /// 构造函数
        /// </summary>
        static RuntimeSessionProvider()
        {
            Timeout = Setting.GetInt32("session_expires", 20);
        }
        #endregion

        #region 获取会话唯一标识符 +string SessionId
        /// <summary>
        /// 获取会话唯一标识符
        /// </summary>
        public string SessionId
        {
            get
            {
                var sessionId = CookieHelper.Get("session_id");
                if (string.IsNullOrEmpty(sessionId))
                {
                    sessionId = Guid.NewGuid().ToString("N");
                }
                CookieHelper.Set("session_id", sessionId);//, DateTime.Now.AddMinutes(Timeout));
                return sessionId;
            }
            set
            {
                CookieHelper.Set("session_id", value);//, DateTime.Now.AddMinutes(Timeout));
            }
        }
        #endregion

        #region 获取指定键的Session对象 +object Get(string key)
        /// <summary>
        /// 获取指定键的Session对象
        /// </summary>
        /// <remarks>
        ///  2013-11-23 22:04 Created By iceStone
        /// </remarks>
        /// <param name="key">Session键</param>
        /// <returns>Session对象</returns>
        public object Get(string key)
        {
            key = SessionKeyPrefix + key + "_" + SessionId;
            var cache = HttpRuntime.Cache[key];
            return cache;
        }
        #endregion

        #region 获取指定类型的指定键的Session对象 +TObj Get<TObj>(string key)
        /// <summary>
        /// 获取指定类型的指定键的Session对象
        /// </summary>
        /// <remarks>
        ///  2013-11-23 22:04 Created By iceStone
        /// </remarks>
        /// <typeparam name="TObj">Session数据类型</typeparam>
        /// <param name="key">Session键</param>
        /// <returns>Session对象</returns>
        public TObj Get<TObj>(string key) where TObj : class
        {
            var cache = Get(key);
            return cache as TObj;
        }
        #endregion

        #region 设置一个Session +void Set(string key, object value)
        /// <summary>
        /// 设置一个Session
        /// </summary>
        /// <remarks>
        ///  2013-11-23 22:04 Created By iceStone
        /// </remarks>
        /// <param name="key">Session键</param>
        /// <param name="value">Session值</param>
        public void Set(string key, object value)
        {
            key = SessionKeyPrefix + key + "_" + SessionId;
            var cache = HttpRuntime.Cache;
            cache.Insert(key, value, null, DateTime.MaxValue, TimeSpan.FromMinutes(Timeout));
        }
        #endregion

        #region 删除一个指定的Session +void Remove(string key)
        /// <summary>
        /// 删除一个指定的Session
        /// </summary>
        /// <remarks>
        ///  2013-11-23 22:04 Created By iceStone
        /// </remarks>
        /// <param name="key">Session键</param>
        public void Remove(string key)
        {
            key = SessionKeyPrefix + key + "_" + SessionId;
            var cache = HttpRuntime.Cache;
            cache.Remove(key);
        }
        #endregion

        #region 清空Session +void Clear()
        /// <summary>
        /// 清空Session
        /// </summary>
        /// <remarks>
        ///  2013-11-23 22:04 Created By iceStone
        /// </remarks>
        public void Clear()
        {
            var cache = HttpRuntime.Cache;
            var cacheEnum = cache.GetEnumerator();
            while (cacheEnum.MoveNext())
            {
                var key = cacheEnum.Key.ToString();
                if (Regex.IsMatch(key, string.Format("^{0}.+{1}&", SessionKeyPrefix, SessionId)))
                    cache.Remove(key);
            }
        }
        #endregion

        #region 删除匹配键的Session +void Clear(string pattern)
        /// <summary>
        /// 删除匹配键的Session
        /// </summary>
        /// <remarks>
        ///  2013-11-23 22:04 Created By iceStone
        /// </remarks>
        /// <param name="pattern">匹配键</param>
        public void Clear(string pattern)
        {
            var cache = HttpRuntime.Cache;
            var cacheEnum = cache.GetEnumerator();
            while (cacheEnum.MoveNext())
            {
                var key = cacheEnum.Key.ToString();
                if (Regex.IsMatch(key, string.Format("^{0}{1}{2}&", SessionKeyPrefix, pattern, SessionId)))
                    cache.Remove(key);
            }
        }
        #endregion

        #region 取消当前会话 +void Abandon()
        /// <summary>
        /// 取消当前会话
        /// </summary>
        /// <remarks>
        ///  2013-11-23 22:04 Created By iceStone
        /// </remarks>
        public void Abandon()
        {
            SessionId = string.Empty;
        }
        #endregion
    }
}