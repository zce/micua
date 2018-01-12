// ***********************************************************************
// Project          : Micua.Infrastructure
// Assembly         : Micua.Utility
// Author           : iceStone
// Created          : 2014-01-08 10:08 PM
// 
// Last Modified By : iceStone
// Last Modified On : 2014-01-08 10:08 PM
// ***********************************************************************
// <copyright file="SessionHelper.cs" company="Wedn.Net">
//     Copyright © 2014 Wedn.Net. All Rights Reserved.
// </copyright>
// <summary>Session操作助手类</summary>
// ***********************************************************************

namespace Micua.Core.Session
{
    using Micua.Core.IoC;

    /// <summary>
    /// Session操作助手类
    /// </summary>
    /// <remarks>
    ///  2014-01-08 10:08 Created By iceStone
    /// </remarks>
    public static class SessionHelper
    {
        #region 缓存供给者对象 +static ISessionProvider SessionProvider
        private static ISessionProvider _sessionProvider;
        /// <summary>
        /// Session供给者对象
        /// </summary>
        public static ISessionProvider SessionProvider
        {
            get
            {
                if (_sessionProvider != null)
                    return _sessionProvider;
                lock ("CacheProvider")
                {
                    if (_sessionProvider != null)
                        return _sessionProvider;
                    return (_sessionProvider = IoCHelper.Resolve<ISessionProvider>());
                }
            }
        }
        #endregion

        #region 获取会话唯一标识符 +static string SessionId
        /// <summary>
        /// 获取会话唯一标识符
        /// </summary>
        public static string SessionId
        {
            get { return SessionProvider.SessionId; }
        } 
        #endregion

        #region 获取指定键的Session对象 +static object Get(string key)
        /// <summary>
        /// 获取指定键的Session对象
        /// </summary>
        /// <remarks>
        ///  2013-11-23 22:04 Created By iceStone
        /// </remarks>
        /// <param name="key">Session键</param>
        /// <returns>Session对象</returns>
        public static object Get(string key)
        {
            return SessionProvider.Get(key);
        }
        #endregion

        #region 获取指定类型的指定键的Session对象 +static TObj Get<TObj>(string key)
        /// <summary>
        /// 获取指定类型的指定键的Session对象
        /// </summary>
        /// <remarks>
        ///  2013-11-23 22:04 Created By iceStone
        /// </remarks>
        /// <typeparam name="TObj">Session数据类型</typeparam>
        /// <param name="key">Session键</param>
        /// <returns>Session对象</returns>
        public static TObj Get<TObj>(string key) where TObj : class
        {
            return SessionProvider.Get<TObj>(key);
        }
        #endregion

        #region 设置一个Session +static void Set(string key, object value)
        /// <summary>
        /// 设置一个Session
        /// </summary>
        /// <remarks>
        ///  2013-11-23 22:04 Created By iceStone
        /// </remarks>
        /// <param name="key">Session键</param>
        /// <param name="value">Session值</param>
        public static void Set(string key, object value)
        {
            SessionProvider.Set(key, value);
        }
        #endregion

        #region 删除一个指定的Session +static void Remove(string key)
        /// <summary>
        /// 删除一个指定的Session
        /// </summary>
        /// <remarks>
        ///  2013-11-23 22:04 Created By iceStone
        /// </remarks>
        /// <param name="key">Session键</param>
        public static void Remove(string key)
        {
            SessionProvider.Remove(key);
        }
        #endregion

        #region 清空Session或者删除匹配键的Session +static void Clear(string pattern = "")
        /// <summary>
        /// 清空Session或者删除匹配键的Session
        /// </summary>
        /// <remarks>
        ///  2013-11-23 22:04 Created By iceStone
        /// </remarks>
        /// <param name="pattern">匹配键</param>
        public static void Clear(string pattern = "")
        {
            SessionProvider.Clear(pattern);
        }
        #endregion

        #region 取消当前会话 +static void Abandon()
        /// <summary>
        /// 取消当前会话
        /// </summary>
        /// <remarks>
        ///  2013-11-23 22:04 Created By iceStone
        /// </remarks>
        public static void Abandon()
        {
            SessionProvider.Abandon();
        }
        #endregion
    }
}