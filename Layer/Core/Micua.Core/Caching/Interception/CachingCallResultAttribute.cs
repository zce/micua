// ***********************************************************************
// Project			: Micua.Infrastructure
// Assembly         : Micua.Core.Caching
// Author           : iceStone
// Created          : 2014年01月10日 09:43
//
// Last Modified By : iceStone
// Last Modified On : 2014年01月10日 10:07
// ***********************************************************************
// <copyright file="CachingCallHandlerAttribute.cs" company="Wedn.Net">
//     Copyright © 2014 Wedn.Net. All Rights Reserved.
// </copyright>
// <summary>将该方法执行结果缓存指定时间</summary>
// ***********************************************************************

namespace Micua.Core.Caching.Interception
{
    using System;

    using Microsoft.Practices.Unity;
    using Microsoft.Practices.Unity.InterceptionExtension;

    /// <summary>
    /// 将该方法执行结果缓存指定时间
    /// </summary>
    /// <remarks>
    ///  2014年01月10日 10:09 Created By iceStone
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Method)]
    public class CachingCallResultAttribute : HandlerAttribute
    {
        /// <summary>
        /// Gets the duration.
        /// </summary>
        /// <value>The duration.</value>
        public int Duration { get; private set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="CachingCallResultAttribute"/> class.
        /// </summary>
        /// <remarks>
        ///  2014年01月10日 10:09 Created By iceStone
        /// </remarks>
        /// <param name="duration">The duration.</param>
        public CachingCallResultAttribute(int duration = 300)
        {
            Duration = duration;
            //if (!string.IsNullOrEmpty(expirationTime))
            //{
            //    TimeSpan expirationTimeSpan;
            //    if (!TimeSpan.TryParse(expirationTime, out expirationTimeSpan))
            //    {
            //        throw new ArgumentException("输入的过期时间（TimeSpan）不合法",
            //            "expirationTime");
            //    }
            //    this.ExpirationTime = expirationTimeSpan;
            //}
        }

        /// <summary>
        /// Derived classes implement this method. When called, it
        /// creates a new call handler as specified in the attribute
        /// configuration.
        /// </summary>
        /// <remarks>
        ///  2014年01月10日 10:09 Created By iceStone
        /// </remarks>
        /// <param name="container">The <see cref="T:Microsoft.Practices.Unity.IUnityContainer" /> to use when creating handlers,
        /// if necessary.</param>
        /// <returns>A new call handler object.</returns>
        public override ICallHandler CreateHandler(IUnityContainer container)
        {
            return new CachingCallHandler(Duration) { Order = Order };
        }
    }
}