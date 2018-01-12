namespace Micua.Core.IoC
{
    /// <summary>
    /// 依赖注入容器工厂
    /// </summary>
    public interface IDependencyResolverFactory
    {
        /// <summary>
        /// 创建实例
        /// </summary>
        /// <returns>实例</returns>
        IDependencyResolver CreateInstance();
    }
}
