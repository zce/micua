// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Config.cs" company="Wedn.Net">
//   Copyright © 2014 Wedn.Net. All Rights Reserved.
// </copyright>
// <summary>
//   配置信息读取
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Micua.Infrastructure.Utility
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    using System.Xml;

    /// <summary>
    /// 配置信息读取
    /// </summary>
    public static class Config
    {
        /// <summary>
        /// 配置数据
        /// </summary>
        public static IDictionary<string, string> Configs { get; private set; }

        #region 配置文件根目录 _string ConfigRoot
        /// <summary>
        /// 配置文件根目录
        /// </summary>
        private static string ConfigRoot
        {
            get
            {
                var vRoot = ConfigurationManager.AppSettings["config_root"] ?? "~/configs/";
                return MachineHelper.MapPath(vRoot);
            }
        }
        #endregion

        #region 配置文件名 _string ConfigFile
        /// <summary>
        /// 配置文件名
        /// </summary>
        private static string ConfigFile
        {
            get
            {
                var name = ConfigurationManager.AppSettings["config_name"] ?? "micua";
                var status = ConfigurationManager.AppSettings["app_status"] ?? string.Empty;
                var extend = ConfigurationManager.AppSettings["config_ext"] ?? ".config";
                return string.Format("{0}{1}{2}", name, status.Length == 0 ? string.Empty : "." + status, extend);

                // #if DEBUG
                //                 return configName + ".debug.config";
                // #else
                //                 return configName + ".config";
                // #endif
            }
        }
        #endregion

        #region 站点Url(根据请求信息得到) +string SiteUrl
        /// <summary>
        /// 站点Url(根据请求信息得到)
        /// </summary>
        /// <value>The site URL.</value>
        public static string SiteUrl
        {
            get
            {
                var request = System.Web.HttpContext.Current.Request;
                return string.Format("{0}://{1}{2}", request.Url.Scheme, request.Url.Authority, request.ApplicationPath).TrimEnd('/').ToLower();
            }
        }
        #endregion

        #region 主题根目录Url(根据请求信息得到) +string ThemeUrl
        /// <summary>
        /// 主题根目录Url(根据请求信息得到)
        /// </summary>
        /// <value>The Theme URL.</value>
        public static string ThemeUrl
        {
            get
            {
                return string.Format(
                    "{0}{1}{2}",
                    SiteUrl,
                    GetString("theme_root", "/theme/"),
                    Setting.GetString("site_theme", "default")).ToLower();
            }
        }
        #endregion

        #region 数据库连接字符串 +string ConnectionString
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public static string ConnectionString
        {
            get
            {
                var connectionString = new ConnectionString
                {
                    DataSource = string.Format("{0}:{1}", Config.GetString("db_host"), Config.GetString("db_port")),
                    InitialCatalog = Config.GetString("db_name"),
                    IntegratedSecurity = Config.GetBoolean("db_integrated_auth"),
                    UserID = Config.GetString("db_username"),
                    Password = Config.GetString("db_password"),
                    MinPoolSize = Config.GetInt32("db_min_pool_size"),
                    MaxPoolSize = Config.GetInt32("db_max_pool_size"),
                    ConnectTimeout = Config.GetInt32("db_connect_timeout")
                };
                return connectionString.ToString();
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes static members of the <see cref="Config"/> class. 
        /// 配置操作助手类
        /// </summary>
        static Config()
        {
            RefreshConfigs();

            // 监视配置文件修改
            var watcher = new FileSystemWatcher(ConfigRoot, ConfigFile)
                              {
                                  EnableRaisingEvents = true,
                                  NotifyFilter = NotifyFilters.LastWrite
                              };
            watcher.Changed += WatcherChanged;

            // watcher.NotifyFilter = NotifyFilters.Attributes | NotifyFilters.CreationTime | NotifyFilters.DirectoryName | NotifyFilters.FileName | NotifyFilters.LastAccess
            // | NotifyFilters.LastWrite | NotifyFilters.Security | NotifyFilters.Size;
            // watcher.IncludeSubdirectories = true;
        }
        #endregion

        #region Utilities
        /// <summary>
        /// 配置文件发生变化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void WatcherChanged(object sender, FileSystemEventArgs e)
        {
            // ConfigurationManager.RefreshSection(SectionName);
            // _configSection = null;
            Configs = null;
            RefreshConfigs();

            // var thread = new Thread((obj) =>
            // {
            // Configs = null;
            // var flag = RefreshConfigs();
            // while (!flag)
            // {
            // Thread.Sleep(5000);
            // flag = RefreshConfigs();
            // }
            // var t = obj as Thread;
            // if (t != null)
            // t.Abort();
            // });
            // thread.Start(thread);
        }

        /// <summary>
        /// 刷新配置方法
        /// </summary>
        private static void RefreshConfigs()
        {
            lock ("ConfigHelper")
            {
                if (Configs != null)
                {
                    return;
                }
                string filePath = GetConfigPath(ConfigFile);
                if (!File.Exists(filePath))
                {
                    throw new ConfigurationErrorsException(string.Format("配置文件{0}未找到", ConfigFile));
                }

                // 防止文件被占用
                // string tempFile = Path.Combine(_configPath, Guid.NewGuid().ToString("N") + ".txt");
                // File.Copy(filePath, tempFile);
                // var json = File.ReadAllText(filePath);
                // File.Delete(tempFile);

                // try
                // {
                // var obj = JsonHelper.Deserialize<IDictionary<string, string>>(File.ReadAllText(filePath));
                // if (obj == null)
                // {
                // throw new ConfigurationErrorsException(string.Format(Resources.configuration_file_0_does_not_pass_validation, _configFile));
                // }
                // Configs = obj;
                // }
                // catch (Exception)
                // {
                // return false;
                // }
                var doc = new XmlDocument();
                var settings = new XmlReaderSettings { IgnoreComments = true, IgnoreWhitespace = true };
                try
                {
                    using (var reader = XmlReader.Create(filePath, settings))
                    {
                        doc.Load(reader);
                    }

                    XmlNode root = doc.DocumentElement;
                    if (root == null)
                    {
                        throw new ConfigurationErrorsException(string.Format("配置文件{0}验证不通过", ConfigFile));
                    }

                    if (!root.HasChildNodes)
                    {
                        return;
                    }

                    Configs = new Dictionary<string, string>();
                    foreach (XmlNode item in root.ChildNodes)
                    {
                        if (item.Attributes == null)
                        {
                            continue;
                        }

                        var key = item.Attributes["key"].Value.ToLower();
                        if (Configs.ContainsKey(key))
                        {
                            Configs[key] = item.Attributes["value"].Value;
                            continue;
                        }

                        Configs.Add(key, item.Attributes["value"].Value);
                    }
                }
                catch
                {
                }

                // if (_configSection == null)
                // {
                // var obj = ConfigurationManager.GetSection(SectionName);
                // if (obj == null || !(obj is NameValueCollection))
                // {
                // throw new ConfigurationErrorsException(string.Format(Resources.configuration_file_0_does_not_pass_validation, ""));
                // }
                // _configSection = ConfigurationManager.GetSection(SectionName) as NameValueCollection;
                // }
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// 获取配置文件绝对路径
        /// </summary>
        /// <param name="configName">配置文件名</param>
        /// <returns>配置文件绝对路径</returns>
        public static string GetConfigPath(string configName)
        {
            return ConfigRoot + configName;
        }
        #endregion

        #region 获取String类型配置信息 +static string GetString(string key, string def = "")
        /// <summary>
        /// 获取字符串类型配置信息
        /// </summary>
        /// <remarks>
        ///  2013-11-23 14:27 Created By iceStone
        /// </remarks>
        /// <param name="key">设置键</param>
        /// <param name="def">缺省值</param>
        /// <returns>设置 值</returns>
        public static string GetString(string key, string def = "")
        {
            // if (_configSection != null) return _configSection[key] ?? def;
            // RefreshConfig();
            // return _configSection != null ? (_configSection[key] ?? def) : def;
            // if (Configs == null)
            // RefreshConfigs();
            key = key.ToLower();
            return Configs != null && Configs.ContainsKey(key) ? Configs[key] : def;
        }
        #endregion

        #region 获取布尔型类型配置信息值 +static bool GetBoolean(string key, bool def = default(Boolean))
        /// <summary>
        /// 获取布尔型类型配置信息值
        /// </summary>
        /// <remarks>
        ///  2013-11-23 14:27 Created By iceStone
        /// </remarks>
        /// <param name="key">设置键</param>
        /// <param name="def">缺省值</param>
        /// <returns>设置值</returns>
        public static bool GetBoolean(string key, bool def = default(Boolean))
        {
            var res = GetString(key);
            if (res.Length == 0)
            {
                return def;
            }

            bool.TryParse(res, out def);
            return def;
        } 
        #endregion

        #region 获取无符号、数值、整数类型配置信息值 +static char GetChar(string key, char def = default(Char))
        /// <summary>
        /// 获取无符号、数值、整数类型配置信息值
        /// </summary>
        /// <remarks>
        ///  2013-11-23 14:27 Created By iceStone
        /// </remarks>
        /// <param name="key">设置键</param>
        /// <param name="def">缺省值</param>
        /// <returns>设置值</returns>
        public static char GetChar(string key, char def = default(Char))
        {
            var res = GetString(key);
            if (res.Length == 0)
            {
                return def;
            }

            char.TryParse(res, out def);
            return def;
        } 
        #endregion

        #region 获取数值、十进制类型配置信息值 +static decimal GetDecimal(string key, decimal def = default(Decimal))
        /// <summary>
        /// 获取数值、十进制类型配置信息值
        /// </summary>
        /// <remarks>
        ///  2013-11-23 14:27 Created By iceStone
        /// </remarks>
        /// <param name="key">设置键</param>
        /// <param name="def">缺省值</param>
        /// <returns>设置值</returns>
        public static decimal GetDecimal(string key, decimal def = default(Decimal))
        {
            var res = GetString(key);
            if (res.Length == 0)
            {
                return def;
            }

            decimal.TryParse(res, out def);
            return def;
        } 
        #endregion

        #region 获取数值、浮点类型配置信息值 +static double GetDouble(string key, double def = default(Double))
        /// <summary>
        /// 获取数值、浮点类型配置信息值
        /// </summary>
        /// <remarks>
        ///  2013-11-23 14:27 Created By iceStone
        /// </remarks>
        /// <param name="key">设置键</param>
        /// <param name="def">缺省值</param>
        /// <returns>设置值</returns>
        public static double GetDouble(string key, double def = default(Double))
        {
            var res = GetString(key);
            if (res.Length == 0)
            {
                return def;
            }

            double.TryParse(res, out def);
            return def;
        } 
        #endregion

        #region 获取数值、浮点类型配置信息值 +static float GetSingle(string key, float def = default(Single))
        /// <summary>
        /// 获取数值、浮点类型配置信息值
        /// </summary>
        /// <remarks>
        ///  2013-11-23 14:27 Created By iceStone
        /// </remarks>
        /// <param name="key">设置键</param>
        /// <param name="def">缺省值</param>
        /// <returns>设置值</returns>
        public static float GetSingle(string key, float def = default(Single))
        {
            var res = GetString(key);
            if (res.Length == 0)
            {
                return def;
            }

            float.TryParse(res, out def);
            return def;
        } 
        #endregion

        #region 获取无符号、数值、整数类型配置信息值 +static byte GetByte(string key, byte def = default(Byte))
        /// <summary>
        /// 获取无符号、数值、整数类型配置信息值
        /// </summary>
        /// <remarks>
        ///  2013-11-23 14:27 Created By iceStone
        /// </remarks>
        /// <param name="key">设置键</param>
        /// <param name="def">缺省值</param>
        /// <returns>设置值</returns>
        public static byte GetByte(string key, byte def = default(Byte))
        {
            var res = GetString(key);
            if (res.Length == 0)
            {
                return def;
            }

            byte.TryParse(res, out def);
            return def;
        } 
        #endregion

        #region 获取有符号、数值、整数类型配置信息值 +static sbyte GetSByte(string key, sbyte def = default(SByte))
        /// <summary>
        /// 获取有符号、数值、整数类型配置信息值
        /// </summary>
        /// <remarks>
        ///  2013-11-23 14:27 Created By iceStone
        /// </remarks>
        /// <param name="key">设置键</param>
        /// <param name="def">缺省值</param>
        /// <returns>设置值</returns>
        public static sbyte GetSByte(string key, sbyte def = default(SByte))
        {
            var res = GetString(key);
            if (res.Length == 0)
            {
                return def;
            }

            sbyte.TryParse(res, out def);
            return def;
        } 
        #endregion

        #region 获取有符号、数值、整数类型配置信息值 +static short GetInt16(string key, short def = default(Int16))
        /// <summary>
        /// 获取有符号、数值、整数类型配置信息值
        /// </summary>
        /// <remarks>
        ///  2013-11-23 14:27 Created By iceStone
        /// </remarks>
        /// <param name="key">设置键</param>
        /// <param name="def">缺省值</param>
        /// <returns>设置值</returns>
        public static short GetInt16(string key, short def = default(Int16))
        {
            var res = GetString(key);
            if (res.Length == 0)
            {
                return def;
            }

            short.TryParse(res, out def);
            return def;
        } 
        #endregion

        #region 获取无符号、数值、整数类型配置信息值 +static ushort GetUInt16(string key, ushort def = default(UInt16))
        /// <summary>
        /// 获取无符号、数值、整数类型配置信息值
        /// </summary>
        /// <remarks>
        ///  2013-11-23 14:27 Created By iceStone
        /// </remarks>
        /// <param name="key">设置键</param>
        /// <param name="def">缺省值</param>
        /// <returns>设置值</returns>
        public static ushort GetUInt16(string key, ushort def = default(UInt16))
        {
            var res = GetString(key);
            if (res.Length == 0)
            {
                return def;
            }

            ushort.TryParse(res, out def);
            return def;
        } 
        #endregion

        #region 获取有符号、数值、整数类型配置信息值 +static int GetInt32(string key, int def = default(Int32))
        /// <summary>
        /// 获取有符号、数值、整数类型配置信息值
        /// </summary>
        /// <remarks>
        ///  2013-11-23 14:27 Created By iceStone
        /// </remarks>
        /// <param name="key">设置键</param>
        /// <param name="def">缺省值</param>
        /// <returns>设置值</returns>
        public static int GetInt32(string key, int def = default(Int32))
        {
            var res = GetString(key);
            if (res.Length == 0)
            {
                return def;
            }

            int.TryParse(res, out def);
            return def;
        } 
        #endregion

        #region 获取无符号、数值、整数类型配置信息值 +static uint GetUInt32(string key, uint def = default(UInt32))
        /// <summary>
        /// 获取无符号、数值、整数类型配置信息值
        /// </summary>
        /// <remarks>
        ///  2013-11-23 14:27 Created By iceStone
        /// </remarks>
        /// <param name="key">设置键</param>
        /// <param name="def">缺省值</param>
        /// <returns>设置值</returns>
        public static uint GetUInt32(string key, uint def = default(UInt32))
        {
            var res = GetString(key);
            if (res.Length == 0)
            {
                return def;
            }

            uint.TryParse(res, out def);
            return def;
        } 
        #endregion

        #region 获取有符号、数值、整数类型配置信息值 +static long GetInt64(string key, long def = default(Int64))
        /// <summary>
        /// 获取有符号、数值、整数类型配置信息值
        /// </summary>
        /// <remarks>
        ///  2013-11-23 14:27 Created By iceStone
        /// </remarks>
        /// <param name="key">设置键</param>
        /// <param name="def">缺省值</param>
        /// <returns>设置值</returns>
        public static long GetInt64(string key, long def = default(Int64))
        {
            var res = GetString(key);
            if (res.Length == 0)
            {
                return def;
            }

            long.TryParse(res, out def);
            return def;
        } 
        #endregion

        #region 获取无符号、数值、整数类型配置信息值 +static ulong GetUInt64(string key, ulong def = default(UInt64))
        /// <summary>
        /// 获取无符号、数值、整数类型配置信息值
        /// </summary>
        /// <remarks>
        ///  2013-11-23 14:27 Created By iceStone
        /// </remarks>
        /// <param name="key">设置键</param>
        /// <param name="def">缺省值</param>
        /// <returns>设置值</returns>
        public static ulong GetUInt64(string key, ulong def = default(UInt64))
        {
            var res = GetString(key);
            if (res.Length == 0)
            {
                return def;
            }

            ulong.TryParse(res, out def);
            return def;
        } 
        #endregion

        #region 获取时间类型配置信息值 +static DateTime GetDateTime(string key, DateTime def = default(DateTime))
        /// <summary>
        /// 获取时间类型配置信息值
        /// </summary>
        /// <remarks>
        ///  2013-11-23 14:27 Created By iceStone
        /// </remarks>
        /// <param name="key">设置键</param>
        /// <param name="def">缺省值</param>
        /// <returns>设置值</returns>
        public static DateTime GetDateTime(string key, DateTime def = default(DateTime))
        {
            var res = GetString(key);
            if (res.Length == 0)
            {
                return def;
            }

            DateTime.TryParse(res, out def);
            return def;
        } 
        #endregion

        #region 获取Guid类型配置信息值 +static Guid GetGuid(string key, Guid def = default(Guid))
        /// <summary>
        /// 获取Guid类型配置信息值
        /// </summary>
        /// <remarks>
        ///  2013-11-23 14:27 Created By iceStone
        /// </remarks>
        /// <param name="key">设置键</param>
        /// <param name="def">缺省值</param>
        /// <returns>设置值</returns>
        public static Guid GetGuid(string key, Guid def = default(Guid))
        {
            var res = GetString(key);
            if (res.Length == 0)
            {
                return def;
            }

            Guid.TryParse(res, out def);
            return def;
        } 
        #endregion
    }
}
