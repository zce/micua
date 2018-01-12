// ***********************************************************************
// Project			: Micua.Infrastructure
// Assembly         : Micua.Session
// Author           : iceStone
// Created          : 2014年01月09日 10:12
//
// Last Modified By : iceStone
// Last Modified On : 2014年01月09日 10:13
// ***********************************************************************
// <copyright file="ISessionProvider.cs" company="Wedn.Net">
//     Copyright © 2014 Wedn.Net. All Rights Reserved.
// </copyright>
// <summary>Session提供程序接口约束</summary>
// ***********************************************************************

namespace Micua.Core.Session
{
    /// <summary>
    /// Session提供程序接口约束
    /// </summary>
    /// <remarks>
    ///  2014年01月09日 10:13 Created By iceStone
    /// </remarks>
    public interface ISessionProvider
    {
        #region 获取会话唯一标识符 +string SessionId
        /// <summary>
        /// 获取会话唯一标识符
        /// </summary>
        string SessionId { get; }
        #endregion

        #region 获取指定键的Session对象 +object Get(string key)
        /// <summary>
        /// 获取指定键的Session对象
        /// </summary>
        /// <remarks>
        ///  2013-11-23 22:04 Created By iceStone
        /// </remarks>
        /// <param name="key">Session键</param>
        /// <returns>Session对象</returns>
        object Get(string key);
        #endregion

        #region 获取指定类型的指定键的Session对象 +TObj Get<TObj>(string key)
        /// <summary>
        /// 获取指定类型的指定键的Session对象
        /// </summary>
        /// <remarks>
        ///  2013-11-23 22:04 Created By iceStone
        /// </remarks>
        /// <typeparam name="TObj">Session数据类型</typeparam>
        /// <param name="key">Session键</param>
        /// <returns>Session对象</returns>
        TObj Get<TObj>(string key) where TObj : class;
        #endregion

        #region 设置一个Session +void Set(string key, object value)
        /// <summary>
        /// 设置一个Session
        /// </summary>
        /// <remarks>
        ///  2013-11-23 22:04 Created By iceStone
        /// </remarks>
        /// <param name="key">Session键</param>
        /// <param name="value">Session值</param>
        void Set(string key, object value);
        #endregion

        #region 删除一个指定的Session +void Remove(string key)
        /// <summary>
        /// 删除一个指定的Session
        /// </summary>
        /// <remarks>
        ///  2013-11-23 22:04 Created By iceStone
        /// </remarks>
        /// <param name="key">Session键</param>
        void Remove(string key);
        #endregion

        #region 清空Session +void Clear()
        /// <summary>
        /// 清空Session
        /// </summary>
        /// <remarks>
        ///  2013-11-23 22:04 Created By iceStone
        /// </remarks>
        void Clear(); 
        #endregion

        #region 删除匹配键的Session +void Clear(string pattern)
        /// <summary>
        /// 删除匹配键的Session
        /// </summary>
        /// <remarks>
        ///  2013-11-23 22:04 Created By iceStone
        /// </remarks>
        /// <param name="pattern">匹配键</param>
        void Clear(string pattern);
        #endregion

        #region 取消当前会话 +void Abandon()
        /// <summary>
        /// 取消当前会话
        /// </summary>
        /// <remarks>
        ///  2013-11-23 22:04 Created By iceStone
        /// </remarks>
        void Abandon();
        #endregion
    }
}