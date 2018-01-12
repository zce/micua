using System.Web.Mvc;

namespace Micua.Core.IoC
{
    /// <summary>
    /// 自定义的控制器实例化工厂
    /// </summary>
    public class ResolverControllerFactory
    {
        /// <summary>
        /// 获取控制器工厂
        /// </summary>
        /// <returns></returns>
        public IControllerFactory GetControllerFactory()
        {
            return IoCHelper.Resolve<IControllerFactory>();
        }
    }
}
