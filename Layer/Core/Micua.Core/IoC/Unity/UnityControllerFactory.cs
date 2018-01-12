using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Practices.Unity;
using Micua.Infrastructure.Utility;

namespace Micua.Core.IoC.Unity
{

    /// <summary>
    /// 自定义的控制器工厂，使MVC控制器可以支持依赖注入
    /// </summary>
    public class UnityControllerFactory : DefaultControllerFactory
    {
        private readonly IUnityContainer _container;
        /// <summary>
        /// 构造函数
        /// </summary>
        public UnityControllerFactory()
        {
            _container = UnityContext.Instance.Container;
        }
        protected override Type GetControllerType(RequestContext requestContext, string controllerName)
        {
            return base.GetControllerType(requestContext, controllerName);
        }
        public override IController CreateController(RequestContext requestContext, string controllerName)
        {
            return base.CreateController(requestContext, controllerName);
        }
        /// <summary>
        /// 检索指定请求上下文和控制器类型的控制器实例。
        /// </summary>
        /// <param name="requestContext">HTTP 请求的上下文，其中包括 HTTP 上下文和路由数据。</param>
        /// <param name="controllerType">控制器的类型。</param>
        /// <exception cref="System.Web.HttpException">controllerType 为 null。</exception>
        /// <exception cref="System.ArgumentException">无法分配 controllerType。</exception>
        /// <exception cref="System.InvalidOperationException">无法创建 controllerType 的实例。</exception>
        /// <returns>控制器实例。</returns>
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (requestContext == null)
            {
                //return null;
                //throw new ArgumentNullException("requestContext");
                throw new System.Web.HttpException(0x194, "Not Found");
            }
            if (controllerType == null)
            {
                //requestContext.HttpContext.Response.Write("你要找到找不到！");
                //return null;
                //return base.GetControllerInstance(requestContext, controllerType);
                throw new System.Web.HttpException(0x194, "Not Found");
                //var handleController = this.GetControllerType(requestContext, Config.GetString("default_shared_controller", "Shared"));
                //ControllerContext controllerContext = new ControllerContext(requestContext, _container.Resolve(handleController) as ControllerBase);
                //var controllerName = (string)requestContext.RouteData.Values["controller"];
                //var actionName = (string)requestContext.RouteData.Values["action"];
                //var model = new HandleErrorInfo(new HttpException(404, "Not Found"), controllerName, actionName);
                //var result = new ViewResult
                //{
                //    ViewName = Config.GetString("default_error_view", "Error"),
                //    MasterName = Config.GetString("default_error_layout", "_Layout"),
                //    ViewData = new ViewDataDictionary<HandleErrorInfo>(model)
                //};
                //////context.ParentActionViewContext = new ViewContext(context, result.View, result.ViewData, result.TempData, context.HttpContext.Response.Output);
                ////context.HttpContext.Response.Clear();
                ////result.ExecuteResult(context);
                //var viewResult = ViewEngines.Engines.FindPartialView(controllerContext, Config.GetString("default_error_view", "Error"));
                //var context = new ViewContext(controllerContext, viewResult.View, result.ViewData, result.TempData, requestContext.HttpContext.Response.Output);

                //viewResult.View.Render(context, controllerContext.HttpContext.Response.Output);
            }

            var controller = _container.Resolve(controllerType) as IController;

            return controller;
        }
    }
}