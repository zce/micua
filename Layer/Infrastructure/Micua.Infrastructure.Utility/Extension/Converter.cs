// ***********************************************************************
// Project          : Micua
// Assembly         : Micua.Infrastructure.Utility
// Author           : Administrator
// Created          : 2014-01-05 11:27 AM
// 
// Last Modified By : Administrator
// Last Modified On : 2014-01-05 11:27 AM
// ***********************************************************************
// <copyright file="Convert.cs" company="Wedn.Net">
//     Copyright © 2014 Wedn.Net. All Rights Reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace System
{
    using System.Text.RegularExpressions;

    /// <summary>
    /// 转换拓展方法
    /// </summary>
    public static partial class Converter
    {
        #region Convert DateTime type to other types

        private const string DateFormat = "yyyy-MM-dd HH:ss";
        /// <summary>
        /// 时间转换成字符串
        /// </summary>
        /// <remarks>
        ///  2014-01-05 13:32:34 Created By iceStone
        /// </remarks>
        /// <param name="dateTime">时间</param>
        /// <returns>字符串</returns>
        public static string ToString(this DateTime dateTime)
        {
            return dateTime.ToString(DateFormat);
        }

        /// <summary>
        /// 将DateTime时间换成中文
        /// </summary>
        /// <remarks>
        ///  2013-11-18 18:53 Created By iceStone
        /// </remarks>
        /// <param name="dateTime">时间</param>
        /// <returns>System.String.</returns>
        /// <example>
        /// 2012-12-21 12:12:21.012 → 1月前
        /// 2011-12-21 12:12:21.012 → 1年前
        /// </example>
        public static string ToChsStr(this DateTime dateTime)
        {
            var ts = DateTime.Now - dateTime;
            if ((int)ts.TotalDays >= 365)
                return (int)ts.TotalDays / 365 + "年前";
            if ((int)ts.TotalDays >= 30 && ts.TotalDays <= 365)
                return (int)ts.TotalDays / 30 + "月前";
            if ((int)ts.TotalDays == 1)
                return "昨天";
            if ((int)ts.TotalDays == 2)
                return "前天";
            if ((int)ts.TotalDays >= 3 && ts.TotalDays <= 30)
                return (int)ts.TotalDays + "天前";
            if ((int)ts.TotalDays != 0) return dateTime.ToString("yyyy年MM月dd日");
            if ((int)ts.TotalHours != 0)
                return (int)ts.TotalHours + "小时前";
            if ((int)ts.TotalMinutes == 0)
                return "刚刚";
            return (int)ts.TotalMinutes + "分钟前";
        }

        #endregion
    }
}