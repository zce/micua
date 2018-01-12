// ***********************************************************************
// Project			: Micua CMS
// Assembly         : Micua.Core.Routing
// Author           : iceStone
// Created          : 2013-12-07 23:07
//
// Last Modified By : iceStone
// Last Modified On : 2013-12-07 23:07
// ***********************************************************************
// <copyright file="AreaRegistrationContextExtensions.cs" company="Wedn.Net">
//     Copyright © 2014 Wedn.Net. All Rights Reserved.
// </copyright>
// <summary>缺少注释</summary>
// ***********************************************************************

namespace Micua.Core.Routing
{
    using System.Linq;
    using System.Web.Mvc;

    /// <summary>
    /// Class AreaRegistrationContextExtensions.
    /// </summary>
    /// <remarks>
    ///  2013-12-07 23:09 Created By iceStone
    /// </remarks>
    public static class AreaRegistrationContextExtensions
    {
        /// <summary>
        /// 使用指定的路由默认值、约束和命名空间，映射指定的 URL 路由并将其与 System.Web.Mvc.AreaRegistrationContext.AreaName属性指定的区域关联。
        /// </summary>
        /// <remarks>
        ///  2013-12-07 23:09 Created By iceStone
        /// </remarks>
        /// <param name="context">注册区域上下文.</param>
        /// <param name="name">路由的名称.</param>
        /// <param name="domain">域名</param>
        /// <param name="url">路由的 URL 模式.</param>
        /// <param name="defaults">一个包含默认路由值的对象.</param>
        /// <returns>对映射路由的引用.</returns>
        public static System.Web.Routing.Route MapRoute(this AreaRegistrationContext context, string name, string domain, string url, object defaults)
        {
            return MapRoute(context, name, domain, url, defaults, null, null);
        }

        /// <summary>
        /// 使用指定的路由默认值、约束和命名空间，映射指定的 URL 路由并将其与 System.Web.Mvc.AreaRegistrationContext.AreaName属性指定的区域关联。
        /// </summary>
        /// <remarks>
        ///  2013-12-07 23:09 Created By iceStone
        /// </remarks>
        /// <param name="context">注册区域上下文.</param>
        /// <param name="name">路由的名称.</param>
        /// <param name="domain">路由的域名约束</param>
        /// <param name="url">路由的 URL 模式.</param>
        /// <param name="defaults">一个包含默认路由值的对象.</param>
        /// <param name="namespaces">应用程序的一组可枚举的命名空间.</param>
        /// <returns>对映射路由的引用.</returns>
        public static System.Web.Routing.Route MapRoute(this AreaRegistrationContext context, string name, string domain, string url, object defaults, string[] namespaces)
        {
            return MapRoute(context, name, domain, url, defaults, null, namespaces);
        }

        /// <summary>
        /// 使用指定的路由默认值、约束和命名空间，映射指定的 URL 路由并将其与 System.Web.Mvc.AreaRegistrationContext.AreaName属性指定的区域关联。
        /// </summary>
        /// <remarks>
        ///  2013-12-07 23:09 Created By iceStone
        /// </remarks>
        /// <param name="context">注册区域上下文.</param>
        /// <param name="name">路由的名称.</param>
        /// <param name="domain">路由的域名约束</param>
        /// <param name="url">路由的 URL 模式.</param>
        /// <param name="defaults">一个包含默认路由值的对象.</param>
        /// <param name="constraints">一组用于指定 URL 参数的有效值的表达式.</param>
        /// <param name="namespaces">应用程序的一组可枚举的命名空间.</param>
        /// <returns>对映射路由的引用.</returns>
        public static System.Web.Routing.Route MapRoute(this AreaRegistrationContext context, string name, string domain, string url, object defaults, object constraints, string[] namespaces)
        {
            if (namespaces == null && context.Namespaces != null)
                namespaces = context.Namespaces.ToArray();

            var route = context.Routes.MapRoute(name, domain, url, defaults, constraints, namespaces);
            route.DataTokens["area"] = context.AreaName;

            bool useNamespaceFallback = (namespaces == null || namespaces.Length == 0);
            route.DataTokens["UseNamespaceFallback"] = useNamespaceFallback;

            return route;
        }
    }
}