using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace Micua.Core.Plugin
{
    public abstract class PluginBase : IPlugin
    {
        /// <summary>
        /// 插件名称
        /// </summary>
        public abstract string Name { get; }
        /// <summary>
        /// 插件默认控制器
        /// </summary>
        public abstract string DefaultController { get; }
        /// <summary>
        /// 插件默认行为
        /// </summary>
        public abstract string DefaultAction { get; }
        /// <summary>
        /// 插件初始化安装
        /// </summary>
        /// <param name="context">插件上下文</param>
        public virtual void Install(PluginContext context)
        {
            context.MapRoute(
                Name + "_default",
                Name + "/{controller}/{action}",
                new { controller = DefaultController, action = DefaultAction }
            );
        }
        /// <summary>
        /// 插件卸载
        /// </summary>
        /// <param name="context">插件上下文</param>
        public virtual void Uninstall(PluginContext context)
        {
            context.Routes.Remove(context.Routes[Name + "_default"]);
        }

        private const string TypeCacheName = "MVC-PluginRegistrationTypeCache.xml";

        internal void CreateContextAndRegister(RouteCollection routes, object state)
        {
            PluginContext context = new PluginContext(this.Name, routes, state);
            string str = base.GetType().Namespace;
            if (str != null)
            {
                context.Namespaces.Add(str + ".*");
            }
            this.Install(context);
        }


        private static bool IsPluginType(Type type)
        {
            return (typeof(PluginBase).IsAssignableFrom(type) && (type.GetConstructor(Type.EmptyTypes) != null));
        }

        public static void RegisterAllPlugins()
        {
            RegisterAllPlugins(null);
        }

        public static void RegisterAllPlugins(object state)
        {
            RegisterAllPlugins(RouteTable.Routes, new BuildManagerWrapper(), state);
        }

        internal static void RegisterAllPlugins(RouteCollection routes, IBuildManager buildManager, object state)
        {
            foreach (Type type in TypeCacheUtil.GetFilteredTypesFromAssemblies(TypeCacheName, new Predicate<Type>(PluginBase.IsPluginType), buildManager))
            {
                ((PluginBase)Activator.CreateInstance(type)).CreateContextAndRegister(routes, state);
            }
        }
    }
}
