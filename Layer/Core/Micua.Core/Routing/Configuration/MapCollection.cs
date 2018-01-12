using System;
using System.Configuration;

namespace Micua.Core.Routing.Configuration
{
    /// <summary>
    /// 路由规则配置项集合
    /// </summary>
    public class MapCollection : ConfigurationElementCollection
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public MapCollection()
        {
            this.AddElementName = "map";
        }
        /// <summary>
        /// 索引器
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Map this[int index]
        {
            get { return base.BaseGet(index) as Map; }

            set
            {
                if (base.BaseGet(index) != null) base.BaseRemoveAt(index);
                this.BaseAdd(index, value);
            }
        }
        /// <summary>
        /// 创建新元素
        /// </summary>
        /// <returns></returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new Map();
        }
        /// <summary>
        /// 获取键
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((Map)element).Name;
        }
        /// <summary>
        /// 默认值
        /// </summary>
        [ConfigurationProperty("default", IsRequired = true)]
        public string Default
        {
            get { return this["default"].ToString(); }
            set { this["default"] = value; }
        }
        /// <summary>
        /// 是否启用
        /// </summary>
        [ConfigurationProperty("enable", IsRequired = true, DefaultValue = true)]
        public bool Enable
        {
            get { return Boolean.Parse(this["enable"].ToString()); }
            set { this["enable"] = value; }
        }
    }
}
