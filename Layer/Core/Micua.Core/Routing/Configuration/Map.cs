using System.Configuration;

namespace Micua.Core.Routing.Configuration
{
    /// <summary>
    /// 一个Route规则配置项
    /// </summary>
    public class Map : ConfigurationElement
    {
        /// <summary>
        /// Route的名称
        /// </summary>
        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        public string Name
        {
            get { return this["name"].ToString(); }
            set { this["name"] = value; }
        }

        /// <summary>
        /// Route的Url
        /// </summary>
        [ConfigurationProperty("url", IsRequired = true, IsKey = true)]
        public string Url
        {
            get { return this["url"].ToString(); }
            set { this["url"] = value; }
        }

        /// <summary>
        /// Route的默认Domain
        /// </summary>
        [ConfigurationProperty("domain", IsRequired = false)]
        public string Domain
        {
            get { return this["domain"].ToString(); }
            set { this["domain"] = value; }
        }

        /// <summary>
        /// Route的默认Controller
        /// </summary>
        [ConfigurationProperty("controller", IsRequired = false)]
        public string Controller
        {
            get { return this["controller"].ToString(); }
            set { this["controller"] = value; }
        }

        /// <summary>
        /// Route的默认Action
        /// </summary>
        [ConfigurationProperty("action", IsRequired = false)]
        public string Action
        {
            get { return this["action"].ToString(); }
            set { this["action"] = value; }
        }

        /// <summary>
        /// Route的参数默认值列表
        /// </summary>
        [ConfigurationProperty("parameters", IsRequired = false)]
        public ParameterCollection Paramaters
        {
            get { return this["parameters"] as ParameterCollection; }
            set { this["parameters"] = value; }
        }

        /// <summary>
        /// Route约束
        /// </summary>
        [ConfigurationProperty("namespaces", IsRequired = false)]
        public NamespaceCollection Namespaces
        {
            get { return this["namespaces"] as NamespaceCollection; }
            set { this["namespaces"] = value; }
        }
    }
}
