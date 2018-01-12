// ***********************************************************************
// Project			: Micua CMS
// Assembly         : Micua.Core.Routing
// Author           : iceStone
// Created          : 2013-12-07 21:58
//
// Last Modified By : iceStone
// Last Modified On : 2013-12-07 23:00
// ***********************************************************************
// <copyright file="MicuaRoute.cs" company="Wedn.Net">
//     Copyright © 2014 Wedn.Net. All Rights Reserved.
// </copyright>
// <summary>缺少注释</summary>
// ***********************************************************************

namespace Micua.Core.Routing
{
    using System;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Web.Routing;

    /// <summary>
    /// Micua 路由类.
    /// </summary>
    /// <remarks>
    ///  2013-12-07 23:01 Created By iceStone
    /// </remarks>
    public class MicuaRoute : Route
    {
        #region Fields

        //private Regex _domainRegex;
        //private Regex _pathRegex;

        #endregion

        #region Properties

        /// <summary>
        /// 域名
        /// </summary>
        public string Domain { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// 使用指定的 URL 模式和处理程序类初始化 <see cref="T:System.Web.Routing.Route" /> 类的新实例。
        /// </summary>
        /// <remarks>
        ///  2013-12-07 23:01 Created By iceStone
        /// </remarks>
        /// <param name="domain">域名部分。</param>
        /// <param name="url">路由的 URL 模式。</param>
        /// <param name="routeHandler">处理路由请求的对象。</param>
        public MicuaRoute(string domain, string url, IRouteHandler routeHandler)
            : base(url, routeHandler)
        {
            Domain = domain;
        }

        /// <summary>
        /// 使用指定的 URL 模式、默认参数值和处理程序类初始化 <see cref="T:System.Web.Routing.Route" /> 类的新实例。
        /// </summary>
        /// <remarks>
        ///  2013-12-07 23:01 Created By iceStone
        /// </remarks>
        /// <param name="domain">域名部分。</param>
        /// <param name="url">路由的 URL 模式。</param>
        /// <param name="defaults">用于 URL 中缺失的任何参数的值。</param>
        /// <param name="routeHandler">处理路由请求的对象。</param>
        public MicuaRoute(string domain, string url, RouteValueDictionary defaults, IRouteHandler routeHandler)
            : base(url, defaults, routeHandler)
        {
            Domain = domain;
        }

        /// <summary>
        /// 使用指定的 URL 模式、默认参数值、约束和处理程序类初始化 <see cref="T:System.Web.Routing.Route" /> 类的新实例。
        /// </summary>
        /// <remarks>
        ///  2013-12-07 23:01 Created By iceStone
        /// </remarks>
        /// <param name="domain">域名部分。</param>
        /// <param name="url">路由的 URL 模式。</param>
        /// <param name="defaults">要在 URL 不包含所有参数时使用的值。</param>
        /// <param name="constraints">一个用于指定 URL 参数的有效值的正则表达式。</param>
        /// <param name="routeHandler">处理路由请求的对象。</param>
        public MicuaRoute(string domain, string url, RouteValueDictionary defaults, RouteValueDictionary constraints,
            IRouteHandler routeHandler)
            : base(url, defaults, constraints, routeHandler)
        {
            Domain = domain;
        }

        /// <summary>
        /// 使用指定的 URL 模式、默认参数值、约束、自定义值和处理程序类初始化 <see cref="T:System.Web.Routing.Route" /> 类的新实例。
        /// </summary>
        /// <remarks>
        ///  2013-12-07 23:01 Created By iceStone
        /// </remarks>
        /// <param name="domain">域名部分。</param>
        /// <param name="url">路由的 URL 模式。</param>
        /// <param name="defaults">要在 URL 不包含所有参数时使用的值。</param>
        /// <param name="constraints">一个用于指定 URL 参数的有效值的正则表达式。</param>
        /// <param name="dataTokens">传递到路由处理程序但未用于确定该路由是否匹配特定 URL 模式的自定义值。这些值会传递到路由处理程序，以便用于处理请求。</param>
        /// <param name="routeHandler">处理路由请求的对象。</param>
        public MicuaRoute(string domain, string url, RouteValueDictionary defaults, RouteValueDictionary constraints,
            RouteValueDictionary dataTokens, IRouteHandler routeHandler)
            : base(url, defaults, constraints, dataTokens, routeHandler)
        {
            Domain = domain;
        }

        #endregion

        #region GetVirtualPath
        /// <summary>
        /// 返回与路由关联的 URL 的相关信息。
        /// </summary>
        /// <remarks>
        ///  2013-12-07 23:01 Created By iceStone
        /// </remarks>
        /// <param name="requestContext">一个对象，封装有关所请求的路由的信息。</param>
        /// <param name="values">一个包含路由参数的对象。</param>
        /// <returns>一个包含与路由关联的 URL 的相关信息的对象。</returns>
        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            //获取虚拟路径;
            var path = base.GetVirtualPath(requestContext, values);
            //如果路径为空则直接返回;
            if (path == null || path.VirtualPath == string.Empty) return path;
            //获取QueryString起始下标
            int qsIndex = path.VirtualPath.IndexOf("?", StringComparison.Ordinal);

            //第一步:转换小写(ToLowerInvariant)
            string newPath = qsIndex >= 0
                ? path.VirtualPath.Substring(0, qsIndex).ToLowerInvariant()
                : path.VirtualPath.ToLowerInvariant();

            //第二步:处理尾部斜杠(Trailing Slash)
            if (newPath.Length > 0 && newPath[newPath.Length - 1] != '/' && newPath.IndexOf('.') == -1)
            {
                //var route = (Route) (requestContext.RouteData.Route);
                //RouteTable.Routes.GetRouteData(requestContext.HttpContext).;
                //var routeUrl = route.Url;
                //if (routeUrl.Length > 0 && routeUrl[routeUrl.Length - 1] == '/')
                newPath += '/';
            }

            //第三步:处理查询参数(Preserve Query String)
            if (qsIndex >= 0)
                newPath += path.VirtualPath.Substring(qsIndex);

            path.VirtualPath = newPath;

            return path;
        }

