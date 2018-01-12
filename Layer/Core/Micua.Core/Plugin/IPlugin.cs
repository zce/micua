// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPlugin.cs" company="Wedn.Net">
//     Copyright © 2014 Wedn.Net. All Rights Reserved.
// </copyright>
// <summary>
//   Defines the IPlugin type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Micua.Core.Plugin
{
    public interface IPlugin
    {
        /// <summary>
        /// 插件名称
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 插件默认控制器
        /// </summary>
        string DefaultController { get; }

        /// <summary>
        /// 插件默认行为
        /// </summary>
        string DefaultAction { get; }

        /// <summary>
        /// 插件初始化安装
        /// </summary>
        /// <param name="context">插件上下文</param>
        void Install(PluginContext context);

        /// <summary>
        /// 插件卸载
        /// </summary>
        /// <param name="context">插件上下文</param>
        void Uninstall(PluginContext context);
    }
}
