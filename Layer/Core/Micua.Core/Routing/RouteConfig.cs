// ***********************************************************************
// Project			: Micua CMS
// Assembly         : Micua.Core
// Author           : iceStone
// Created          : 2013-11-23 22:46
//
// Last Modified By : iceStone
// Last Modified On : 2013-11-23 22:47
// ***********************************************************************
// <copyright file="RouteConfig.cs" company="Wedn.Net">
//     Copyright © 2014 Wedn.Net. All Rights Reserved.
// </copyright>
// <summary>缺少注释</summary>
// ***********************************************************************

namespace Micua.Core.Routing
{
    using System.Configuration;
    using System.Web.Mvc;
    using System.Web.Routing;

    using Micua.Core.Routing.Configuration;

    /// <summary>
    /// Class RouteConfig.
    /// </summary>
    /// <remarks>
    ///  2013-11-23 22:47 Created By iceStone
    /// </remarks>
    public class RouteConfig
    {
        /// <summary>
        /// Registers the routes.
        /// </summary>
        /// <remarks>
        ///  2013-11-23 22:47 Created By iceStone
        /// </remarks>
        public static void RegisterRoutes(RouteCollection routes)
        {
            var section = ConfigurationManager.GetSection("routing") as RouteConfigurationSection;
            if (section == null)
                throw new ConfigurationErrorsException("The <routing> configuration section does not exist.");
            // 清除
            //routes.Clear();
            // 处理忽略项
            foreach (Ignore item in section.Ignores)
            {
                routes.IgnoreRoute(item.Url, item.Constraints.ToRouteValueDictionary());
            }

            #region 处理区域映射
            if (section.Areas.Enable)
            {
                foreach (Area item in section.Areas)
                {
                    var context = new AreaRegistrationContext(item.Name, routes);
                    foreach (Map map in item)
                    {
                        var defaults = new RouteValueDictionary();
                        var constraints = new RouteValueDictionary();

                        if (map.Controller != string.Empty)
                            defaults.Add("controller", map.Controller);

                        if (map.Action != string.Empty)
                            defaults.Add("action", map.Action);
                        foreach (Parameter param in map.Paramaters)
                        {
                            defaults.Add(param.Name, param.Value);
                            if (string.IsNullOrEmpty(param.Constraint)) continue;
                            constraints.Add(param.Name, param.Constraint);
                        }
                        if (constraints.Count == 0)
                        {
                            constraints = null;
                        }
                        var namespaces = map.Namespaces.ToArray();
                        var route = context.Routes.MapRoute(item.Name + "_" + map.Name, map.Domain, map.Url, defaults, constraints, namespaces);
                        route.DataTokens["area"] = context.AreaName;
                        route.DataTokens["UseNamespaceFallback"] = (namespaces == null || namespaces.Length == 0);
                    }
                }
            }
            #endregion

            #region 处理映射项
            if (section.Maps.Enable)
            {
                foreach (Map item in section.Maps)
                {
                    var defaults = new RouteValueDictionary();
                    var constraints = new RouteValueDictionary();

                    if (item.Controller != string.Empty)
                        defaults.Add("controller", item.Controller);

                    if (item.Action != string.Empty)
                        defaults.Add("action", item.Action);
                    foreach (Parameter param in item.Paramaters)
                    {
                        defaults.Add(param.Name, param.Value);
                        if (string.IsNullOrEmpty(param.Constraint)) continue;
                        constraints.Add(param.Name, param.Constraint);
                    }
                    if (constraints.Count == 0)
                        constraints = null;
                    routes.MapRoute(item.Name, item.Domain, item.Url, defaults, constraints, item.Namespaces.ToArray());
                }
            }
            #endregion

            // 设置校验文件存在为否
            routes.RouteExistingFiles = false;
            // 设置URL格式
            routes.LowercaseUrls = true;
            routes.AppendTrailingSlash = true;

            #region MyRegion

            ////var routes = RouteTable.Routes;
            //routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //routes.IgnoreRoute("content/{*pathInfo}");
            //routes.IgnoreRoute("assert/{*pathInfo}");

            ////routes.MapRoute(
            ////    name: "Index",
            ////    domain: "www.micua.me",
            ////    url: "{controller}/{action}/",
            ////    constraints: null,
            ////    defaults: new { controller = "Blog", action = "Index" },
            ////    namespaces: new[] { "Micua.Controller" }
            ////);
            //routes.MapRoute(
            //    name: "Page",
            //    domain: "www.micua.me",
            //    url: "{item}/",
            //    constraints: null,
            //    defaults: new { controller = "Page", action = "Detail" },
            //    namespaces: new[] { "Micua.Controller" }
            //);
            //routes.MapRoute(
            //    name: "Article",
            //    domain: "www.micua.me",
            //    url: "post/{item}/",
            //    constraints: null,
            //    defaults: new { controller = "Blog", action = "Detail" },
            //    namespaces: new[] { "Micua.Controller" }
            //);
            //routes.MapRoute(
            //    name: "Default",
            //    domain: "www.micua.me",
            //    url: "{controller}/{action}/",
            //    constraints: null,
            //    defaults: new { controller = "Blog", action = "Index" },
            //    namespaces: new[] { "Micua.Controller" }
            //);
            ////routes.MapRoute(
            ////    name: "Login",
            ////    domain: "{area}.micua.me",
            ////    url: "{controller}/{action}/",
            ////    constraints: null,
            ////    defaults: new { controller = "Dashboard", action = "Index" },
            ////    namespaces: new[] { "Micua.Admin.Controllers" }
            ////);
            ////routes.MapRoute(
            ////    name: "Admin",
            ////    domain: "{area}.micua.me",
            ////    url: "{controller}/{action}/",
            ////    constraints: null,
            ////    defaults: new { controller = "Login", action = "Index" },
            ////    namespaces: new[] { "Micua.Login.Controllers" }
            ////);
            ////routes.MapRoute(
            ////    name: "Default1",
            ////    url: "{controller}/{action}/",
            ////    defaults: new { controller = "Home", action = "Index" }
            ////);
            ////routes.MapRoute(
            ////    name: "Default2",
            ////    url: "{controller}/{action}.html",
            ////    defaults: new { controller = "Home", action = "Index" }
            ////); 

            #endregion

            #region MyRegion

            //routes.Clear();

            //// Turns off the unnecessary file exists check
            //routes.RouteExistingFiles = true;

            //// Ignore axd files such as assest, image, sitemap etc
            //routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //// Ignore the assets directory which contains images, js, css & html
            //routes.IgnoreRoute("content/{*pathInfo}");
            //routes.IgnoreRoute("assert/{*pathInfo}");

            //// Ignore the error directory which contains error pages
            ////routes.IgnoreRoute("styles/{*pathInfo}");

            ////Exclude favicon (google toolbar request gif file as fav icon which is weird)
            //routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.([iI][cC][oO]|[gG][iI][fF])(/.*)?" });

            //routes.MapDomain(
            //    "Home",
            //    "http://www.{*domain}",
            //    new { area = "" },
            //    innerRoutes =>
            //    {
            //        innerRoutes.MapRoute(
            //            "Home_Index",
            //            "",
            //            new { controller = "Home", action = "Index" },
            //            new[] { "Micua.Controller" });

            //        innerRoutes.MapRoute(
            //            "Home_Default",
            //            "{controller}/{action}/",
            //            new { controller = "Home", action = "Index" },
            //            new[] { "Micua.Controller" });
            //    });

            //routes.MapDomain(
            //    Setting.AdminAreaName,
            //    "http://{area}.{*domain}",
            //    new { area = Setting.AdminAreaName },
            //    innerRoutes =>
            //    {
            //        innerRoutes.MapRoute(
            //            Setting.AdminAreaName + "_Index",
            //            "",
            //            new { controller = "Dashboard", action = "Index" },
            //            new[] { "Micua.Admin.Controllers" });

            //        innerRoutes.MapRoute(
            //            Setting.AdminAreaName + "_Default",
            //            "{controller}/{action}/",
            //            new { controller = "Dashboard", action = "Index" },
            //            new[] { "Micua.Admin.Controllers" });
            //    }); 

            #endregion
        }
    }
}