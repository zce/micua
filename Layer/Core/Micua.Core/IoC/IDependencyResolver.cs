using System;
using System.Collections.Generic;

namespace Micua.Core.IoC
{
    /// <summary>
    /// 依赖注入容器接口约束
    /// </summary>
    public interface IDependencyResolver : IDisposable
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        void Register<T>(T instance);

        /// <summary>
        /// 注入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="existing"></param>
        void Inject<T>(T existing);
        /// <summary>
        /// 解析
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="type">类型</param>
        /// <returns>类型实例</returns>
        T Resolve<T>(Type type);
        /// <summary>
        /// 解析
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        T Resolve<T>(Type type, string name);
        /// <summary>
        /// 解析
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T Resolve<T>();
        /// <summary>
        /// 解析
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        T Resolve<T>(string name);
        /// <summary>
        /// 全部解析
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> ResolveAll<T>();
    }

}
