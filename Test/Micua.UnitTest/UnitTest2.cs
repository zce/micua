using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Micua.UnitTest
{
    [TestClass]
    public class UnitTest2
    {
        Micua.Domain.Model.MicuaContext context = new Domain.Model.MicuaContext();
        [TestMethod]
        public void TestMethod1()
        {
            #region Linq
            var temp = from b in context.Blogs
                       join p in context.Posts
                       on b.Id equals p.BlogId
                       select p;
            #endregion

            var temp2 = context.Posts.Join(
                context.Blogs, 
                p => p.BlogId, 
                b => b.Id,
                (p, b) => p);


            foreach (var item in temp2)
            {
                Console.WriteLine(item.Title);
            }
        }
    }
}
