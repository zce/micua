// ***********************************************************************
// Project			: Micua CMS
// Assembly         : Micua.Core
// Author           : iceStone
// Created          : 2013-11-23 22:40
//
// Last Modified By : iceStone
// Last Modified On : 2013-11-23 22:41
// ***********************************************************************
// <copyright file="FilterConfig.cs" company="Wedn.Net">
//     Copyright © 2014 Wedn.Net. All Rights Reserved.
// </copyright>
// <summary>缺少注释</summary>
// ***********************************************************************

namespace Micua.Application.Configs
{
    using System;
    using System.Web.Mvc;
    using Micua.Core.Routing.Filter;
    using Micua.Core.Tracing.Filter;
    using Micua.Infrastructure.Utility;

    /// <summary>
    /// 过滤器配置
    /// </summary>
    /// <remarks>
    ///  2013-11-23 22:41 Created By iceStone
    /// </remarks>
    public class FilterConfig
    {
        /// <summary>
        /// 注册全局过滤器
        /// </summary>
        /// <remarks>
        ///  2013-11-23 22:41 Created By iceStone
        /// </remarks>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //注册去除重复URL筛选器。
            //filters.Add(new RemoveDuplicateUrlAttribute());
            // 注册登录状态筛选器。
            // filters.Add(new LoginStatusFilter());
            // 注册异常跟踪筛选器。
            // filters.Add(new TracingFilter());
            // 注册错误处理筛选器
            filters.Add(new ErrorFilter
            {
                View = Config.GetString("default_error_view", "Error"),
                Master = Config.GetString("default_error_layout", "_Layout"),
                ExceptionType = typeof(Exception)
            });
        }
    }
}