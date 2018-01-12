using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Micua.Core.IoC
{
    /// <summary>
    /// IoC操作类
    /// 使用该类，可以使程序支持多种IoC框架
    /// </summary>
    public static class IoCHelper
    {
        static IoCHelper()
        {
            if (_resolver == null)
                //初始化IoC
                InitializeWith(new DependencyResolverFactory());
        }
        private static IDependencyResolver _resolver;

        [DebuggerStepThrough]
        public static void InitializeWith(IDependencyResolverFactory factory)
        {
            if (_resolver == null)
                _resolver = factory.CreateInstance();
        }

        [DebuggerStepThrough]
        public static void Register<T>(T instance)
        {

            _resolver.Register(instance);
        }

        [DebuggerStepThrough]
        public static void Inject<T>(T existing)
        {

            _resolver.Inject(existing);
        }

        [DebuggerStepThrough]
        public static T Resolve<T>(Type type)
        {

            return _resolver.Resolve<T>(type);
        }

        [DebuggerStepThrough]
        public static T Resolve<T>(Type type, string name)
        {

            return _resolver.Resolve<T>(type, name);
        }

        [DebuggerStepThrough]
        public static T Resolve<T>()
        {
            return _resolver.Resolve<T>();
        }

        [DebuggerStepThrough]
        public static T Resolve<T>(string name)
        {

            return _resolver.Resolve<T>(name);
        }

        [DebuggerStepThrough]
        public static IEnumerable<T> ResolveAll<T>()
        {
            return _resolver.ResolveAll<T>();
        }

        [DebuggerStepThrough]
        public static void Reset()
        {
            if (_resolver != null)
            {
                _resolver.Dispose();
            }
        }
    }

}
