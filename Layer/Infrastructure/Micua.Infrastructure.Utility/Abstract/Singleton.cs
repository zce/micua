// +----------------------------------------------------------------------
// | Micua [ DO IT BATTER ! ]
// +----------------------------------------------------------------------
// | Copyright © 2014 Wedn.Net. All Rights Reserved.
// +----------------------------------------------------------------------
// | Summary : 延迟加载单例模版
// +----------------------------------------------------------------------
// | Author  : iceStone <ice@wedn.net>
// +----------------------------------------------------------------------

namespace Micua.Infrastructure.Utility
{
    using System;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Singleton Class Template
    /// 延迟加载单例模版
    /// </summary>
    /// <remarks>
    ///  2013-12-07 23:56 Created By iceStone
    /// </remarks>
    /// <typeparam name="T">单例类型</typeparam>
    public abstract class Singleton<T>
    {
        /// <summary>
        /// The _instance
        /// </summary>
        private static readonly Lazy<T> _instance = new Lazy<T>(() =>
        {
            var ctors = typeof(T).GetConstructors(
                BindingFlags.Instance
                | BindingFlags.NonPublic
                | BindingFlags.Public);
            if (ctors.Count() != 1)
                throw new InvalidOperationException(String.Format("Type {0} must have exactly one constructor.", typeof(T)));
            var ctor = ctors.SingleOrDefault(c => !c.GetParameters().Any() && c.IsPrivate);
            if (ctor == null)
                throw new InvalidOperationException(String.Format("The constructor for {0} must be private and take no parameters.", typeof(T)));
            return (T)ctor.Invoke(null);
        });

        /// <summary>
        /// 获取单例实体对象
        /// </summary>
        /// <value>实体对象.</value>
        public static T Instance
        {
            get { return _instance.Value; }
        }
    }
}