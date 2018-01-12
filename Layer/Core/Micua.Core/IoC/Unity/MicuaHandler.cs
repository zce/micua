using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Micua.Core.IoC.Unity
{
    /// <summary>
    /// 程序入口处理程序
    /// </summary>
    public class MicuaHandler : MvcHandler
    {
        #region 初始化 MicuaHandler 类的新实例。 +MicuaHandler(RequestContext requestContext)
        /// <summary>
        /// 初始化 MicuaHandler 类的新实例。
        /// </summary>
        /// <param name="requestContext">请求上下文。</param>
        /// <exception cref="ArgumentNullException">requestContext 参数为 null。</exception>
        public MicuaHandler(RequestContext requestContext) : base(requestContext) { }
        #endregion

        #region 使用指定的 HTTP 上下文来添加版本标头。 override void AddVersionHeader(HttpContextBase httpContext)
        /// <summary>
        /// 使用指定的 HTTP 上下文来添加版本标头。
        /// </summary>
        /// <param name="httpContext">HTTP 上下文。</param>
        protected override void AddVersionHeader(HttpContextBase httpContext)
        {
            if (!DisableMvcResponseHeader)
            {
                httpContext.Response.AppendHeader("X-Micua-Version", MicuaInfo.Version.ToString());
                //httpContext.Response.AppendHeader("X-Pingback", Micua.Infrastructure.Utility.Config.SiteUrl + "/xmlrpc.php");
            }
            httpContext.Response.Headers.Remove("Server");
            httpContext.Response.Headers.Remove("X-AspNet-Version");
        }
        #endregion

        #region 使用指定的基 HTTP 请求上下文来处理请求。 override void ProcessRequest(HttpContext httpContext)
        /// <summary>
        /// 使用指定的基 HTTP 请求上下文来处理请求。
        /// </summary>
        /// <param name="httpContext">HTTP 上下文。</param>
        protected override void ProcessRequest(HttpContext httpContext)
        {
            HttpContextBase context = new HttpContextWrapper(httpContext);
            this.ProcessRequest(context);
        }
        #endregion

        #region 使用指定的基 HTTP 请求上下文来处理请求。 override void ProcessRequest(HttpContextBase httpContext)
        /// <summary>
        /// 使用指定的基 HTTP 请求上下文来处理请求。
        /// </summary>
        /// <param name="httpContext">HTTP 上下文。</param>
        protected override void ProcessRequest(HttpContextBase httpContext)
        {
            base.ProcessRequest(httpContext);
            var response = httpContext.Response;
            if (response.StatusCode != 200)
            {
                //友好展示
                var ex = httpContext.Server.GetLastError();
                //var result = new ViewResult
                //{
                //    ViewName = "Error"
                //};
                //result.ViewBag.Error = Parse("UnknownAction");
                ////HttpContext.ExceptionHandled = true;
                httpContext.Response.Clear();
                //httpContext.Response.StatusCode = 404;
                //httpContext.Response.TrySkipIisCustomErrors = true;
                //result.ExecuteResult(base.RequestContext);
                response.Write("404");
            }
        }
        #endregion
    }
}
