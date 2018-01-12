// ***********************************************************************
// Project			: Micua.Infrastructure
// Assembly         : Micua.Core.Caching
// Author           : iceStone
// Created          : 2013-11-23 22:03
//
// Last Modified By : iceStone
// Last Modified On : 2013-11-23 22:03
// ***********************************************************************
// <copyright file="ICacheProvider.cs" company="Wedn.Net">
//     Copyright © 2014 Wedn.Net. All Rights Reserved.
// </copyright>
// <summary>缓存供给者约束接口</summary>
// ***********************************************************************

namespace Micua.Core.Caching
{
    using System;

    /// <summary>
    /// 缓存供给者约束接口
    /// </summary>
    public interface ICacheProvider
    {
        #region 获取缓存数据 +object Get(string key)
        /// <summary>
        /// 获取缓存数据
        /// </summary>
        /// <remarks>
        ///  2013-11-23 22:03 Created By iceStone
        /// </remarks>
        /// <param name="key">键</param>
        /// <returns>System.Object.</returns>
        object Get(string key); 
        #endregion

        #region 获取缓存数据 +T Get<T>(string key) where T : class
        /// <summary>
        /// 获取缓存数据
        /// </summary>
        /// <remarks>
        ///  2013-11-23 22:03 Created By iceStone
        /// </remarks>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="key">键</param>
        /// <returns>指定数据类型的对象</returns>
        T Get<T>(string key) where T : class; 
        #endregion

        #region 设置数据缓存 +void Set(string key, object value, DateTime absoluteExpiration)
        /// <summary>
        /// 设置数据缓存
        /// </summary>
        /// <remarks>
        ///  2013-11-23 22:03 Created By iceStone
        /// </remarks>
        /// <param name="key">键</param>
        /// <param name="value">缓存对象</param>
        /// <param name="absoluteExpiration">绝对过期时间</param>
        void Set(string key, object value, DateTime absoluteExpiration); 
        #endregion

        #region 设置数据缓存 +void Set(string key, object value, TimeSpan slidingExpiration)
        /// <summary>
        /// 设置数据缓存
        /// </summary>
        /// <remarks>
        ///  2013-11-23 22:03 Created By iceStone
        /// </remarks>
        /// <param name="key">键</param>
        /// <param name="value">缓存对象</param>
        /// <param name="slidingExpiration">最后一次访问所插入对象时与该对象到期时之间的时间间隔</param>
        void Set(string key, object value, TimeSpan slidingExpiration); 
        #endregion

        #region 移除指定键的缓存数据 +void Remove(string key)
        /// <summary>
        /// 移除指定键的缓存数据
        /// </summary>
        /// <remarks>
        ///  2013-11-23 22:03 Created By iceStone
        /// </remarks>
        /// <param name="key">键</param>
        void Remove(string key); 
        #endregion

        #region 清除全部缓存数据 +void Clear()
        /// <summary>
        /// 清除全部缓存数据
        /// </summary>
        /// <remarks>
        ///  2013-11-23 22:03 Created By iceStone
        /// </remarks>
        void Clear(); 
        #endregion

        #region 清除通配键的缓存数据 +void Clear(string pattern)
        /// <summary>
        /// 清除通配键的缓存数据
        /// </summary>
        /// <remarks>
        ///  2013-11-23 22:03 Created By iceStone
        /// </remarks>
        /// <param name="pattern">键</param>
        void Clear(string pattern); 
        #endregion
    }
}
