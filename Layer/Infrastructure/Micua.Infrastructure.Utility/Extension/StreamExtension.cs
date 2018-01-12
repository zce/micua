// ***********************************************************************
// Project          : Micua
// Assembly         : Micua.Infrastructure.Utility
// Author           : iceStone
// Created          : 2014-01-05 1:49 PM
// 
// Last Modified By : iceStone
// Last Modified On : 2014-01-05 1:49 PM
// ***********************************************************************
// <copyright file="StreamExtensions.cs" company="Wedn.Net">
//     Copyright © 2014 Wedn.Net. All Rights Reserved.
// </copyright>
// <summary>文件流拓展方法</summary>
// ***********************************************************************

namespace System
{
    using System.IO;
    using System.Security.Cryptography;

    /// <summary>
    /// 文件流拓展方法
    /// </summary>
    public static class StreamExtension
    {
        #region 获取流的MD5值 +static string MD5(this Stream stream)

        /// <summary>
        /// 获取流的MD5值
        /// </summary>
        /// <remarks>
        ///  2013-11-18 18:53 Created By iceStone
        /// </remarks>
        /// <param name="stream">流</param>
        /// <returns>MD5值</returns>
        public static string GetMD5(this Stream stream)
        {
            var oMd5Hasher = new MD5CryptoServiceProvider();
            var arrbytHashValue = oMd5Hasher.ComputeHash(stream);

            // 由以连字符分隔的十六进制对构成的String，其中每一对表示value 中对应的元素；例如“F-2C-4A”
            string strHashData = BitConverter.ToString(arrbytHashValue);

            // 替换-
            return strHashData.Replace("-", string.Empty).ToLower();
        }
        #endregion
    }
}