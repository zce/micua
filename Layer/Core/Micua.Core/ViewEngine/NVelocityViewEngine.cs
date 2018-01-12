namespace Micua.Core.ViewEngine
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Web.Mvc;

    using Commons.Collections;

    using Micua.Infrastructure.Utility;

    using NVelocity;
    using NVelocity.App;
    using NVelocity.Runtime;

    public class NVelocityViewEngine : VirtualPathProviderViewEngine, IViewEngine
    {
        public static NVelocityViewEngine Default = null;
        private static readonly IDictionary<string, object> DefaultProperties = new Dictionary<string, object>();
        private readonly VelocityEngine engine;

        static NVelocityViewEngine()
        {
            string targetViewFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "views");
            //DEFAULT_PROPERTIES.Add(RuntimeConstants.RESOURCE_LOADER, "file");
            DefaultProperties.Add(RuntimeConstants.FILE_RESOURCE_LOADER_PATH, targetViewFolder);
            DefaultProperties.Add("file.resource.loader.class", "Micua.Core.ViewEngine.FileResourceLoaderEx\\, Micua.Core");

            Default = new NVelocityViewEngine();
        }

        public NVelocityViewEngine()
            : this(DefaultProperties)
        {
        }

        public NVelocityViewEngine(IDictionary<string, object> properties)
        {
            string themeRoot = "~" + Config.GetString("theme_root", "/Themes/")
                               + Setting.GetString("site_theme", "default");

            string areaRoot = themeRoot + "/Plugins";
            this.AreaViewLocationFormats = new[]
            {
                areaRoot + "/{2}/Views/{1}/{0}.htm",
                themeRoot + "/Areas/{2}/Views/{1}/{0}.htm",
                "~/Plugins/{2}/Views/{1}/{0}.htm", 
                "~/Areas/{2}/Views/{1}/{0}.htm",
            };
            this.AreaMasterLocationFormats = new[]
            {
                areaRoot + "/{2}/Views/Shared/{0}.htm",
                themeRoot + "/Areas/{2}/Views/Shared/{0}.htm",
                "~/Plugins/{2}/Views/Shared/{0}.htm",
                "~/Areas/{2}/Views/Shared/{0}.htm",
            };
            this.AreaPartialViewLocationFormats = new[]
            {
                areaRoot + "/{2}/Views/{1}/{0}.htm",
                areaRoot + "/{2}/Views/Shared/{0}.htm",
                themeRoot + "/Areas/{2}/Views/{1}/{0}.htm",
                themeRoot + "/Areas/{2}/Views/Shared/{0}.htm",
                "~/Plugins/{2}/Views/{1}/{0}.htm",
                "~/Plugins/{2}/Views/Shared/{0}.htm",
                "~/Areas/{2}/Views/{1}/{0}.htm",
                "~/Areas/{2}/Views/Shared/{0}.htm",
            };
            this.ViewLocationFormats = new[]
            {
                themeRoot + "/Views/{0}.htm", 
                themeRoot + "/Views/{1}/{0}.htm",
                "~/Views/{0}.htm", 
                "~/Views/{1}/{0}.htm",
            };
            this.MasterLocationFormats = new[]
            {
                themeRoot + "/Views/Shared/{0}.htm", 
                "~/Views/Shared/{0}.htm",
            };
            this.PartialViewLocationFormats = new[]
            {
                themeRoot + "/Views/{0}.htm", themeRoot + "/Views/{1}/{0}.htm",
                themeRoot + "/Views/Shared/{0}.htm", "~/Views/{0}.htm",
                "~/Views/{1}/{0}.htm", "~/Views/Shared/{0}.htm",
            };
            this.FileExtensions = new[] { "htm" };

            if (properties == null)
            {
                properties = DefaultProperties;
            }

            var props = new ExtendedProperties();
            foreach (var item in properties)
            {
                props.AddProperty(item.Key, item.Value);
            }

            this.engine = new VelocityEngine();
            this.engine.Init(props);
        }

        protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
        {
            Template viewTemplate = this.GetTemplate(viewPath);
            Template masterTemplate = this.GetTemplate(masterPath);
            var view = new NVelocityView(controllerContext, viewTemplate, masterTemplate);
            return view;
        }
        protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath)
        {
            Template viewTemplate = this.GetTemplate(partialPath);
            var view = new NVelocityView(controllerContext, viewTemplate, null);
            return view;
        }
        public Template GetTemplate(string viewPath)
        {
            if (string.IsNullOrEmpty(viewPath))
            {
                return null;
            }

            return this.engine.GetTemplate(MachineHelper.MapPath(viewPath));
        }

    }
}
