using System.Configuration;

namespace Micua.Core.Routing.Configuration
{
    /// <summary>
    /// 参数配置元素
    /// </summary>
    public class Parameter : ConfigurationElement
    {
        /// <summary>
        /// 参数名
        /// </summary>
        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        public string Name
        {
            get { return this["name"].ToString(); }
            set { this["name"] = value; }
        }

        /// <summary>
        /// 参数默认值
        /// </summary>
        [ConfigurationProperty("value", IsRequired = true)]
        public string Value
        {
            get { return this["value"].ToString(); }
            set { this["value"] = value; }
        }

        /// <summary>
        /// 对参数的约束，支持正则
        /// </summary>
        [ConfigurationProperty("constraint", IsRequired = false)]
        public string Constraint
        {
            get { return this["constraint"].ToString(); }
            set { this["constraint"] = value; }
        }
    }
}
