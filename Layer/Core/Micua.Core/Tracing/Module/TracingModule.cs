// ***********************************************************************
// Project          : Micua
// Assembly         : Micua.Core
// Author           : Administrator
// Created          : 2014-01-11 5:57 PM
// 
// Last Modified By : Administrator
// Last Modified On : 2014-01-11 5:57 PM
// ***********************************************************************
// <copyright file="TracingModule.cs" company="Wedn.Net">
//     Copyright © 2014 Wedn.Net. All Rights Reserved.
// </copyright>
// <summary>系统跟踪模块</summary>
// ***********************************************************************

namespace Micua.Core.Tracing.Module
{
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    /// <summary>
    /// 系统跟踪模块
    /// </summary>
    public class TracingModule : IHttpModule
    {
        #region 初始化模块 +void Init(HttpApplication context)
        /// <summary>
        /// 初始化模块，并使其为处理请求做好准备。
        /// </summary>
        /// <param name="context"> 一个 System.Web.HttpApplication，它提供对 ASP.NET 应用程序内所有应用程序对象的公用的方法、属性和事件的访问</param>
        public void Init(HttpApplication context)
        {
            context.Error += context_Error;
        }
        #endregion

        #region 异常事件 _void context_Error(object sender, System.EventArgs e)
        /// <summary>
        /// 当引发未经处理的异常时发生。
        /// </summary>
        /// <param name="sender">事件源。</param>
        /// <param name="e">不包含任何事件数据的 System.EventArgs。</param>
        void context_Error(object sender, System.EventArgs e)
        {
            //var application = sender as HttpApplication;
            //var exception = application.Server.GetLastError() as HttpException;
            //if (exception != null)
            //{
            //    var httpContext = new HttpContextWrapper(application.Context);
            //    RouteData routeData = RouteTable.Routes.GetRouteData(httpContext);
            //    var controllerName = (string)routeData.Values["controller"];
            //    var actionName = (string)routeData.Values["action"];
            //    var model = new HandleErrorInfo(exception, controllerName, actionName);
            //    var controller = new SharedController();
            //    controller.ViewData.Model = model;
            //    routeData.Values["controller"] = "Shared";
            //    routeData.Values["action"] = "Error";
            //    routeData.Values["status"] = exception.GetHttpCode();
            //    httpContext.ClearError(); //httpContext.Response.Clear();
            //    httpContext.Response.ContentType = "text/html; charset=utf-8";
            //    httpContext.Response.StatusCode = exception.GetHttpCode();
            //    ((IController)controller).Execute(new RequestContext(httpContext, routeData));
            //} 
            //if (context == null)
            //    return;
            //var exception = context.Server.GetLastError();
            //if (exception.GetType().Name == "HttpException")
            //{
            //    HttpException ex = exception as HttpException;
            //    if (ex != null && ex.GetHttpCode() == 404)
            //    {
            //        context.Response.StatusCode = 404;
            //    }
            //}
            //else
            //{
            //    LogHelper.WriteErrorLog("TracingModule", exception.Message, exception);
            //}

        }
        #endregion

        #region 资源释放 +void Dispose()
        /// <summary>
        /// 处置由实现 System.Web.IHttpModule 的模块使用的资源（内存除外）。
        /// </summary>
        public void Dispose()
        {
        }
        #endregion
    }
}