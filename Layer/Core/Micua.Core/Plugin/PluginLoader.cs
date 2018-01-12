// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PluginLoader.cs" company="Wedn.Net">
//     Copyright © 2014 Wedn.Net. All Rights Reserved.
// </copyright>
// <summary>
//   Defines the PluginLoader type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

// [assembly: PreApplicationStartMethod(typeof(PluginLoader), "Initialize")]
namespace Micua.Core.Plugin
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Web.Compilation;

    public class PluginLoader
    {
        public static void LoadAssemblies(DirectoryInfo pluginDir, DirectoryInfo shadowDir)
        {
            var shadowFiles = shadowDir.GetFiles("*.dll", SearchOption.AllDirectories);
            foreach (var item in shadowFiles)
            {
                try
                {
                     File.Delete(item.FullName);
                }
                catch (Exception) { }
            }
            var binFiles = pluginDir.GetFiles("*.dll", SearchOption.AllDirectories);
            if (binFiles.Length == 0) return;

            foreach (var plug in binFiles)
            {
                //running in full trust
                //************
                //if (GetCurrentTrustLevel() != AspNetHostingPermissionLevel.Unrestricted)
                //set in web.config, probing to plugin\temp and copy all to that folder
                //************************
                //var shadowCopyPlugFolder = new DirectoryInfo(AppDomain.CurrentDomain.DynamicDirectory);
                var shadowCopiedPlug = new FileInfo(Path.Combine(shadowDir.FullName, plug.Name));
                try
                {
                    File.Copy(plug.FullName, shadowCopiedPlug.FullName, true); //TODO: Exception handling here...
                }
                catch (Exception) { }
                var shadowCopiedAssembly = Assembly.Load(AssemblyName.GetAssemblyName(shadowCopiedPlug.FullName));

                //add the reference to the build manager
                BuildManager.AddReferencedAssembly(shadowCopiedAssembly);
            }
        }

        //private static AspNetHostingPermissionLevel GetCurrentTrustLevel()
        //{
        //    foreach (AspNetHostingPermissionLevel trustLevel in
        //        new AspNetHostingPermissionLevel[]
        //            {
        //                AspNetHostingPermissionLevel.Unrestricted,
        //                AspNetHostingPermissionLevel.High,
        //                AspNetHostingPermissionLevel.Medium,
        //                AspNetHostingPermissionLevel.Low,
        //                AspNetHostingPermissionLevel.Minimal
        //            })
        //    {
        //        try
        //        {
        //            new AspNetHostingPermission(trustLevel).Demand();
        //        }
        //        catch (System.Security.SecurityException)
        //        {
        //            continue;
        //        }

        //        return trustLevel;
        //    }

        //    return AspNetHostingPermissionLevel.None;
        //}

    }
}