// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ILocalizationProvider.cs" company="Wedn.Net">
//     Copyright © 2014 Wedn.Net. All Rights Reserved.
// </copyright>
// <summary>
//   The LocalizationProvider interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Micua.Core.Localization
{
    /// <summary>
    /// The LocalizationProvider interface.
    /// </summary>
    public interface ILocalizationProvider
    {
        /// <summary>
        /// 转换字符串
        /// </summary>
        /// <param name="input">输入</param>
        /// <returns>输出</returns>
        string Parse(string input);

        /// <summary>
        /// 转换格式化字符串
        /// </summary>
        /// <param name="input">输入</param>
        /// <param name="args">一个对象数组，其中包含零个或多个要设置格式的对象</param>
        /// <returns>输出</returns>
        string Parse(string input,params object[] args);

    }
}
