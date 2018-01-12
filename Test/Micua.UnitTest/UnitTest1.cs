using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Micua.Domain.Model;

namespace Micua.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        const string sql = @"INSERT INTO [dbo].[post] VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', 0, 0, 0, 0, 10, 10, 0, 1, 1, NULL);";
        [TestMethod]
        public void TestMethod1()
        {
            var doc = new XmlDocument();
            var settings = new XmlReaderSettings { IgnoreComments = true, IgnoreWhitespace = true };
            using (var reader = XmlReader.Create("d://wedn.xml", settings))
            {
                doc.Load(reader);
            }
            var nodes = doc.DocumentElement.FirstChild.ChildNodes;
            //StringBuilder sbd = new StringBuilder();
            IList<string> sqls = new List<string>();
            foreach (XmlNode node in nodes)
            {
                if (node.Name.Equals("item", StringComparison.InvariantCultureIgnoreCase))
                {
                    var postInfo = node.ChildNodes;
                    sqls.Add(string.Format(sql,
                        postInfo[1].InnerText.Replace("http://blog.wedn.net/post/", string.Empty).Replace(".html", string.Empty),
                        postInfo[3].InnerText,
                        postInfo[0].InnerText,
                        postInfo[2].InnerText.ToDateTime().ToString(),
                        postInfo[2].InnerText.ToDateTime().ToString()   ,
                        postInfo[6].InnerText.Replace("\r", string.Empty).Replace("\n", string.Empty).Replace("'", "''"),
                        postInfo[7].InnerText.Replace("\r", string.Empty).Replace("\n", string.Empty).Replace("'", "''")
                        ));
                }
            }
            File.AppendAllLines("d://sql.sql", sqls);
        }
    }
}
