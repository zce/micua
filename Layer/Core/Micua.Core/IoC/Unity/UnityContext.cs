using System.Configuration;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Micua.Infrastructure.Utility;
namespace Micua.Core.IoC.Unity
{
    /// <summary>
    /// Unity上下文
    /// </summary>
    public sealed class UnityContext : Singleton<UnityContext>
    {
        //配置UnityContainer
        private readonly UnityConfigurationSection _section = ConfigurationManager.GetSection(UnityConfigurationSection.SectionName) as UnityConfigurationSection;
        private UnityContext() { }

        private UnityContainer _container;
        /// <summary>
        /// Unity的IoC容器对象 
        /// </summary>
        public IUnityContainer Container
        {
            get
            {
                if (_container != null) return _container;
                _container = new UnityContainer();
                if (null == _section)
                {
                    throw new ConfigurationErrorsException("The <unity> configuration section does not exist.");
                }
                foreach (var item in _section.Containers)
                {
                    _section.Configure(_container, item.Name);
                }

                return _container;
            }
        }


    }
}
