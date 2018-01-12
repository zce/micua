// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Setting.cs" company="Wedn.Net">
//   Copyright © 2014 Wedn.Net. All Rights Reserved.
// </copyright>
// <summary>
//   设置信息类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Micua.Infrastructure.Utility
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 设置信息类
    /// </summary>
    /// <remarks>
    ///  2013-11-23 14:27 Created By iceStone
    /// </remarks>
    public static class Setting
    {
        /// <summary>
        /// The lock helper.
        /// </summary>
        const string LockHelper = "setting_lock_helper";

        /// <summary>
        /// 设置信息字典(数据仓储)
        /// </summary>
        public static IDictionary<string, string> Settings { get; private set; }

        /// <summary>
        /// Initializes static members of the <see cref="Setting"/> class.
        /// </summary>
        static Setting()
        {
            Settings = new Dictionary<string, string>();
        }

        #region 设置设置信息字典 +static void Set(IDictionary<string, string> data)
        /// <summary>
        /// 设置设置信息字典
        /// </summary>
        /// <param name="data">设置信息字典</param>
        public static void Set(IDictionary<string, string> data)
        {
            lock (LockHelper)
            {
                foreach (var item in data)
                {
                    var key = item.Key.ToLower();
                    if (Settings.ContainsKey(key))
                        Settings[key] = item.Value;
                    else
                        Settings.Add(key, item.Value);
                }
            }
        }
        #endregion

        #region 修改信息字典值 +static void Set(string key, string value)
        /// <summary>
        /// 修改信息字典值
        /// </summary>
        /// <remarks>
        ///  2013-11-23 14:27 Created By iceStone
        /// </remarks>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public static void Set(string key, string value)
        {
            key = key.ToLower();
            lock (LockHelper)
            {
                if (Settings.ContainsKey(key))
                    Settings[key] = value;
                else
                    Settings.Add(key, value);
            }
        }
        #endregion

        #region 移除单个设置 +static void Remove(string key)
        /// <summary>
        /// 移除单个设置
        /// </summary>
        /// <remarks>
        ///  2013-11-23 14:27 Created By iceStone
        /// </remarks>
        /// <param name="key">键</param>
        public static void Remove(string key)
        {
            key = key.ToLower();
            lock (LockHelper)
            {
                Settings.Remove(key);
            }
        }
        #endregion

        #region 移除多个设置 +static void Remove(params string[] keys)
        /// <summary>
        /// 移除多个设置
        /// </summary>
        /// <remarks>
        ///  2013-11-23 14:27 Created By iceStone
        /// </remarks>
        /// <param name="keys">键</param>
        public static void Remove(params string[] keys)
        {
            lock (LockHelper)
            {
                foreach (var key in keys)
                {
                    Settings.Remove(key.ToLower());
                }
            }
        }
        #endregion

        #region 获取String类型设置信息 +static string GetString(string key, string def = "")
        /// <summary>
        /// 获取字符串类型设置信息
        /// </summary>
        /// <remarks>
        ///  2013-11-23 14:27 Created By iceStone
        /// </remarks>
        /// <param name="key">设置键</param>
        /// <param name="def">缺省值</param>
        /// <returns>设置值</returns>
        public static string GetString(string key, string def = "")
        {
            // if (_configSection != null) return _configSection[key] ?? def;
            // RefreshConfig();
            // return _configSection != null ? (_configSection[key] ?? def) : def;
            // if (Configs == null)
            //    RefreshConfigs();
            key = key.ToLower();
            return Settings != null && Settings.ContainsKey(key) ? Settings[key] : def;
        }
        #endregion

        #region 获取布尔型类型设置信息值 +static bool GetBoolean(string key, bool def = default(Boolean))
        /// <summary>
        /// 获取布尔型类型设置信息值
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

        #region 获取无符号、数值、整数类型设置信息值 +static char GetChar(string key, char def = default(Char))
        /// <summary>
        /// 获取无符号、数值、整数类型设置信息值
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

        #region 获取数值、十进制类型设置信息值 +static decimal GetDecimal(string key, decimal def = default(Decimal))
        /// <summary>
        /// 获取数值、十进制类型设置信息值
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

        #region 获取数值、浮点类型设置信息值 +static double GetDouble(string key, double def = default(Double))
        /// <summary>
        /// 获取数值、浮点类型设置信息值
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

        #region 获取数值、浮点类型设置信息值 +static float GetSingle(string key, float def = default(Single))
        /// <summary>
        /// 获取数值、浮点类型设置信息值
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

        #region 获取无符号、数值、整数类型设置信息值 +static byte GetByte(string key, byte def = default(Byte))
        /// <summary>
        /// 获取无符号、数值、整数类型设置信息值
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

        #region 获取有符号、数值、整数类型设置信息值 +static sbyte GetSByte(string key, sbyte def = default(SByte))
        /// <summary>
        /// 获取有符号、数值、整数类型设置信息值
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

        #region 获取有符号、数值、整数类型设置信息值 +static short GetInt16(string key, short def = default(Int16))
        /// <summary>
        /// 获取有符号、数值、整数类型设置信息值
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

        #region 获取无符号、数值、整数类型设置信息值 +static ushort GetUInt16(string key, ushort def = default(UInt16))
        /// <summary>
        /// 获取无符号、数值、整数类型设置信息值
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

        #region 获取有符号、数值、整数类型设置信息值 +static int GetInt32(string key, int def = default(Int32))
        /// <summary>
        /// 获取有符号、数值、整数类型设置信息值
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

        #region 获取无符号、数值、整数类型设置信息值 +static uint GetUInt32(string key, uint def = default(UInt32))
        /// <summary>
        /// 获取无符号、数值、整数类型设置信息值
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

        #region 获取有符号、数值、整数类型设置信息值 +static long GetInt64(string key, long def = default(Int64))
        /// <summary>
        /// 获取有符号、数值、整数类型设置信息值
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

        #region 获取无符号、数值、整数类型设置信息值 +static ulong GetUInt64(string key, ulong def = default(UInt64))
        /// <summary>
        /// 获取无符号、数值、整数类型设置信息值
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

        #region 获取时间类型设置信息值 +static DateTime GetDateTime(string key, DateTime def = default(DateTime))
        /// <summary>
        /// 获取时间类型设置信息值
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

        #region 获取Guid类型设置信息值 +static Guid GetGuid(string key, Guid def = default(Guid))
        /// <summary>
        /// 获取Guid类型设置信息值
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
