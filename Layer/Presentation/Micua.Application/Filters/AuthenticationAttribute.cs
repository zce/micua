using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Micua.Core.IoC;
using Micua.Domain.Service;

namespace Micua.Application.Filters
{
    /// <summary>
    /// 登录状态筛选器。
    /// </summary>
    /// <remarks>
    /// 2014年01月09日 13:20 Created By iceStone
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    public class AuthenticationAttribute : FilterAttribute, IAuthorizationFilter //: ActionFilterAttribute
    {
        /// <summary>
        /// 在需要授权时调用。
        /// </summary>
        /// <param name="filterContext">筛选器上下文。</param>
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (!IoCHelper.Resolve<IUserService>().CheckLogin())
            {
                var urlHelper = new UrlHelper(filterContext.RequestContext);
                filterContext.Result = new RedirectResult(urlHelper.Action("Login", "Default",
                    new
                    {
                        area = "Account",
                        redirect = filterContext.RequestContext.HttpContext.Request.Url.ToString()
                    }));
            }
        }



        //#region 在执行操作方法之前调用 +void OnActionExecuting(ActionExecutingContext filterContext)
        ///// <summary>
        ///// 在执行操作方法之前由 ASP.NET MVC 框架调用。
        ///// </summary>
        ///// <remarks>
        ///// 2014年01月13日 13:20 Created By iceStone
        ///// </remarks>
        ///// <param name="filterContext">筛选器上下文。</param>
        //public override void OnActionExecuting(ActionExecutingContext filterContext)
        //{
        //    IoCHelper.Resolve<IUserService>().CheckLogin();
        //    base.OnActionExecuting(filterContext);
        //}
        //#endregion

        //#region 在执行操作方法后调用 +void OnActionExecuted(ActionExecutedContext filterContext)
        ///// <summary>
        ///// 在执行操作方法后由 ASP.NET MVC 框架调用。
        ///// </summary>
        ///// <remarks>
        ///// 2014年01月13日 13:20 Created By iceStone
        ///// </remarks>
        ///// <param name="filterContext">筛选器上下文。</param>
        //public override void OnActionExecuted(ActionExecutedContext filterContext)
        //{
        //    base.OnActionExecuted(filterContext);
        //}
        //#endregion

        //#region 在执行操作结果之前调用 +void OnResultExecuting(ResultExecutingContext filterContext)
        ///// <summary>
        ///// 在执行操作结果之前由 ASP.NET MVC 框架调用。
        ///// </summary>
        ///// <remarks>
        ///// 2014年01月13日 13:20 Created By iceStone
        ///// </remarks>
        ///// <param name="filterContext">筛选器上下文。</param>
        //public override void OnResultExecuting(ResultExecutingContext filterContext)
        //{
        //    base.OnResultExecuting(filterContext);
        //}
        //#endregion

        //#region 在执行操作结果后调用 +void OnResultExecuted(ResultExecutedContext filterContext)
        ///// <summary>
        ///// 在执行操作结果后由 ASP.NET MVC 框架调用。
        ///// </summary>
        ///// <remarks>
        ///// 2014年01月13日 13:20 Created By iceStone
        ///// </remarks>
        ///// <param name="filterContext">筛选器上下文。</param>
        //public override void OnResultExecuted(ResultExecutedContext filterContext)
        //{
        //    base.OnResultExecuted(filterContext);
        //}
        //#endregion
    }
}
