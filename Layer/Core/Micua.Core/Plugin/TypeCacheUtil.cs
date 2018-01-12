namespace Micua.Core.Plugin
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    internal static class TypeCacheUtil
    {
        private static IEnumerable<Type> FilterTypesInAssemblies(IBuildManager buildManager, Predicate<Type> predicate)
        {
            IEnumerable<Type> emptyTypes = Type.EmptyTypes;
            foreach (Assembly assembly in buildManager.GetReferencedAssemblies())
            {
                Type[] types;
                try
                {
                    types = assembly.GetTypes();
                }
                catch (ReflectionTypeLoadException exception)
                {
                    types = exception.Types;
                }
                emptyTypes = emptyTypes.Concat<Type>(types);
            }
            return (from type in emptyTypes
                    where TypeIsPublicClass(type) && predicate(type)
                    select type);
        }

        public static List<Type> GetFilteredTypesFromAssemblies(string cacheName, Predicate<Type> predicate, IBuildManager buildManager)
        {
            TypeCacheSerializer serializer = new TypeCacheSerializer();
            List<Type> matchingTypes = ReadTypesFromCache(cacheName, predicate, buildManager, serializer);
            if (matchingTypes == null)
            {
                matchingTypes = FilterTypesInAssemblies(buildManager, predicate).ToList<Type>();
                SaveTypesToCache(cacheName, matchingTypes, buildManager, serializer);
            }
            return matchingTypes;
        }

        internal static List<Type> ReadTypesFromCache(string cacheName, Predicate<Type> predicate, IBuildManager buildManager, TypeCacheSerializer serializer)
        {
            Func<Type, bool> func = null;
            try
            {
                Stream stream = buildManager.ReadCachedFile(cacheName);
                if (stream != null)
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        List<Type> source = serializer.DeserializeTypes(reader);
                        if (source != null)
                        {
                            if (func == null)
                            {
                                func = type => TypeIsPublicClass(type) && predicate(type);
                            }
                            if (source.All<Type>(func))
                            {
                                return source;
                            }
                        }
                    }
                }
            }
            catch
            {
            }
            return null;
        }

        internal static void SaveTypesToCache(string cacheName, IList<Type> matchingTypes, IBuildManager buildManager, TypeCacheSerializer serializer)
        {
            try
            {
                Stream stream = buildManager.CreateCachedFile(cacheName);
                if (stream != null)
                {
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        serializer.SerializeTypes(matchingTypes, writer);
                    }
                }
            }
            catch
            {
            }
        }

        private static bool TypeIsPublicClass(Type type)
        {
            return ((((type != null) && type.IsPublic) && type.IsClass) && !type.IsAbstract);
        }
    }
}
