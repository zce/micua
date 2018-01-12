// ***********************************************************************
// Project			: Micua CMS
// Assembly         : Micua.Core
// Author           : iceStone
// Created          : 2013-11-23 22:40
//
// Last Modified By : iceStone
// Last Modified On : 2013-11-23 22:40
// ***********************************************************************
// <copyright file="WebApiConfig.cs" company="Wedn.Net">
//     Copyright © 2014 Wedn.Net. All Rights Reserved.
// </copyright>
// <summary>缺少注释</summary>
// ***********************************************************************

namespace Micua.Application.Configs
{
    using System.Web.Http;

    /// <summary>
    /// WebAPI配置
    /// </summary>
    /// <remarks>
    ///  2013-11-23 22:41 Created By iceStone
    /// </remarks>
    public static class WebApiConfig
    {
        /// <summary>
        /// WebAPI注册
        /// </summary>
        /// <param name="config"></param>
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