        #endregion

        #region GetRouteData

        /// <summary>
        /// 返回有关所请求路由的信息。
        /// </summary>
        /// <remarks>
        ///  2013-12-08 03:02 Created By iceStone
        /// </remarks>
        /// <param name="httpContext">一个对象，封装有关 HTTP 请求的信息。</param>
        /// <returns>一个包含路由定义值的对象。</returns>
        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            #region old
            //// 构造 regex
            //_domainRegex = CreateRegex(Domain);
            //_pathRegex = CreateRegex(Url);

            //// 取请求报文主机头
            //var requestDomain = httpContext.Request.Headers["host"];
            //if (!string.IsNullOrEmpty(requestDomain))
            //{
            //    if (requestDomain.IndexOf(":", StringComparison.Ordinal) > 0)
            //    {
            //        requestDomain = requestDomain.Substring(0, requestDomain.IndexOf(":", StringComparison.Ordinal));
            //    }
            //}
            //else
            //{
            //    if (httpContext.Request.Url != null) requestDomain = httpContext.Request.Url.Host;
            //}
            //var requestPath = httpContext.Request.AppRelativeCurrentExecutionFilePath.Substring(2) + httpContext.Request.PathInfo;

            //// 匹配域名和路由
            //var domainMatch = _domainRegex.Match(requestDomain);
            //var pathMatch = _pathRegex.Match(requestPath);

            //// 路由数据
            //if (!domainMatch.Success || !pathMatch.Success) return null;
            //var data = new RouteData(this, RouteHandler);

            //// 添加默认选项
            //if (Defaults != null)
            //{
            //    foreach (var item in Defaults)
            //    {
            //        data.Values[item.Key] = item.Value;
            //        if (item.Key.Equals("area") || item.Key.Equals("Namespaces"))
            //        {
            //            data.DataTokens[item.Key] = item.Value;
            //        }
            //    }
            //}

            //// 匹配域名路由
            //for (var i = 1; i < domainMatch.Groups.Count; i++)
            //{
            //    var group = domainMatch.Groups[i];
            //    if (!group.Success) continue;
            //    var key = _domainRegex.GroupNameFromNumber(i);

            //    if (string.IsNullOrEmpty(key) || char.IsNumber(key, 0) || string.IsNullOrEmpty(group.Value))
            //        continue;
            //    if (key.Equals("area"))
            //    {
            //        data.DataTokens[key] = group.Value;
            //    }
            //    data.Values[key] = group.Value;
            //}

            //// 匹配域名路径
            //for (var i = 1; i < pathMatch.Groups.Count; i++)
            //{
            //    var group = pathMatch.Groups[i];
            //    if (!group.Success) continue;
            //    var key = _pathRegex.GroupNameFromNumber(i);

            //    if (string.IsNullOrEmpty(key) || char.IsNumber(key, 0) || string.IsNullOrEmpty(group.Value))
            //        continue;
            //    if (key.Equals("area"))
            //    {
            //        data.DataTokens[key] = group.Value;
            //    }
            //    data.Values[key] = group.Value;
            //}

            //return data; 
            #endregion

            // 匹配路径部分
            var data = base.GetRouteData(httpContext);

            if (string.IsNullOrEmpty(Domain))
            {
                return data;
            }

            if (data == null)
            {
                return null;
            }

            // 构造 regex
            var domainRegex = GenerateUrlRegex(Domain);


            // 请求信息
            string requestDomain = httpContext.Request.Headers["host"];
            if (!string.IsNullOrEmpty(requestDomain))
            {
                if (requestDomain.IndexOf(':') > 0)
                {
                    requestDomain = requestDomain.Substring(0, requestDomain.IndexOf(":"));
                }
            }
            else
            {
                requestDomain = httpContext.Request.Url.Host;
            }

            // 匹配域名和路由
            Match domainMatch = domainRegex.Match(requestDomain);

            if (domainMatch.Success)
            {
                // 匹配域名路由
                for (var i = 1; i < domainMatch.Groups.Count; i++)
                {
                    var group = domainMatch.Groups[i];
                    if (!group.Success) continue;
                    var key = domainRegex.GroupNameFromNumber(i);

                    if (string.IsNullOrEmpty(key) || char.IsNumber(key, 0) || string.IsNullOrEmpty(group.Value))
                        continue;
                    if (key.Equals("area"))
                    {
                        data.DataTokens[key] = group.Value;
                    }
                    data.Values[key] = group.Value;
                }
                return data;
            }
            else
            {
                // 域名没有匹配上
                return null;
            }


            //return base.GetRouteData(httpContext);
        }

        private Regex GenerateUrlRegex(string source)
        {
            // 替换
            source = source.Replace("/", @"\/?")
                .Replace(".", @"\.?")
                .Replace("-", @"\-?")
                .Replace("{", @"(?<")
                .Replace("}", @">([a-zA-Z0-9_]*))");

            return new Regex("^" + source + "$");
        }

        #endregion
    }
    sealed class MicuaIgnoreRoute : MicuaRoute
    {
        public MicuaIgnoreRoute(string url) : base(string.Empty, url, new StopRoutingHandler()) { }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary routeValues)
        {
            return null;
        }
    }
}