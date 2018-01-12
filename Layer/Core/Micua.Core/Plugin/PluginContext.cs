// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PluginContext.cs" company="Wedn.Net">
//     Copyright © 2014 Wedn.Net. All Rights Reserved.
// </copyright>
// <summary>
//   Defines the PluginContext type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Micua.Core.Plugin
{
    using System.Web.Mvc;
    using System.Web.Routing;

    using Micua.Core.Routing;

    public class PluginContext : AreaRegistrationContext
    {
        public PluginContext(string name, RouteCollection routes, object state)
            : base(name, routes, state)
        {

        }
        public new Route MapRoute(string name, string url)
        {
            return this.MapRoute(name, url, null);
        }

        public new Route MapRoute(string name, string url, object defaults)
        {
            return this.MapRoute(name, url, defaults, null);
        }

        public new Route MapRoute(string name, string url, string[] namespaces)
        {
            return this.MapRoute(name, url, null, namespaces);
        }

        public new Route MapRoute(string name, string url, object defaults, object constraints)
        {
            return this.MapRoute(name, url, defaults, constraints, null);
        }

        public new Route MapRoute(string name, string url, object defaults, string[] namespaces)
        {
            return this.MapRoute(name, url, defaults, null, namespaces);
        }

        public new Route MapRoute(string name, string url, object defaults, object constraints, string[] namespaces)
        {
            //if (namespaces == null && Namespaces != null)
            //    namespaces = Namespaces.ToArray();

            //var route = Routes.MapRoute(name, string.Empty, url, defaults, constraints, namespaces);
            //route.DataTokens["area"] = AreaName;
            //route.DataTokens["plugin"] = AreaName;

            //bool useNamespaceFallback = (namespaces == null || namespaces.Length == 0);
            //route.DataTokens["UseNamespaceFallback"] = useNamespaceFallback;

            //return route;
            return this.MapRoute(name, string.Empty, url, defaults, constraints, namespaces);
        }

    }
}
