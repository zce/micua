// ***********************************************************************
// Project			: Micua.Infrastructure
// Assembly         : Micua.Core.Routing
// Author           : iceStone
// Created          : 2014年01月09日 13:19
//
// Last Modified By : iceStone
// Last Modified On : 2014年01月09日 13:20
// ***********************************************************************
// <copyright file="RemoveDuplicateUrlAttribute.cs" company="Wedn.Net">
//     Copyright © 2014 Wedn.Net. All Rights Reserved.
// </copyright>
// <summary>去除重复URL筛选器</summary>
// ***********************************************************************

namespace Micua.Core.Routing.Filter
{
    using System.Web.Mvc;
    using System.Web.Routing;

    /// <summary>
    /// 去除重复URL筛选器。
    /// </summary>
    /// <remarks>
    ///  2014年01月09日 13:20 Created By iceStone
    /// </remarks>
    public class RemoveDuplicateUrlAttribute : ActionFilterAttribute
    {
        #region 在执行操作方法之前调用 +void OnActionExecuting(ActionExecutingContext filterContext)
        /// <summary>
        /// 在执行操作方法之前由 ASP.NET MVC 框架调用。
        /// </summary>
        /// <remarks>
        /// 2014年01月13日 13:20 Created By iceStone
        /// </remarks>
        /// <param name="filterContext">筛选器上下文。</param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var routes = RouteTable.Routes;
            var requestContext = filterContext.RequestContext;
            var routeData = requestContext.RouteData;
            var dataTokens = routeData.DataTokens;
            if (dataTokens["area"] == null)
                dataTokens.Add("area", string.Empty);
            var vpd = routes.GetVirtualPathForArea(requestContext, routeData.Values);
            if (vpd != null)
            {
                var virtualPath = vpd.VirtualPath.ToLower();
                var request = requestContext.HttpContext.Request;
                if (string.Equals(virtualPath, request.Path) || filterContext.IsChildAction)
                {
                    base.OnActionExecuting(filterContext);
                    return;
                }
                filterContext.Result = new RedirectResult(virtualPath + request.Url.Query, true);
                return;
            }
            base.OnActionExecuting(filterContext);
        }
        #endregion

        #region 在执行操作方法后调用 +void OnActionExecuted(ActionExecutedContext filterContext)
        /// <summary>
        /// 在执行操作方法后由 ASP.NET MVC 框架调用。
        /// </summary>
        /// <remarks>
        /// 2014年01月13日 13:20 Created By iceStone
        /// </remarks>
        /// <param name="filterContext">筛选器上下文。</param>
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }
        #endregion

        #region 在执行操作结果之前调用 +void OnResultExecuting(ResultExecutingContext filterContext)
        /// <summary>
        /// 在执行操作结果之前由 ASP.NET MVC 框架调用。
        /// </summary>
        /// <remarks>
        /// 2014年01月13日 13:20 Created By iceStone
        /// </remarks>
        /// <param name="filterContext">筛选器上下文。</param>
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            base.OnResultExecuting(filterContext);
        }
        #endregion

        #region 在执行操作结果后调用 +void OnResultExecuted(ResultExecutedContext filterContext)
        /// <summary>
        /// 在执行操作结果后由 ASP.NET MVC 框架调用。
        /// </summary>
        /// <remarks>
        /// 2014年01月13日 13:20 Created By iceStone
        /// </remarks>
        /// <param name="filterContext">筛选器上下文。</param>
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);
        }
        #endregion
    }
}