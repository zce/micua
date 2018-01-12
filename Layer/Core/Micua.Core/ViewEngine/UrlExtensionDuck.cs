namespace Micua.Core.ViewEngine
{
    using System;
    using System.Web.Mvc;
    using System.Web.Mvc.Html;
    using System.Web.Routing;

    /// <summary>
    /// The html extension duck.
    /// </summary>
    public class UrlExtensionDuck : ExtensionDuck
    {
        /// <summary>
        /// The Url extension types.
        /// </summary>
        public static readonly Type[] UrlExtensionTypes = {
                                        typeof(UrlExtensions),
                                    };

        public UrlExtensionDuck(RequestContext request)
            : this(new UrlHelper(request))
        {
        }

        public UrlExtensionDuck(UrlHelper urlHelper)
            : this(urlHelper, UrlExtensionTypes)
        {
        }

        public UrlExtensionDuck(UrlHelper urlHelper, params Type[] extentionTypes)
            : base(urlHelper, extentionTypes)
        {
        }
    }
}
