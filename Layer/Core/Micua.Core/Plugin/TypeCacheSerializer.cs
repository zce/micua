namespace Micua.Core.Plugin
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Xml;

    internal sealed class TypeCacheSerializer
    {
        private static readonly Guid _mvcVersionId = typeof(TypeCacheSerializer).Module.ModuleVersionId;

        public List<Type> DeserializeTypes(TextReader input)
        {
            XmlDocument document = new XmlDocument();
            document.Load(input);
            XmlElement documentElement = document.DocumentElement;
            Guid guid = new Guid(documentElement.Attributes["mvcVersionId"].Value);
            if (guid != _mvcVersionId)
            {
                return null;
            }
            List<Type> list = new List<Type>();
            foreach (XmlNode node in documentElement.ChildNodes)
            {
                Assembly assembly = Assembly.Load(node.Attributes["name"].Value);
                foreach (XmlNode node2 in node.ChildNodes)
                {
                    Guid guid2 = new Guid(node2.Attributes["versionId"].Value);
                    foreach (XmlNode node3 in node2.ChildNodes)
                    {
                        string innerText = node3.InnerText;
                        Type item = assembly.GetType(innerText);
                        if ((item == null) || (item.Module.ModuleVersionId != guid2))
                        {
                            return null;
                        }
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public void SerializeTypes(IEnumerable<Type> types, TextWriter output)
        {
            IEnumerable<IGrouping<Assembly, IGrouping<Module, Type>>> enumerable = from type in types
                                                                                   group type by type.Module into groupedByModule
                                                                                   group groupedByModule by groupedByModule.Key.Assembly;
            XmlDocument document = new XmlDocument();
            document.AppendChild(document.CreateComment("此文件自动生成。请不要修改此文件的内容。"));
            XmlElement newChild = document.CreateElement("typeCache");
            document.AppendChild(newChild);
            newChild.SetAttribute("lastModified", this.CurrentDate.ToString());
            newChild.SetAttribute("mvcVersionId", _mvcVersionId.ToString());
            foreach (IGrouping<Assembly, IGrouping<Module, Type>> grouping in enumerable)
            {
                XmlElement element2 = document.CreateElement("assembly");
                newChild.AppendChild(element2);
                element2.SetAttribute("name", grouping.Key.FullName);
                foreach (IGrouping<Module, Type> grouping2 in grouping)
                {
                    XmlElement element3 = document.CreateElement("module");
                    element2.AppendChild(element3);
                    element3.SetAttribute("versionId", grouping2.Key.ModuleVersionId.ToString());
                    foreach (Type type in grouping2)
                    {
                        XmlElement element4 = document.CreateElement("type");
                        element3.AppendChild(element4);
                        element4.AppendChild(document.CreateTextNode(type.FullName));
                    }
                }
            }
            document.Save(output);
        }

        private DateTime CurrentDate
        {
            get
            {
                DateTime? currentDateOverride = this.CurrentDateOverride;
                if (!currentDateOverride.HasValue)
                {
                    return DateTime.Now;
                }
                return currentDateOverride.GetValueOrDefault();
            }
        }

        internal DateTime? CurrentDateOverride { get; set; }
    }
}
