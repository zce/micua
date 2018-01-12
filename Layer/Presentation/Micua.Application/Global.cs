// --------------------------------------------------------------------------------------------------------------------
// Project          : Micua CMS
// Assembly         : Micua.Core
// Author           : iceStone
// Created          : 2013-12-29 6:13 PM
// 
// Last Modified By : iceStone
// Last Modified On : 2013-12-29 6:13 PM
// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Global.asax.cs" company="Wedn.Net">
//     Copyright © 2014 Wedn.Net. All Rights Reserved.
// </copyright>
// <summary>
//     网站应用程序全局类型
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Micua
{
    using System;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;
    using Micua.Application.Configs;
    using Micua.Core.Bundle;
    using Micua.Core.IoC;
    using Micua.Core.Plugin;
    using Micua.Core.Routing;
    using Micua.Core.Tracing;
    using Micua.Core.ViewEngine;
    using Micua.Infrastructure.Utility;

    /// <summary>
    /// 网站应用程序全局类型
    /// </summary>
    /// <remarks>
    /// 2013-11-23 22:33 Created By iceStone
    /// </remarks>
    public class MvcApplication : System.Web.HttpApplication
    {
        private static bool DbConnected;
        static MvcApplication()
        {
            DbConnected = SqlHelper.TestConnection(Config.GetString("db_host"), Config.GetInt32("db_port"), Config.GetInt32("db_connect_timeout"));
        }

        #region 网站程序启动事件
        /// <summary>
        /// 网站程序启动事件
        /// </summary>
        /// <remarks>
        ///  2013-11-23 22:33 Created By iceStone
        /// </remarks>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Application_Start(object sender, EventArgs e)
        {
            //var connectionString = new ConnectionString
            //  {
            //      DataSource = Config.GetString("db_server"),
            //      InitialCatalog = Config.GetString("db_name"),
            //      IntegratedSecurity = Config.GetBoolean("db_integrated_auth"),
            //      UserID = Config.GetString("db_user"),
            //      Password = Config.GetString("db_password"),
            //      MinPoolSize = Config.GetInt32("db_min_pool_size"),
            //      MaxPoolSize = Config.GetInt32("db_max_pool_size"),
            //      ConnectTimeout = Config.GetInt32("db_connect_timeout")
            //  };
            //var config = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~/"); //System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);
            //var connStrs = config.ConnectionStrings;
            //connStrs.ConnectionStrings.Remove("MicuaContext");
            //connStrs.ConnectionStrings.Add(new System.Configuration.ConnectionStringSettings("MicuaContext", connectionString.ToString(), Config.GetString("db_provider_name")));
            //config.Save(System.Configuration.ConfigurationSaveMode.Modified);


            // 初始化IoC
            IoCHelper.InitializeWith(new DependencyResolverFactory());

            // 加载全局设置
            try
            {
                Setting.Set("Generator", "WEDN.NET");
                Setting.Set("site_name", "斯通摇滚吧！");
                // Setting.Set("site_theme", "twentytwelve");
                Setting.Set("site_theme", "default");
                Setting.Set("site_description", "又一个WEDN.NET站点");
                Setting.Set("site_icp", "京ICP备14041905号-1");
                Setting.Set("site_copyright", "Copyright © 2014 Wedn.Net. All Rights Reserved.");
                Setting.Set("widget_home_sidebar", string.Empty);

                // Setting.Set(IoCHelper.Resolve<IOptionService>().GetSetting());
            }
            catch (Exception exception)
            {
                LogHelper.WriteErrorLog("MicuaMvc", "Exception", exception);

                // throw;
            }

            // 注册自定义的控制器，使MVC控制器可以支持依赖注入
            ControllerBuilder.Current.SetControllerFactory(new ResolverControllerFactory().GetControllerFactory());

            // 注册所有插件
            PluginBase.RegisterAllPlugins();

            // 注册区域信息
            //AreaRegistration.RegisterAllAreas();

            // 注册WebAPI
            // WebApiConfig.Register(GlobalConfiguration.Configuration);

            // 注册路由
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            // 注册控制器配置
            // ControllerBuilder.Current.RegisterDefaultNamespace();

            // 注册过滤器
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            // 注册捆绑资源
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // 注册模版引擎
            ViewEngineConfig.RegisterViewEngines(ViewEngines.Engines);
        }
        #endregion

        #region 会话开始事件
        /// <summary>
        /// 会话开始事件
        /// </summary>
        /// <remarks>
        ///  2013-11-23 22:33 Created By iceStone
        /// </remarks>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Session_Start(object sender, EventArgs e)
        {
        }
        #endregion

        #region 请求开始事件
        /// <summary>
        /// 请求开始事件
        /// </summary>
        /// <remarks>
        ///  2013-11-23 22:33 Created By iceStone
        /// </remarks>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            //Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
            // WednApplication.Init();
            //if (!DbConnected)
            //{
            //    DbConnected = SqlHelper.TestConnection(Config.GetString("db_host"), Config.GetInt32("db_port"), Config.GetInt32("db_connect_timeout"));
            //    if (!DbConnected)
            //    {
            //        Response.ContentType = "text/html";
            //        Response.Write("<h2>数据库连接异常，请联系管理员！</h2>");
            //        Response.End();
            //    }
            //}
        }
        #endregion

        #region 验证请求
        /// <summary>
        /// 验证请求
        /// </summary>
        /// <remarks>
        ///  2013-11-23 22:33 Created By iceStone
        /// </remarks>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
        }
        #endregion

        #region 程序错误事件
        /// <summary>
        /// 程序错误事件
        /// </summary>
        /// <remarks>
        ///  2013-11-23 22:33 Created By iceStone
        /// </remarks>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Application_Error(object sender, EventArgs e)
        {
#if !DEBUG
            if (this.Context.IsDebuggingEnabled || this.Context.Request.IsLocal)
            {
                return;
            }
            var exception = Server.GetLastError() as HttpException;
            if (exception != null)
            {
                var httpContext = new HttpContextWrapper(this.Context);
                RouteData routeData = RouteTable.Routes.GetRouteData(httpContext);
                var controllerName = (string)routeData.Values["controller"];
                var actionName = (string)routeData.Values["action"];
                var model = new HandleErrorInfo(exception, controllerName, actionName);
                var controller = new SharedController();
                controller.ViewData.Model = model;
                routeData.Values["controller"] = "Shared";
                routeData.Values["action"] = "Error";
                routeData.Values["status"] = exception.GetHttpCode();
                httpContext.ClearError(); //httpContext.Response.Clear();
                httpContext.Response.ContentType = "text/html; charset=utf-8";
                httpContext.Response.StatusCode = exception.GetHttpCode();
                ((IController)controller).Execute(new RequestContext(httpContext, routeData));
            }
#endif
            //var result = new ViewResult
            //{
            //    ViewName = Config.GetString("default_error_view", "Error"),
            //    MasterName = Config.GetString("default_error_layout", "_Layout"),
            //    ViewData = new ViewDataDictionary<HandleErrorInfo>(model),
            //    //ViewEngineCollection = new ViewEngineCollection()
            //};
            //LogHelper.WriteErrorLog(ex.Message,ex); //记录日志信息  
            //var httpStatusCode = (ex is HttpException) ? (ex as HttpException).GetHttpCode() : 500; //这里仅仅区分两种错误  
            //var httpContext = ((MvcApplication)sender).Context;
            //httpContext.ClearError();
            //httpContext.Response.Clear();
            //httpContext.Response.StatusCode = httpStatusCode;
            //var shouldHandleException = true;
            //HandleErrorInfo errorModel;

            //var routeData = new RouteData();
            //routeData.Values["controller"] = "Shared";

            //switch (httpStatusCode)
            //{
            //    case 404:
            //        routeData.Values["action"] = "Error";
            //        errorModel = new HandleErrorInfo(new Exception(string.Format("No page Found", httpContext.Request.UrlReferrer), ex), "Utility", "PageNotFound");
            //        break;

            //    default:
            //        routeData.Values["action"] = "Error";
            //        Exception exceptionToReplace = null; //这里使用了EntLib的异常处理模块的一些功能  
            //        shouldHandleException = ExceptionPolicy.HandleException(ex, "LogAndReplace", out exceptionToReplace);
            //        errorModel = new HandleErrorInfo(exceptionToReplace, "Utility", "Error");
            //        break;
            //}

            //if (shouldHandleException)
            //{
            //    var controller = new UtilityController();
            //    controller.ViewData.Model = errorModel; //通过代码路由到指定的路径  
            //    ((IController)controller).Execute(new RequestContext(new HttpContextWrapper(httpContext), routeData));
            //}  
        }
        #endregion

        #region 会话结束事件
        /// <summary>
        /// 会话结束事件
        /// </summary>
        /// <remarks>
        ///  2013-11-23 22:33 Created By iceStone
        /// </remarks>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Session_End(object sender, EventArgs e)
        {
        }
        #endregion

        #region 程序结束事件
        /// <summary>
        /// 程序结束事件
        /// </summary>
        /// <remarks>
        ///  2013-11-23 22:33 Created By iceStone
        /// </remarks>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Application_End(object sender, EventArgs e)
        {
        }
        #endregion
    }
}