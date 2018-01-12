namespace Micua.Application.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Microsoft.Practices.Unity;
    using Micua.Application.Models;
    using Micua.Domain.Service;
    /// <summary>
    /// WidgetController
    /// </summary>
    /// <remarks>
    /// 2014-08-19 11:40:10 Created by iceStone
    /// </remarks>
    public class WidgetController : MicuaController
    {
        #region Fields

        #endregion

        #region Properties
        [Dependency]
        public IOptionService OptionService { get; set; }
        #endregion

        #region Constructors

        #endregion

        #region Action

        #endregion

        #region ChildAction
        /// <summary>
        /// 挂件入口
        /// </summary>
        /// <param name="containerName">区域标识</param>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult Index(string containerName)
        {
            var model = OptionService.GetWidgets(containerName)
                .Select(w => new RenderWidget
                {
                    ActionName = w.a,
                    ControllerName = w.c,
                    RouteValues = RouteData.Values
                });

            return PartialView(model);
        }
        #endregion

        #region Utilities

        #endregion
    }
}