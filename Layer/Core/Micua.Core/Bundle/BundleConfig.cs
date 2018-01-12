// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Config.cs" company="Wedn.Net">
//   Copyright © 2014 Wedn.Net. All Rights Reserved.
// </copyright>
// <summary>
//   配置信息读取
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Micua.Core.Bundle
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web.Optimization;
    using System.Xml;

    using Micua.Infrastructure.Utility;

    /// <summary>
    /// 资源文件捆绑配置
    /// </summary>
    /// <remarks>
    ///  2013-11-23 22:44 Created By iceStone
    /// </remarks>
    public class BundleConfig
    {
        /// <summary>
        /// The bundle configuration path
        /// </summary>
        public static string BundleConfigPath
        {
            get
            {
                return Config.GetConfigPath(Config.GetString("bundle_config_file", "bundle.config"));
            }
        }

        /// <summary>
        /// 资源文件捆绑
        /// </summary>
        /// <remarks>
        ///  2013-11-23 22:44 Created By iceStone
        /// </remarks>
        /// <exception cref="System.Exception">
        /// 未配置/Config/Bundle.config文件
        /// </exception>
        public static void RegisterBundles(BundleCollection bundles)
        {
            var doc = new XmlDocument();
            var settings = new XmlReaderSettings { IgnoreComments = true, IgnoreWhitespace = true };
            // var configPath = MachineHelper.MapPath(BundleConfigPath);
            if (!File.Exists(BundleConfigPath))
            {
                return;
            }

            using (var reader = XmlReader.Create(BundleConfigPath, settings))
            {
                doc.Load(reader);
            }

            XmlNode root = doc.DocumentElement;
            if (root == null)
            {
                throw new Exception(string.Format("未配置{0}文件", Config.GetString("bundle_config_file", "bundle.config")));
            }

            // Regester Style 
            var styleList = root.ChildNodes[0].ChildNodes; // .SelectNodes("styles/item");
            if (styleList.Count > 0)
            {
                foreach (XmlNode node in styleList)
                {
                    string path = CheckNodeRegedit(node);
                    if (string.IsNullOrEmpty(path))
                    {
                        continue;
                    }

                    var bound = new StyleBundle(path);
                    var files = GetFilesFormNode(node);
                    if (!files.Any())
                    {
                        continue;
                    }

                    bound.Include(files.ToArray());
                    bundles.Add(bound);
                }
            }

            // Regester Script 
            var scriptList = root.ChildNodes[1].ChildNodes;
            if (scriptList.Count > 0)
            {
                foreach (XmlNode node in scriptList)
                {
                    string path = CheckNodeRegedit(node);
                    if (string.IsNullOrEmpty(path))
                    {
                        continue;
                    }

                    var bound = new ScriptBundle(path);
                    var files = GetFilesFormNode(node);
                    if (!files.Any())
                    {
                        continue;
                    }

                    bound.Include(files.ToArray());
                    bundles.Add(bound);
                }
            }

            // BundleTable.EnableOptimizations = true;
        }

        /// <summary>
        /// 如果内容为空则不添加
        /// </summary>
        /// <remarks>
        ///  2013-11-23 22:44 Created By iceStone
        /// </remarks>
        /// <param name="node">The node.</param>
        /// <returns>List{System.String}.</returns>
        private static IEnumerable<string> GetFilesFormNode(XmlNode node)
        {
            return node.ChildNodes.Cast<XmlNode>()
                .Select(nodeFile => nodeFile.InnerText.Trim())
                .Where(text => !string.IsNullOrEmpty(text));
        }

        /// <summary>
        /// 检查注册的Node
        /// </summary>
        /// <remarks>
        ///  2013-11-23 22:44 Created By iceStone
        /// </remarks>
        /// <param name="node">The node.</param>
        /// <returns>System.String.</returns>
        private static string CheckNodeRegedit(XmlNode node)
        {
            if (node.Attributes == null)
            {
                return string.Empty;
            }

            var pathAtt = node.Attributes["path"];
            if (pathAtt == null || string.IsNullOrEmpty(pathAtt.Value.Trim()) || node.ChildNodes.Count == 0)
            {
                return string.Empty;
            }

            return pathAtt.Value.Trim();
        }


        ///// <summary>
        ///// 注册捆绑配置
        ///// </summary>
        //public static void RegisterBundles(BundleCollection bundles)
        //{
        //    ////后台基本样式
        //    //var adminCssBase = ;
        //    //bundles.Add(new Bundle("~/content/scripts/base").Include("~/Content/Scripts/bootstrap/bootstrap.js"));
        //    bundles.Add(new ScriptBundle("~/content/scripts/base").Include(
        //        "~/Content/Scripts/jquery/jquery.js"
        //        , "~/Content/Scripts/bootstrap/bootstrap.js"
        //        , "~/Content/Scripts/admin.js"
        //    ));
        //    bundles.Add(new StyleBundle("~/content/styles/base").Include(
        //        "~/Content/Styles/admin-layout.css"
        //        , "~/Content/Styles/bootstrap.css"
        //        , "~/Content/Styles/bootstrap-responsive.css"
        //    ));
        //    BundleTable.EnableOptimizations = true;
        //}
    }
}