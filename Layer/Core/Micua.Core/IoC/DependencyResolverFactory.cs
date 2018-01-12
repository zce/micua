namespace Micua.Core.IoC
{
    using System;

    using Micua.Infrastructure.Utility;

    public class DependencyResolverFactory : IDependencyResolverFactory
    {
        private readonly Type _resolverType;

        public DependencyResolverFactory()
            : this(Config.GetString("dependency_resolver_type_name", "Micua.Core.IoC.Unity.UnityDependencyResolver, Micua.Core"))
        {
        }

        public DependencyResolverFactory(string resolverTypeName)
        {
            _resolverType = Type.GetType(resolverTypeName, true, true);
        }

        public IDependencyResolver CreateInstance()
        {
            return Activator.CreateInstance(_resolverType) as IDependencyResolver;
        }
    }
}
