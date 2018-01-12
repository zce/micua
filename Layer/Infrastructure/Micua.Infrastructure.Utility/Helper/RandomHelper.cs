// ***********************************************************************
// Project          : Micua
// Assembly         : Micua.Infrastructure.Utility
// Author           : Administrator
// Created          : 2014-01-11 11:32 PM
// 
// Last Modified By : Administrator
// Last Modified On : 2014-01-11 11:32 PM
// ***********************************************************************
// <copyright file="RandomHelper.cs" company="Wedn.Net">
//     Copyright © 2014 Wedn.Net. All Rights Reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Micua.Infrastructure.Utility
{
    using System;
    using System.Text;

    /// <summary>
    /// 随机对象操作类
    /// </summary>
    public static class RandomHelper
    {
        //随机数对象
        private static readonly Random Random;
        const string Numbers = "1234567890";
        const string Chars = "1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        static RandomHelper()
        {
            //为随机数对象赋值
            Random = new Random();
        }
        #endregion

        #region 获取指定长度的随机数字 +static long GetNumber(int length)
        /// <summary>
        /// 获取指定长度的随机数字
        /// </summary>
        /// <param name="length">长度</param>
        /// <returns>随机数字</returns>
        public static long GetNumber(int length)
        {
            var sbRes = new StringBuilder(length);
            for (var i = 0; i < length; i++)
            {
                sbRes.Append(Numbers[Random.Next(62)]);
            }
            return sbRes.ToString().ToInt64();
        } 
        #endregion

        #region 生成一个指定范围的随机整数 +static int GetNumber(int minNum, int maxNum)
        /// <summary>
        /// 生成一个指定范围的随机整数，该随机数范围包括最小值，但不包括最大值
        /// </summary>
        /// <param name="minNum">最小值</param>
        /// <param name="maxNum">最大值</param>
        public static int GetNumber(int minNum, int maxNum)
        {
            return Random.Next(minNum, maxNum);
        }
        #endregion

        #region 获取指定长度的随机数字 +static string GetString(int length)
        /// <summary>
        /// 获取指定长度的随机数字
        /// </summary>
        /// <param name="length">长度</param>
        /// <returns>随机数字</returns>
        public static string GetString(int length)
        {
            var sbRes = new StringBuilder(length);
            for (var i = 0; i < length; i++)
            {
                sbRes.Append(Chars[Random.Next(55)]);
            }
            return sbRes.ToString();
        }
        #endregion

        //#region 生成一个0.0到1.0的随机小数
        ///// <summary>
        ///// 生成一个0.0到1.0的随机小数
        ///// </summary>
        //public static double GetRandomDouble()
        //{
        //    return Random.NextDouble();
        //}
        //#endregion
    }
}