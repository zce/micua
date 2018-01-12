// ***********************************************************************
// Project			: Micua CMS
// Assembly         : Micua.Core.Routing
// Author           : iceStone
// Created          : 2013-12-07 23:02
//
// Last Modified By : iceStone
// Last Modified On : 2013-12-07 23:06
// ***********************************************************************
// <copyright file="RouteCollectionExtensions.cs" company="Wedn.Net">
//     Copyright © 2014 Wedn.Net. All Rights Reserved.
// </copyright>
// <summary>缺少注释</summary>
// ***********************************************************************

namespace Micua.Core.Routing
{
    using System;
    using System.Configuration;
    using System.Web.Routing;

    using Micua.Core.IoC.Unity;
    using Micua.Core.Routing.Configuration;

    /// <summary>
    /// Class RouteCollectionExtensions.
    /// </summary>
    /// <remarks>
    ///  2013-12-07 23:06 Created By iceStone
    /// </remarks>
    public static class RouteCollectionExtensions
    {
        /// <summary>
        /// 忽略给定可用路由列表和约束列表的指定 URL 路由。
        /// </summary>
        /// <param name="routes">应用程序的路由的集合。</param>
        /// <param name="url">要忽略的路由的 URL 模式。</param>
        /// <param name="constraints">一组表达式，用于指定 url 参数的值。</param>
        /// <exception cref="ArgumentNullException">routes 或 url 参数为 null。</exception>
        public static void IgnoreRoute(this RouteCollection routes, string url, RouteValueDictionary constraints)
        {
            if (routes == null)
                throw new ArgumentNullException("routes");
            if (url == null)
                throw new ArgumentNullException("url");

            var item = new MicuaIgnoreRoute(url) { Constraints = constraints };

            routes.Add(item);
        }
        /// <summary>
        /// 映射指定的 URL 路由(转换小写)。
        /// </summary>
        /// <remarks>
        ///  2013-12-07 23:06 Created By iceStone
        /// </remarks>
        /// <param name="routes">应用程序的路由的集合.</param>
        /// <param name="name">要映射的路由的名称.</param>
        /// <param name="domain">域名部分.</param>
        /// <param name="url">路由的 URL 模式.</param>
        /// <param name="defaults">一个包含默认路由值的对象.</param>
        /// <returns> 对映射路由的引用.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// routes 或 url 参数为 null
        /// </exception>
        public static void MapRoute(this RouteCollection routes, string name, string domain, string url, object defaults)
        {
            if (routes == null) throw new ArgumentNullException("routes");

            routes.MapRoute(name, domain, url, defaults, null, null);
        }


        /// <summary>
        /// 映射指定的 URL 路由(转换小写)。
        /// </summary>
        /// <remarks>
        ///  2013-12-07 23:06 Created By iceStone
        /// </remarks>
        /// <param name="routes">应用程序的路由的集合.</param>
        /// <param name="name">要映射的路由的名称.</param>
        /// <param name="url">路由的 URL 模式.</param>
        /// <param name="domain">域名部分.</param>
        /// <param name="defaults">一个包含默认路由值的对象.</param>
        /// <param name="constraints">一组表达式，用于指定 url 参数的值.</param>
        /// <returns> 对映射路由的引用.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// routes 或 url 参数为 null
        /// </exception>
        public static void MapRoute(this RouteCollection routes, string name, string domain, string url, object defaults, object constraints)
        {
            if (routes == null) throw new ArgumentNullException("routes");

            routes.MapRoute(name, domain, url, defaults, constraints, null);
        }

        /// <summary>
        /// 映射指定的 URL 路由(转换小写)。
        /// </summary>
        /// <remarks>
        ///  2013-12-07 23:06 Created By iceStone
        /// </remarks>
        /// <param name="routes">应用程序的路由的集合.</param>
        /// <param name="name">要映射的路由的名称.</param>
        /// <param name="url">路由的 URL 模式.</param>
        /// <param name="domain">域名部分.</param>
        /// <param name="defaults">一个包含默认路由值的对象.</param>
        /// <param name="constraints">一组表达式，用于指定 url 参数的值.</param>
        /// <param name="namespaces">应用程序的一组命名空间.</param>
        /// <returns> 对映射路由的引用.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// routes 或 url 参数为 null
        /// </exception>
        public static Route MapRoute(this RouteCollection routes, string name, string domain, string url, object defaults, object constraints, string[] namespaces)
        {
            return MapRoute(routes, name, domain, url, new RouteValueDictionary(defaults),
                new RouteValueDictionary(constraints), namespaces);
        }
        /// <summary>
        /// 映射指定的 URL 路由(转换小写)。
        /// </summary>
        /// <remarks>
        ///  2013-12-07 23:06 Created By iceStone
        /// </remarks>
        /// <param name="routes">应用程序的路由的集合.</param>
        /// <param name="name">要映射的路由的名称.</param>
        /// <param name="url">路由的 URL 模式.</param>
        /// <param name="domain">域名部分.</param>
        /// <param name="defaults">一个包含默认路由值的对象.</param>
        /// <param name="constraints">一组表达式，用于指定 url 参数的值.</param>
        /// <param name="namespaces">应用程序的一组命名空间.</param>
        /// <returns> 对映射路由的引用.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// routes 或 url 参数为 null
        /// </exception>
        public static Route MapRoute(this RouteCollection routes, string name, string domain, string url, RouteValueDictionary defaults, RouteValueDictionary constraints, string[] namespaces)
        {
            if (routes == null) throw new ArgumentNullException("routes");
            if (url == null) throw new ArgumentNullException("url");
            
            var route = new MicuaRoute(domain, url, new MicuaRouteHandler())
            {
                Defaults = defaults,
                Constraints = constraints,
                DataTokens = new RouteValueDictionary()
            };

            if ((namespaces != null) && (namespaces.Length > 0))
                route.DataTokens["Namespaces"] = namespaces;
            //route.Constraints.Remove("Namespaces");
            if (string.IsNullOrEmpty(name))
                routes.Add(route);
            else
                routes.Add(name, route);

            return route;
            
        }
        /// <summary>
        /// 取结点
        /// </summary>
        /// <returns></returns>
        public static RouteConfigurationSection GetSection()
        {
            var section = ConfigurationManager.GetSection("route") as RouteConfigurationSection;
            return section;
        }
    }

}