// +----------------------------------------------------------------------
// | Micua [ DO IT BATTER ! ]
// +----------------------------------------------------------------------
// | Copyright © 2014 Wedn.Net. All Rights Reserved.
// +----------------------------------------------------------------------
// | Summary : 基于NVelocity的视图
// +----------------------------------------------------------------------
// | Author  : iceStone <ice@wedn.net>
// +----------------------------------------------------------------------

namespace Micua.Core.ViewEngine
{
    using System.IO;
    using System.Web.Mvc;

    using Commons.Collections;

    using Micua.Infrastructure.Utility;

    using NVelocity;
    using NVelocity.App;
    using NVelocity.Context;
    using NVelocity.Runtime;

    /// <summary>
    /// The n velocity view.
    /// </summary>
    public class NVelocityView1 : IView
    {
        private VelocityEngine velocity;
        private ControllerContext controllerContext;
        private IContext context;
        private string viewPath;
        private string viewName;
        private string layoutPath;
        private string layoutName;
        public NVelocityView1(ControllerContext controllerContext, string viewPath, string viewName, string layoutPath = "", string layoutName = "")
        {
            this.controllerContext = controllerContext;
            this.viewPath = viewPath;
            this.viewName = viewName;
            this.layoutPath = layoutPath;
            this.layoutName = layoutName;
            this.velocity = new VelocityEngine();
            // 使用设置初始化VelocityEngine
            var props = new ExtendedProperties();
            props.AddProperty(RuntimeConstants.FILE_RESOURCE_LOADER_PATH, MachineHelper.MapPath(viewPath));
            // props.AddProperty(RuntimeConstants, viewPath);
            props.AddProperty(RuntimeConstants.INPUT_ENCODING, "utf-8");

            //   props.AddProperty(RuntimeConstants.OUTPUT_ENCODING, "gb2312");
            //    props.AddProperty(RuntimeConstants.RESOURCE_LOADER, "file");

            //  props.SetProperty(RuntimeConstants.RESOURCE_MANAGER_CLASS, "NVelocity.Runtime.Resource.ResourceManagerImpl\\,NVelocity");

            this.velocity.Init(props);
            // RuntimeConstants.RESOURCE_MANAGER_CLASS 
            // 为模板变量赋值
            this.context = new VelocityContext();
        }

        /// <summary>
        /// The render.
        /// </summary>
        /// <param name="viewContext">
        /// The view context.
        /// </param>
        /// <param name="writer">
        /// The writer.
        /// </param>
        public void Render(ViewContext viewContext, TextWriter writer)
        {
            // 系统变量
            this.context.Put("SiteUrl", Config.SiteUrl);
            this.context.Put(
                "ThemeUrl",
                string.Format(
                    "{0}{1}{2}",
                    Config.SiteUrl,
                    Config.GetString("theme_root", "/theme/"),
                    Setting.GetString("site_theme", "default")).ToLower());
            this.context.Put("Settings", Setting.Settings);
            this.context.Put("Configs", Config.Configs);
            this.context.Put("RouteData", viewContext.RouteData);
            this.context.Put("UrlHelper", new UrlHelper(viewContext.RequestContext));

            // Action视图包
            foreach (var item in viewContext.ViewData)
            {
                this.context.Put(item.Key, item.Value);
            }

            var template = this.velocity.GetTemplate(this.viewName);
            if (this.layoutPath.Length != 0 && this.layoutName.Length != 0 && !controllerContext.IsChildAction)
            {
                var mainWriter = new StringWriter();
                template.Merge(this.context, mainWriter);
                this.context.Put(Config.GetString("template_content_placeholder", "CONTENT_PLACEHOLDER"), mainWriter.ToString());
                this.velocity = new VelocityEngine();
                // 使用设置初始化VelocityEngine
                var props = new ExtendedProperties();
                props.AddProperty(RuntimeConstants.FILE_RESOURCE_LOADER_PATH, MachineHelper.MapPath(this.layoutPath));
                // props.AddProperty(RuntimeConstants, viewPath);
                props.AddProperty(RuntimeConstants.INPUT_ENCODING, "utf-8");
                this.velocity.Init(props);
                Template layout = this.velocity.GetTemplate(this.layoutName);
                layout.Merge(this.context, writer);
            }
            else
            {
                template.Merge(this.context, writer);
            }
        }
    }
}
