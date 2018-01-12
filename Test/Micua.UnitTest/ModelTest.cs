using System;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Micua.Domain.Model;

namespace Micua.UnitTest
{
    [TestClass]
    public class ModelTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var modelTypes = GetEntityTypes();

            foreach (var item in modelTypes)
            {
                Console.WriteLine(item.Name);
            }
        }

        private static System.Collections.Generic.IEnumerable<Type> GetEntityTypes()
        {
            var modelAssembly = Assembly.GetAssembly(typeof(MicuaContext));
            var modelTypes = modelAssembly.GetTypes().Where(t => typeof(EntityBase).IsAssignableFrom(t) && !t.IsAbstract);
            return modelTypes;

            
        }
    }

    class MyClass : Infrastructure.Utility.Singleton<MyClass>
    {
        
    }
}
