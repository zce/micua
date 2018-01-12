// ***********************************************************************
// Project			: 
// Assembly         : Micua.Core
// Author           : Administrator
// Created          : 2014年01月13日 11:23
//
// Last Modified By : Administrator
// Last Modified On : 2014年01月13日 11:31
// ***********************************************************************
// <copyright file="TracingFilter.cs" company="Wedn.Net">
//     Copyright © 2014 Wedn.Net. All Rights Reserved.
// </copyright>
// <summary>跟踪筛选器</summary>
// ***********************************************************************

namespace Micua.Core.Tracing.Filter
{
    using System.Web.Mvc;

    /// <summary>
    /// 跟踪筛选器。
    /// </summary>
    /// <remarks>
    ///  2014年01月13日 11:33 Created By Administrator
    /// </remarks>
    public class TracingFilter : ActionFilterAttribute
    {
        #region 在执行操作方法之前调用 +void OnActionExecuting(ActionExecutingContext filterContext)
        /// <summary>
        /// 在执行操作方法之前由 ASP.NET MVC 框架调用。
        /// </summary>
        /// <remarks>
        ///  2014年01月13日 11:33 Created By Administrator
        /// </remarks>
        /// <param name="filterContext">筛选器上下文。</param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
        } 
        #endregion

        #region 在执行操作方法后调用 +void OnActionExecuted(ActionExecutedContext filterContext)
        /// <summary>
        /// 在执行操作方法后由 ASP.NET MVC 框架调用。
        /// </summary>
        /// <remarks>
        ///  2014年01月13日 11:33 Created By Administrator
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
        ///  2014年01月13日 11:33 Created By Administrator
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
        ///  2014年01月13日 11:33 Created By Administrator
        /// </remarks>
        /// <param name="filterContext">筛选器上下文。</param>
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);
        } 
        #endregion
    }
}