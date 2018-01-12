namespace Micua.Application.Results
{
    using System;
    using System.Web;
    using System.Web.Mvc;
    using Micua.Infrastructure.Utility;

    public class MicuaHttpNotFoundResult : HttpNotFoundResult
    {
        public MicuaHttpNotFoundResult() : this(null) { }

        public MicuaHttpNotFoundResult(string statusDescription) : base(statusDescription) { }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            var controllerName = (string)context.RouteData.Values["controller"];
            var actionName = (string)context.RouteData.Values["action"];
            var model = new HandleErrorInfo(new HttpException(404, "Not Found"), controllerName, actionName);
            var result = new ViewResult
            {
                ViewName = Config.GetString("default_error_view", "Error"),
                MasterName = Config.GetString("default_error_layout", "_Layout"),
                ViewData = new ViewDataDictionary<HandleErrorInfo>(model)
            };
            context.HttpContext.Response.Clear();
            result.ExecuteResult(context);
        }
    }
}