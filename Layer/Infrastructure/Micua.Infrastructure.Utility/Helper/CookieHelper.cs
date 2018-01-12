// ***********************************************************************
// Project          : Micua.Infrastructure
// Assembly         : Micua.Infrastructure.Utility
// Author           : iceStone
// Created          : 2014-01-06 10:50 PM
// 
// Last Modified By : iceStone
// Last Modified On : 2014-01-06 10:50 PM
// ***********************************************************************
// <copyright file="CookieHelper.cs" company="Wedn.Net">
//     Copyright © 2014 Wedn.Net. All Rights Reserved.
// </copyright>
// <summary>客户端Cookie操作助手类</summary>
// ***********************************************************************

namespace Micua.Infrastructure.Utility
{
    using System;
    using System.Text.RegularExpressions;
    using System.Web;

    /// <summary>
    /// 客户端Cookie操作助手类
    /// </summary>
    /// <remarks>
    ///  2013-11-23 22:04 Created By iceStone
    /// </remarks>
    public static class CookieHelper
    {
        /// <summary>
        /// Key前缀,防止冲突
        /// </summary>
        private static readonly string CookieKeyPrefix = Config.GetString("cookie_key_prefix");

        #region 当前的HttpContext +static HttpContext Context
        /// <summary>
        /// 当前的HttpContext
        /// </summary>
        /// <remarks>
        ///  2013-11-23 22:04 Created By iceStone
        /// </remarks>
        private static HttpContext Context
        {
            get
            {
                var context = HttpContext.Current;
                if (context == null)
                    throw new NullReferenceException("Unable to get the current HttpContext!");
                return context;
            }
        }
        #endregion

        #region 取客户端Cookie值 +static string Get(string key)
        /// <summary>
        /// 取客户端Cookie值
        /// </summary>
        /// <remarks>
        ///  2013-11-23 22:04 Created By iceStone
        /// </remarks>
        /// <param name="key">该Cookie的键</param>
        /// <returns>该Cookie的值</returns>
        public static string Get(string key)
        {
            key = CookieKeyPrefix + key;
            var value = string.Empty;
            var cookie = Context.Request.Cookies[key];
            if (cookie != null)
                value = HttpUtility.UrlDecode(cookie.Value);
            return value;
        }
        #endregion

        #region 取客户端Cookie值 +static TObj Get<TObj>(string key)
        /// <summary>
        /// 取客户端Cookie值,反序列化成指定类型
        /// </summary>
        /// <remarks>
        ///  2013-11-23 22:04 Created By iceStone
        /// </remarks>
        /// <typeparam name="TObj">Cookie类型</typeparam>
        /// <param name="key">该Cookie的键</param>
        /// <returns>该Cookie的值</returns>
        public static TObj Get<TObj>(string key) where TObj : class
        {
            var value = Get(key);
            return JsonHelper.Deserialize<TObj>(value);
        }
        #endregion

        #region 设置客户端Cookie值 +static void Set(string key, string value, DateTime expires, string domain = "", string path = "/", bool secure = false)
        /// <summary>
        /// 设置客户端Cookie值
        /// </summary>
        /// <remarks>
        ///  2013-11-23 22:04 Created By iceStone
        /// </remarks>
        /// <param name="key">该Cookie的键</param>
        /// <param name="value">该Cookie的值</param>
        /// <param name="expires">该Cookie的过期日期</param>
        /// <param name="domain">该Cookie的作用域名</param>
        /// <param name="path">该Cookie的作用路径</param>
        /// <param name="secure">该Cookie是否只作用于HTTPS</param>
        public static void Set(string key, string value, DateTime? expires = null, string domain = "", string path = "/", bool secure = false)
        {
            key = CookieKeyPrefix + key;
            var cookie = new HttpCookie(key)
            {
                Value = HttpUtility.UrlEncode(value),       // Cookie值
                Domain = domain,                            // 作用域名
                Path = path,                                // 作用路径
                Secure = secure,                            // 是否只作用于HTTPS
            };
            if (expires != null)
                cookie.Expires = (DateTime)expires;

            var cookies = Context.Response.Cookies;
            cookies.Remove(key);
            cookies.Add(cookie);
        }
        #endregion

