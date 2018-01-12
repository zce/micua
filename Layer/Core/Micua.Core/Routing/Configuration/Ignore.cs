using System.Configuration;
using Micua.Core.Routing.Configuration;

namespace Micua.Core.Routing.Configuration
{
    /// <summary>
    /// 忽略Routing的项
    /// </summary>
    public class Ignore : ConfigurationElement
    {
        /// <summary>
        /// URL
        /// </summary>
        [ConfigurationProperty("url", IsRequired = true, IsKey = true)]
        public string Url
        {
            get { return this["url"].ToString(); }
            set { this["url"] = value; }
        }
        /// <summary>
        /// 约束
        /// </summary>
        [ConfigurationProperty("constraints", IsRequired = false)]
        public ConstraintCollection Constraints
        {
            get { return this["constraints"] as ConstraintCollection; }
            set { this["constraints"] = value; }
        }
    }
}
