namespace Micua.Core.Tracing.Filter
{
    using System;
    using System.Web;
    using System.Web.Mvc;

    using Micua.Core.ViewEngine;

    /// <summary>
    /// The error filter.
    /// </summary>
    public class ErrorFilter : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            var exception = filterContext.Exception;
            var statusCode = new HttpException(null, exception).GetHttpCode();
            //if (exception.GetType().Name == "HttpException")
            //{
            //    if (statusCode == 404)
            //    {
            //        filterContext.HttpContext.Response.StatusCode = 404;
            //        return;
            //    }
            //}
#if !DEBUG
            if (filterContext.IsChildAction
                || filterContext.ExceptionHandled
                || filterContext.HttpContext.IsDebuggingEnabled 
                || filterContext.HttpContext.Request.IsLocal) // && filterContext.HttpContext.IsCustomErrorEnabled)
            {
                return;
            }
            var controllerName = (string)filterContext.RouteData.Values["controller"];
            var actionName = (string)filterContext.RouteData.Values["action"];
            var model = new HandleErrorInfo(filterContext.Exception, controllerName, actionName);
            var result = new ViewResult
            {
                ViewName = this.View,
                MasterName = this.Master,
                ViewData = new ViewDataDictionary<HandleErrorInfo>(model),
                TempData = filterContext.Controller.TempData,
                //ViewEngineCollection = new ViewEngineCollection()
            };
            //foreach (var item in ViewEngineConfig.ViewEngines)
            //{
            //    result.ViewEngineCollection.Add(item);
            //}

            filterContext.Result = result;
            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Clear();
            filterContext.HttpContext.Response.StatusCode = statusCode;
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
            if ((statusCode == 500) && this.ExceptionType.IsInstanceOfType(exception))
            {
                LogHelper.WriteErrorLog("TracingModule", exception.Message, exception);
            }
#endif
            //base.OnException(filterContext);
        }
    }
}