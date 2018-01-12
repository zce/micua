// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PluginManager.cs" company="Wedn.Net">
//     Copyright © 2014 Wedn.Net. All Rights Reserved.
// </copyright>
// <summary>
//     Sets the application up for the plugin referencing
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.IO;
using System.Web;

using Micua.Core.Plugin;
using Micua.Infrastructure.Utility;

[assembly: PreApplicationStartMethod(typeof(PluginManager), "Initialize")]

namespace Micua.Core.Plugin
{
    /// <summary>
    /// Sets the application up for the plugin referencing
    /// </summary>
    public class PluginManager
    {
        /// <summary>
        /// 插件目录。
        /// </summary>
        private static readonly DirectoryInfo PluginFolder;

        /// <summary>
        /// 插件程序集临时目录。
        /// </summary>
        private static readonly DirectoryInfo PluginShadowFolder;
        static PluginManager()
        {
            PluginFolder = new DirectoryInfo(MachineHelper.MapPath("~/Plugins"));
            PluginShadowFolder = new DirectoryInfo(MachineHelper.MapPath("~/Plugins/Bin"));
        }
        /// <summary>
        /// Initialize
        /// </summary>
        public static void Initialize()
        {
            //lock ("plugin_manager_lock_helper")
            //{
            //    PluginLoader.LoadAssemblies(PluginFolder, PluginShadowFolder);
            //}
            //Plugin.RegisterAllPlugins();
        }
    }
}
