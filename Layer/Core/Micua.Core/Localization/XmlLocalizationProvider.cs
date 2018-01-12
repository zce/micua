// --------------------------------------------------------------------------------------------------------------------
// <copyright file="XmlLocalizationProvider.cs" company="Wedn.Net">
//     Copyright © 2014 Wedn.Net. All Rights Reserved.
// </copyright>
// <summary>
//   The xml localization provider.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Micua.Core.Localization
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Xml;

    using Micua.Infrastructure.Utility;

    /// <summary>
    /// The xml localization provider.
    /// </summary>
    public class XmlLocalizationProvider : ILocalizationProvider
    {
        /// <summary>
        /// Gets or sets the dictionary.
        /// </summary>
        public IDictionary<string, string> Dictionary { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlLocalizationProvider"/> class.
        /// </summary>
        /// <param name="culture">区域信息</param>
        /// <exception cref="FileLoadException">没有找到{0}语言包</exception>
        public XmlLocalizationProvider(CultureInfo culture)
        {
            this.Dictionary = new Dictionary<string, string>();
            var package = MachineHelper.MapPath(
                                    string.Format(
                                        "{0}lang.{1}.xml",
                                        Config.GetString("lang_package_root", "~/contents/languages/"),
                                        culture));
            if (!File.Exists(package))
            {
                throw new FileLoadException(string.Format("没有找到{0}语言包", package));
            }

            var doc = new XmlDocument();
            var settings = new XmlReaderSettings { IgnoreComments = true, IgnoreWhitespace = true };
            try
            {
                using (var reader = XmlReader.Create(package, settings))
                {
                    doc.Load(reader);
                }

                XmlNode root = doc.DocumentElement;
                if (root == null)
                {
                    return;
                }

                if (!root.HasChildNodes)
                {
                    return;
                }

                foreach (XmlNode item in root.ChildNodes)
                {
                    var key = item.Name.ToLower();
                    if (this.Dictionary.ContainsKey(key))
                    {
                        this.Dictionary[key] = item.InnerText.Trim();
                    }

                    this.Dictionary.Add(key, item.InnerText.Trim());
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// 转换字符串
        /// </summary>
        /// <param name="input">输入</param>
        /// <returns>输出</returns>
        public string Parse(string input)
        {
            string result;
            return this.Dictionary.TryGetValue(input.ToKeyCase(), out result) ? result : input;
        }

        /// <summary>
        /// 转换格式化字符串
        /// </summary>
        /// <param name="input">输入</param>
        /// <param name="args">一个对象数组，其中包含零个或多个要设置格式的对象</param>
        /// <returns>输出</returns>
        public string Parse(string input, params object[] args)
        {
            return this.Parse(input).Format(args);
        }
    }
}