using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Microsoft.Practices.Unity;

namespace Micua.Core.IoC.Unity
{
    /// <summary>
    /// Unity依赖注入容器
    /// </summary>
    public class UnityDependencyResolver : IDependencyResolver
    {

        private readonly IUnityContainer _container;

        /// <summary>
        /// 内部构造方法
        /// </summary>
        [DebuggerStepThrough]
        public UnityDependencyResolver()
        {
            _container = UnityContext.Instance.Container;

            //手动 注册缓存模块
            //UnityContext.Current.Container.RegisterInstance<ICacheStrategy>(new WebCaching());
        }

        [DebuggerStepThrough]
        public void Register<T>(T instance)
        {
            _container.RegisterInstance(instance);
        }

        [DebuggerStepThrough]
        public void Inject<T>(T existing)
        {
            _container.BuildUp(existing);
        }

        [DebuggerStepThrough]
        public T Resolve<T>(Type type)
        {
            return (T)_container.Resolve(type);
        }

        [DebuggerStepThrough]
        public T Resolve<T>(Type type, string name)
        {
            return (T)_container.Resolve(type, name);
        }

        [DebuggerStepThrough]
        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        [DebuggerStepThrough]
        public T Resolve<T>(string name)
        {
            return _container.Resolve<T>(name);
        }

        [DebuggerStepThrough]
        public IEnumerable<T> ResolveAll<T>()
        {
            IEnumerable<T> namedInstances = _container.ResolveAll<T>();
            T unnamedInstance = default(T);

            try
            {
                unnamedInstance = _container.Resolve<T>();
            }
            catch (ResolutionFailedException)
            {
                //When default instance is missing
            }

            return Equals(unnamedInstance, default(T)) 
                ? namedInstances 
                : new ReadOnlyCollection<T>(new List<T>(namedInstances) { unnamedInstance });
        }


        public void Dispose()
        {
            _container.Dispose();
        }
    }
}
