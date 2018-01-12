using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Micua.Infrastructure.Utility;

namespace Micua.Core.Plugin
{
    public class PluginViewEngine : BuildManagerViewEngine
    {
        /// <summary>
        /// 模版引擎构造
        /// </summary>
        public PluginViewEngine() : this(null) { }

        /// <summary>
        /// 模版引擎构造
        /// </summary>
        public PluginViewEngine(IViewPageActivator viewPageActivator)
            : base(viewPageActivator)
        {
            #region 移到配置文件

            string themeRoot = "~" + Config.GetString("theme_root", "/Themes/")
                               + Setting.GetString("site_theme", "default");

            string areaRoot = themeRoot + "/Plugins";

            #region 完整版

            AreaViewLocationFormats = new[] { 
                 areaRoot + "/{2}/Views/{1}/{0}.cshtml",
                 areaRoot + "/{2}/Views/Shared/{0}.cshtml",
                 "~/Plugins/{2}/Views/{1}/{0}.cshtml",
                 "~/Plugins/{2}/Views/Shared/{0}.cshtml",
             };
            AreaMasterLocationFormats = new[] {
                 areaRoot + "/{2}/Views/{1}/{0}.cshtml",
                 areaRoot + "/{2}/Views/Shared/{0}.cshtml",
                 "~/Plugins/{2}/Views/{1}/{0}.cshtml",
                 "~/Plugins/{2}/Views/Shared/{0}.cshtml",
             };
            AreaPartialViewLocationFormats = new[] { 
                 areaRoot + "/{2}/Views/{1}/{0}.cshtml",
                 areaRoot + "/{2}/Views/Shared/{0}.cshtml",
                 "~/Plugins/{2}/Views/{1}/{0}.cshtml",
                 "~/Plugins/{2}/Views/Shared/{0}.cshtml",
             };
            FileExtensions = new[] { "cshtml" }; 

            #endregion

            #endregion
        }

        /// <summary>
        /// 创建局部视图
        /// </summary>
        /// <param name="controllerContext"></param>
        /// <param name="partialPath"></param>
        /// <returns></returns>
        protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath)
        {
            return new RazorView(controllerContext, partialPath, null, false, FileExtensions, ViewPageActivator);
        }
        /// <summary>
        /// 创建视图
        /// </summary>
        /// <param name="controllerContext"></param>
        /// <param name="viewPath"></param>
        /// <param name="masterPath"></param>
        /// <returns></returns>
        protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
        {
            return new RazorView(controllerContext, viewPath, masterPath, true, FileExtensions, ViewPageActivator);
        }
    }
}
