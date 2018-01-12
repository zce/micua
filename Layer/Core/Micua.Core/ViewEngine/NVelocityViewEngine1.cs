// +----------------------------------------------------------------------
// | Micua [ DO IT BATTER ! ]
// +----------------------------------------------------------------------
// | Copyright © 2014 Wedn.Net. All Rights Reserved.
// +----------------------------------------------------------------------
// | Summary : 基于NVelocity的模版引擎
// +----------------------------------------------------------------------
// | Author  : iceStone <ice@wedn.net>
// +----------------------------------------------------------------------

namespace Micua.Core.ViewEngine
{
    using System.Linq;
    using System.Web.Mvc;

    using Micua.Infrastructure.Utility;

    /// <summary>
    /// The n velocity view engine.
    /// </summary>
    public class NVelocityViewEngine1 : VirtualPathProviderViewEngine
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NVelocityViewEngine1"/> class. 
        /// 模版引擎构造
        /// </summary>
        public NVelocityViewEngine1()
        {
            #region 移到配置文件

            string themeRoot = "~" + Config.GetString("theme_root", "/Themes/") + Setting.GetString("site_theme", "default");

            AreaViewLocationFormats = new[] { 
                themeRoot + "/Areas/{2}/Views/{1}/{0}.htm",
                themeRoot + "/Areas/{2}/Views/Shared/{0}.htm",
                "~/Areas/{2}/Views/{1}/{0}.htm",
                "~/Areas/{2}/Views/Shared/{0}.htm",
            };
            AreaMasterLocationFormats = new[] {
                themeRoot + "/Areas/{2}/Views/{1}/{0}.htm",
                themeRoot + "/Areas/{2}/Views/Shared/{0}.htm",
                "~/Areas/{2}/Views/{1}/{0}.htm",
                "~/Areas/{2}/Views/Shared/{0}.htm",
            };
            AreaPartialViewLocationFormats = new[] { 
                themeRoot + "/Areas/{2}/Views/{1}/{0}.htm",
                themeRoot + "/Areas/{2}/Views/Shared/{0}.htm",
                "~/Areas/{2}/Views/{1}/{0}.htm",
                "~/Areas/{2}/Views/Shared/{0}.htm",
            };
            ViewLocationFormats = new[] {
                themeRoot + "/Views/{0}.htm", 
                themeRoot + "/Views/{1}/{0}.htm", 
                themeRoot + "/Views/Shared/{0}.htm", 
                "~/Views/{0}.htm", 
                "~/Views/{1}/{0}.htm", 
                "~/Views/Shared/{0}.htm", 
            };
            MasterLocationFormats = new[] {
                themeRoot + "/Views/{0}.htm", 
                themeRoot + "/Views/{1}/{0}.htm", 
                themeRoot + "/Views/Shared/{0}.htm",
                "~/Views/{0}.htm", 
                "~/Views/{1}/{0}.htm", 
                "~/Views/Shared/{0}.htm",
            };
            PartialViewLocationFormats = new[] {
                themeRoot + "/Views/{0}.htm", 
                themeRoot + "/Views/{1}/{0}.htm",
                themeRoot + "/Views/Shared/{0}.htm", 
                "~/Views/{0}.htm", 
                "~/Views/{1}/{0}.htm",
                "~/Views/Shared/{0}.htm", 
            };
            FileExtensions = new[] { "vm", "htm" };

            #endregion
        }


        /// <summary>
        /// The create partial view.
        /// </summary>
        /// <param name="controllerContext">
        /// The controller context.
        /// </param>
        /// <param name="partialPath">
        /// The partial path.
        /// </param>
        /// <returns>
        /// The <see cref="IView"/>.
        /// </returns>
        protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath)
        {
            int index = partialPath.LastIndexOf('/');
            var viewPath = partialPath.Substring(0, index + 1);
            var viewName = partialPath.Substring(index + 1);
            return new NVelocityView1(controllerContext,viewPath, viewName);
        }

        /// <summary>
        /// The create view.
        /// </summary>
        /// <param name="controllerContext">
        /// The controller context.
        /// </param>
        /// <param name="viewPath">
        /// The view path.
        /// </param>
        /// <param name="masterPath">
        /// The master path.
        /// </param>
        /// <returns>
        /// The <see cref="IView"/>.
        /// </returns>
        protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
        {
            masterPath = this.FindLayoutView(controllerContext);
            int index = viewPath.LastIndexOf('/');
            var viewName = viewPath.Substring(index + 1);
            viewPath = viewPath.Substring(0, index + 1);
            if (string.IsNullOrEmpty(masterPath))
            {
                return new NVelocityView1(controllerContext, viewPath, viewName);
            }

            index = masterPath.LastIndexOf('/');
            var layoutPath = masterPath.Substring(0, index + 1);
            var layoutName = masterPath.Substring(index + 1);
            return new NVelocityView1(controllerContext, viewPath, viewName, layoutPath, layoutName);
        }


        /// <summary>
        /// The find layout view.
        /// </summary>
        /// <param name="controllerContext">
        /// The controller context.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string FindLayoutView(ControllerContext controllerContext)
        {
            var controllerName = controllerContext.RouteData.GetRequiredString("controller");
            var areaName = controllerContext.RouteData.DataTokens["area"];
            var layoutName = Config.GetString("template_layout_name", "_Layout");
            foreach (var path in this.MasterLocationFormats.Select(item => string.Format(item, layoutName, controllerName)).Where(path => this.FileExists(controllerContext, path)))
            {
                return path;
            }

            foreach (var path in this.AreaMasterLocationFormats.Select(item => string.Format(item, layoutName, controllerName, areaName)).Where(path => this.FileExists(controllerContext, path)))
            {
                return path;
            }

            return string.Empty;
        }
    }
}
