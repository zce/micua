namespace Micua.Application.Controllers
{
    using System.Web.Mvc;
    using Micua.Application.Results;
    using Micua.Core.Session;
    using Micua.Domain.Model;

    public abstract class MicuaController : Controller
    {
        #region CurrentUser

        private User _currentUser;
        /// <summary>
        /// 当前用户对象实体
        /// </summary>
        /// <value>The current user.</value>
        protected User CurrentUser
        {
            get { return _currentUser ?? (_currentUser = SessionHelper.Get<User>(SessionKeys.CurrentUser)); }
        }

        #endregion

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //var culture = filterContext.RouteData.Values["culture"].IsNotNull()
            //                  ? filterContext.RouteData.Values["culture"].ToString()
            //                  : "zh-CHS";
            //if (filterContext.RouteData.Values["culture"].IsNotNull())
            //    LocalizationHelper.CurrentCulture = CultureInfo.GetCultureInfo(culture);
            base.OnActionExecuting(filterContext);
        }

        protected override ViewResult View(string viewName, string masterName, object model)
        {
            if (masterName != null && masterName.Length == 0)
            {
                masterName = "_Layout";
            }

            return base.View(viewName, masterName, model);
        }

        protected override HttpNotFoundResult HttpNotFound(string statusDescription)
        {
            //return base.HttpNotFound(statusDescription);
            // throw new System.Web.HttpException(404, "Not Found");
            return new MicuaHttpNotFoundResult(statusDescription);
        }

        protected override void HandleUnknownAction(string actionName)
        {
            // base.HandleUnknownAction(actionName);
            // throw new System.Web.HttpException(404, "Not Found");
            new MicuaHttpNotFoundResult().ExecuteResult(ControllerContext);
        }
    }
}