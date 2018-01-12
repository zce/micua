// ***********************************************************************
// Project          : Micua
// Assembly         : Micua.Core
// Author           : Administrator
// Created          : 2013-12-29 5:48 PM
// 
// Last Modified By : Administrator
// Last Modified On : 2013-12-29 5:48 PM
// ***********************************************************************
// <copyright file="ViewEngine.cs" company="Wedn.Net">
//     Copyright © 2014 Wedn.Net. All Rights Reserved.
// </copyright>
// <summary>视图引擎</summary>
// ***********************************************************************

namespace Micua.Core.ViewEngine
{
    using System.Web.Mvc;

    using Micua.Infrastructure.Utility;

    /// <summary>
    /// 视图引擎
    /// </summary>
    public class MicuaViewEngine : BuildManagerViewEngine
    {
        /// <summary>
        /// 模版引擎构造
        /// </summary>
        public MicuaViewEngine() : this(null) { }

        /// <summary>
        /// 模版引擎构造
        /// </summary>
        public MicuaViewEngine(IViewPageActivator viewPageActivator)
            : base(viewPageActivator)
        {
            #region 移到配置文件

            string themeRoot = "~" + Config.GetString("theme_root", "/Themes/")
                               + Setting.GetString("site_theme", "default");

            #region 完整版

            AreaViewLocationFormats = new[] { 
                 themeRoot + "/Areas/{2}/Views/{1}/{0}.cshtml",
                 themeRoot + "/Areas/{2}/Views/Shared/{0}.cshtml",
                 "~/Areas/{2}/Views/{1}/{0}.cshtml",
                 "~/Areas/{2}/Views/Shared/{0}.cshtml",
             };
            AreaMasterLocationFormats = new[] {
                 themeRoot + "/Areas/{2}/Views/{1}/{0}.cshtml",
                 themeRoot + "/Areas/{2}/Views/Shared/{0}.cshtml",
                 "~/Areas/{2}/Views/{1}/{0}.cshtml",
                 "~/Areas/{2}/Views/Shared/{0}.cshtml",
             };
            AreaPartialViewLocationFormats = new[] { 
                 themeRoot + "/Areas/{2}/Views/{1}/{0}.cshtml",
                 themeRoot + "/Areas/{2}/Views/Shared/{0}.cshtml",
                 "~/Areas/{2}/Views/{1}/{0}.cshtml",
                 "~/Areas/{2}/Views/Shared/{0}.cshtml",
             };
            ViewLocationFormats = new[] {
                 themeRoot + "/Views/{0}.cshtml", 
                 themeRoot + "/Views/{1}/{0}.cshtml", 
                 themeRoot + "/Views/Shared/{0}.cshtml", 
                 "~/Views/{0}.cshtml", 
                 "~/Views/{1}/{0}.cshtml", 
                 "~/Views/Shared/{0}.cshtml", 
             };
            MasterLocationFormats = new[] {
                 themeRoot + "/Views/{0}.cshtml", 
                 themeRoot + "/Views/{1}/{0}.cshtml", 
                 themeRoot + "/Views/Shared/{0}.cshtml",
                 "~/Views/{0}.cshtml", 
                 "~/Views/{1}/{0}.cshtml", 
                 "~/Views/Shared/{0}.cshtml",
             };
            PartialViewLocationFormats = new[] {
                 themeRoot + "/Views/{0}.cshtml", 
                 themeRoot + "/Views/{1}/{0}.cshtml",
                 themeRoot + "/Views/Shared/{0}.cshtml", 
                 "~/Views/{0}.cshtml", 
                 "~/Views/{1}/{0}.cshtml",
                 "~/Views/Shared/{0}.cshtml", 
             };
            FileExtensions = new[] { "cshtml" }; 

            #endregion

            #region 精简版

            //this.AreaViewLocationFormats = new[]
            //{
            //    areaRoot + "/{2}/Views/{1}/{0}.cshtml",
            //    themeRoot + "/Areas/{2}/Views/{1}/{0}.cshtml",
            //    "~/Plugins/{2}/Views/{1}/{0}.cshtml", 
            //    "~/Areas/{2}/Views/{1}/{0}.cshtml",
            //};
            //this.AreaMasterLocationFormats = new[]
            //{
            //    areaRoot + "/{2}/Views/Shared/{0}.cshtml",
            //    themeRoot + "/Areas/{2}/Views/Shared/{0}.cshtml",
            //    "~/Plugins/{2}/Views/Shared/{0}.cshtml",
            //    "~/Areas/{2}/Views/Shared/{0}.cshtml",
            //};
            //this.AreaPartialViewLocationFormats = new[]
            //{
            //    areaRoot + "/{2}/Views/{1}/{0}.cshtml",
            //    areaRoot + "/{2}/Views/Shared/{0}.cshtml",
            //    themeRoot + "/Areas/{2}/Views/{1}/{0}.cshtml",
            //    themeRoot + "/Areas/{2}/Views/Shared/{0}.cshtml",
            //    "~/Plugins/{2}/Views/{1}/{0}.cshtml",
            //    "~/Plugins/{2}/Views/Shared/{0}.cshtml",
            //    "~/Areas/{2}/Views/{1}/{0}.cshtml",
            //    "~/Areas/{2}/Views/Shared/{0}.cshtml",
            //};
            //this.ViewLocationFormats = new[]
            //{
            //    themeRoot + "/Views/{0}.cshtml", themeRoot + "/Views/{1}/{0}.cshtml",
            //    "~/Views/{0}.cshtml", "~/Views/{1}/{0}.cshtml",
            //};
            //this.MasterLocationFormats = new[]
            //{
            //    themeRoot + "/Views/Shared/{0}.cshtml", 
            //    "~/Views/Shared/{0}.cshtml",
            //};
            //this.PartialViewLocationFormats = new[]
            //{
            //    themeRoot + "/Views/{0}.cshtml", themeRoot + "/Views/{1}/{0}.cshtml",
            //    themeRoot + "/Views/Shared/{0}.cshtml", "~/Views/{0}.cshtml",
            //    "~/Views/{1}/{0}.cshtml", "~/Views/Shared/{0}.cshtml",
            //};
            //this.FileExtensions = new[] { "cshtml" };

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