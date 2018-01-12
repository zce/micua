namespace Micua.Core.ViewEngine
{
    using System;
    using System.Web.Mvc;
    using System.Web.Mvc.Html;

    /// <summary>
    /// The html extension duck.
    /// </summary>
    public class HtmlExtensionDuck : ExtensionDuck
    {
        /// <summary>
        /// The html extension types.
        /// </summary>
        public static readonly Type[] HtmlExtensionTypes = {
                                        typeof(DisplayExtensions),
                                        typeof(DisplayTextExtensions),
                                        typeof(EditorExtensions),
                                        typeof(FormExtensions), 
                                        typeof(InputExtensions), 
                                        typeof(LabelExtensions),
                                        typeof(LinkExtensions), 
                                        typeof(MvcForm),
                                        typeof(PartialExtensions),
                                        typeof(RenderPartialExtensions),
                                        typeof(SelectExtensions),
                                        typeof(TextAreaExtensions),
                                        typeof(ValidationExtensions),
                                        typeof(HtmlExtensions),
                                    };

        public HtmlExtensionDuck(ViewContext viewContext, IViewDataContainer container)
            : this(new HtmlHelper(viewContext, container))
        {
        }

        public HtmlExtensionDuck(HtmlHelper htmlHelper)
            : this(htmlHelper, HtmlExtensionTypes)
        {
        }

        public HtmlExtensionDuck(HtmlHelper htmlHelper, params Type[] extentionTypes)
            : base(htmlHelper, extentionTypes)
        {
        }
    }
}
