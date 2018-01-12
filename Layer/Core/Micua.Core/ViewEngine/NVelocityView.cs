// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NVelocityView.cs" company="Wedn.Net">
//   Copyright © 2014 Wedn.Net. All Rights Reserved.
// </copyright>
// <summary>
//   Defines the NVelocityView type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Micua.Core.ViewEngine
{
    using System;
    using System.Collections;
    using System.IO;
    using System.Web.Mvc;

    using Micua.Core.ComponentModel;
    using Micua.Core.Localization;
    using Micua.Infrastructure.Utility;

    using NVelocity;

    public class NVelocityView : IViewDataContainer, IView
    {
        private ControllerContext _controllerContext;
        private readonly Template _masterTemplate;
        private readonly Template _viewTemplate;

        public NVelocityView(ControllerContext controllerContext, string viewPath, string masterPath)
            : this(
                controllerContext,
                NVelocityViewEngine.Default.GetTemplate(viewPath),
                NVelocityViewEngine.Default.GetTemplate(masterPath))
        {
        }
        public NVelocityView(ControllerContext controllerContext, Template viewTemplate, Template masterTemplate)
        {
            this._controllerContext = controllerContext;
            this._viewTemplate = viewTemplate;
            this._masterTemplate = masterTemplate;
        }

        public Template ViewTemplate
        {
            get { return this._viewTemplate; }
        }

        public Template MasterTemplate
        {
            get { return this._masterTemplate; }
        }

        private VelocityContext CreateContext(ViewContext context)
        {
            var entries = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
            if (context.ViewData != null)
            {
                foreach (var pair in context.ViewData)
                {
                    entries[pair.Key] = pair.Value;
                }
            }

            entries["viewdata"] = context.ViewData;
            entries["tempdata"] = context.TempData;
            entries["routedata"] = context.RouteData;
            entries["controller"] = context.Controller;
            entries["httpcontext"] = context.HttpContext;
            entries["viewbag"] = context.ViewData;
            entries["settings"] = new DictionaryWapper(Setting.Settings);
            entries["configs"] = new DictionaryWapper(Config.Configs);
            entries["siteurl"] = Config.SiteUrl;
            entries["themeurl"] = Config.ThemeUrl;
            entries["powered"] = MicuaInfo.Powered;

            this.CreateAndAddHelpers(entries, context);

            return new VelocityContext(entries);
        }

        private void CreateAndAddHelpers(Hashtable entries, ViewContext context)
        {
            entries["html"] = entries["htmlhelper"] = new HtmlExtensionDuck(context, this);
            entries["url"] = entries["urlhelper"] = new UrlExtensionDuck(context.RequestContext);
            entries["ajax"] = entries["ajaxhelper"] = new AjaxHelper(context, this);
            entries["lang"] = entries["langhelper"] = new XmlLocalizationProvider(LocalizationHelper.CurrentUICulture);
        }

        public void Render(ViewContext viewContext, TextWriter writer)
        {
            this.ViewData = viewContext.ViewData;

            bool hasLayout = this._masterTemplate != null;

            VelocityContext context = this.CreateContext(viewContext);

            if (hasLayout)
            {
                var sw = new StringWriter();
                this._viewTemplate.Merge(context, sw);

                context.Put(Config.GetString("template_content_placeholder", "CONTENT_PLACEHOLDER"), sw.GetStringBuilder());

                this._masterTemplate.Merge(context, writer);
            }
            else
            {
                this._viewTemplate.Merge(context, writer);
            }
        }

        private ViewDataDictionary viewData;
        public ViewDataDictionary ViewData
        {
            get
            {
                if (this.viewData == null)
                {
                    return this._controllerContext.Controller.ViewData;
                }

                return this.viewData;
            }

            set
            {
                this.viewData = value;
            }
        }
    }
}