// ***********************************************************************
// Project			: Micua.Infrastructure
// Assembly         : Micua.Core.Caching
// Author           : iceStone
// Created          : 2014年01月10日 09:43
//
// Last Modified By : iceStone
// Last Modified On : 2014年01月10日 10:08
// ***********************************************************************
// <copyright file="CachingCallHandler.cs" company="Wedn.Net">
//     Copyright © 2014 Wedn.Net. All Rights Reserved.
// </copyright>
// <summary>缓存调用处理程序</summary>
// ***********************************************************************

namespace Micua.Core.Caching.Interception
{
    using System;
    using System.Reflection;
    using System.Text;

    using Microsoft.Practices.Unity.InterceptionExtension;

    /// <summary>
    /// 缓存调用处理程序
    /// </summary>
    /// <remarks>
    ///  2014年01月10日 10:08 Created By iceStone
    /// </remarks>
    public class CachingCallHandler : ICallHandler
    {
        /// <summary>
        /// Order in which the handler will be executed
        /// </summary>
        /// <value>The order.</value>
        public int Order { get; set; }
        /// <summary>
        /// 持续时间（秒）
        /// </summary>
        /// <value>The duration.</value>
        public int Duration { get; set; }
        /// <summary>
        /// 默认持续时间（秒）
        /// </summary>
        /// <value>The default duration.</value>
        internal static int DefaultDuration { get; private set; }
        /// <summary>
        /// 缓存键生成委托
        /// </summary>
        /// <value>The cache key generator.</value>
        internal static Func<MethodBase, object[], string> CacheKeyGenerator { get; private set; }

        /// <summary>
        /// Initializes static members of the <see cref="CachingCallHandler"/> class.
        /// </summary>
        /// <remarks>
        ///  2014年01月10日 10:08 Created By iceStone
        /// </remarks>
        static CachingCallHandler()
        {
            DefaultDuration = 300;
            Guid prefix = Guid.NewGuid();
            //缓存项Key的格式：{GUID}：{方法返回声明类型名称}{方法名称}：{输入参数HashCode}
            CacheKeyGenerator = (method, inputs) =>
            {
                var sb = new StringBuilder();
                sb.AppendFormat("{0}:", prefix);
                sb.AppendFormat("{0}:", method.DeclaringType != null ? method.DeclaringType.FullName : string.Empty);
                sb.AppendFormat("{0}:", method.Name);
                if (null != inputs)
                {
                    Array.ForEach(inputs, input =>
                    {
                        string hashCode = (null == input) ? string.Empty : input.GetHashCode().ToString();
                        sb.AppendFormat("{0}:", hashCode);
                    });
                }
                return sb.ToString(); //.TrimEnd(':');
            };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CachingCallHandler"/> class.
        /// </summary>
        /// <remarks>
        ///  2014年01月10日 10:08 Created By iceStone
        /// </remarks>
        /// <param name="duration">The duration.</param>
        public CachingCallHandler(int? duration = null)
        {
            Duration = duration.HasValue ? duration.Value : DefaultDuration;
        }
        /// <summary>
        /// 方法执行之前调用
        /// </summary>
        /// <remarks>
        ///  2014年01月10日 10:08 Created By iceStone
        /// </remarks>
        /// <param name="input">输入</param>
        /// <param name="getNext">执行体</param>
        /// <returns>方法返回结果</returns>
        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            var targetMethod = (MethodInfo)input.MethodBase;
            if (targetMethod.ReturnType == typeof(void))
            {
                //如果没有返回值，则直接执行方法
                return getNext()(input, getNext);
            }
            var inputs = new object[input.Inputs.Count];
            input.Inputs.CopyTo(inputs, 0);
            string cacheKey = CacheKeyGenerator(targetMethod, inputs);
            var cachedResult = CacheHelper.Get<object[]>(cacheKey);//HttpRuntime.Cache.Get(cacheKey) as object[];
            if (null == cachedResult)
            {
                IMethodReturn realReturn = getNext()(input, getNext);
                if (null == realReturn.Exception)
                {
                    CacheHelper.Set(cacheKey, new[] { realReturn.ReturnValue }, DateTime.Now.AddSeconds(Duration));
                    //HttpRuntime.Cache.Insert(cacheKey, new object[] { realReturn.ReturnValue }, null, DateTime.Now.Add(this.ExpirationTime), System.Web.Caching.Cache.NoSlidingExpiration);
                }
                return realReturn;
            }
            return input.CreateMethodReturn(cachedResult[0], new object[] { input.Arguments });
        }
    }
}