        #region 设置客户端Cookie值 +static void Set(string key, object value, DateTime expires, string domain = "", string path = "/", bool secure = false)
        /// <summary>
        /// 设置客户端Cookie值
        /// </summary>
        /// <remarks>
        ///  2013-11-23 22:04 Created By iceStone
        /// </remarks>
        /// <param name="key">该Cookie的键</param>
        /// <param name="value">该Cookie的值</param>
        /// <param name="expires">该Cookie的过期日期</param>
        /// <param name="domain">该Cookie的作用域名</param>
        /// <param name="path">该Cookie的作用路径</param>
        /// <param name="secure">该Cookie是否只作用于HTTPS</param>
        public static void Set(string key, object value, DateTime? expires = null, string domain = "", string path = "/", bool secure = false)
        {
            key = CookieKeyPrefix + key;
            var cookie = new HttpCookie(key)
            {
                Value = HttpUtility.UrlEncode(JsonHelper.Serialize(value)),      // Cookie值
                Domain = domain,                                                 // 作用域名
                Path = path,                                                     // 作用路径
                Secure = secure,                                                 // 是否只作用于HTTPS
            };
            if (expires != null)
                cookie.Expires = (DateTime)expires;

            var cookies = Context.Response.Cookies;
            cookies.Remove(key);
            cookies.Add(cookie);
        }
        #endregion

        #region 移除客户端Cookies中指定的值 +static void Remove(string key)
        /// <summary>
        /// 移除客户端Cookies中指定的值
        /// </summary>
        /// <remarks>
        ///  2013-11-23 22:04 Created By iceStone
        /// </remarks>
        /// <param name="key">该Cookie的键</param>
        public static void Remove(string key)
        {
            key = CookieKeyPrefix + key;
            // var cookies = Context.Response.Cookies;
            // cookies.Remove(key);
            var cookie = Context.Request.Cookies[key];
            if (cookie == null)
            {
                return;
            }

            cookie.Expires = DateTime.Now.AddDays(-7);
            Context.Response.Cookies.Add(cookie);
        }
        #endregion

        #region 清除 Cookie 集合中的所有 Cookie +static void Clear(string pattern = "")
        /// <summary>
        /// 清除 Cookie 集合中的所有 Cookie,或指定正则匹配键的 Cookie
        /// </summary>
        /// <remarks>
        ///  2014-01-23 22:04 Created By iceStone
        /// </remarks>
        /// <param name="pattern">正则匹配键</param>
        public static void Clear(string pattern = "")
        {
            var cookies = Context.Request.Cookies;

            // if (string.IsNullOrEmpty(pattern))
            // {
            // foreach (string key in cookies)
            // {
            // var cookie = Context.Request.Cookies[key];
            // if(cookie==null)continue;
            // cookie.Expires = DateTime.Now.AddDays(-7);
            // Context.Response.Cookies.Add(cookie);
            // }
            // }
            // else
            // {
            // var regex = new Regex(CookieKeyPrefix + pattern);
            // foreach (var cookie in cookies.Cast<string>().Where(key => regex.IsMatch(key)).Select(key => Context.Request.Cookies[key]).Where(cookie => cookie != null))
            // {
            // cookie.Expires = DateTime.Now.AddDays(-7);
            // Context.Response.Cookies.Add(cookie);
            // }
            // }
            var regex = new Regex(CookieKeyPrefix + pattern);
            var count = cookies.Keys.Count;
            for (int i = 0; i < count; i++)
            {
                if (!regex.IsMatch(cookies.Keys[i]))
                {
                    continue;
                }

                var cookie = Context.Request.Cookies[cookies.Keys[i]];
                if (cookie == null)
                {
                    continue;
                }

                cookie.Expires = DateTime.Now.AddDays(-7);
                Context.Response.Cookies.Add(cookie);
            }
        }
        #endregion
    }
}
