// ***********************************************************************
// Project			: Micua.Infrastructure
// Assembly         : Micua.Core.Tracing
// Author           : iceStone
// Created          : 2014年01月09日 18:42
//
// Last Modified By : iceStone
// Last Modified On : 2014年01月10日 10:49
// ***********************************************************************
// <copyright file="TracingCallHandler.cs" company="Wedn.Net">
//     Copyright © 2014 Wedn.Net. All Rights Reserved.
// </copyright>
// <summary>跟踪调用处理程序</summary>
// ***********************************************************************

namespace Micua.Core.Tracing.Interception
{
    using Microsoft.Practices.Unity.InterceptionExtension;

    using Micua.Infrastructure.Utility;

    /// <summary>
    /// 跟踪调用处理程序
    /// </summary>
    /// <remarks>
    ///  2014年01月10日 10:49 Created By iceStone
    /// </remarks>
    public class TracingCallHandler : ICallHandler
    {
        /// <summary>
        /// Order in which the handler will be executed
        /// </summary>
        /// <value>The order.</value>
        public int Order { get; set; }
        /// <summary>
        /// Implement this method to execute your handler processing.
        /// </summary>
        /// <remarks>
        ///  2014年01月10日 10:49 Created By iceStone
        /// </remarks>
        /// <param name="input">Inputs to the current call to the target.</param>
        /// <param name="getNext">Delegate to execute to get the next delegate in the handler
        /// chain.</param>
        /// <returns>Return value from the target.</returns>
        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            //if (input == null) throw new ArgumentNullException("input");
            //if (getNext == null) throw new ArgumentNullException("getNext");
            GuardHelper.ArgumentNotNull(input, "input");
            GuardHelper.ArgumentNotNull(getNext, "getNext");
            //开始执行
            var result = getNext()(input, getNext);
            //执行结果没有异常，直接返回执行结果
            if (result.Exception == null) return result;
            //否则记录日志信息
            LogHelper.WriteErrorLog("TracingCallHandler", result.Exception.Message, result.Exception);
            //处理异常
            result.Exception = null;
            return result;
        }

    }
}