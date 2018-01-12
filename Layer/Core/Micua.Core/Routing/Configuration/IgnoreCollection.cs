using System.Configuration;

namespace Micua.Core.Routing.Configuration
{
    /// <summary>
    /// 忽略Routing的项集合
    /// </summary>
    public class IgnoreCollection : ConfigurationElementCollection
    {
        /// <summary>
        /// 索引器
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Ignore this[int index]
        {
            get
            {
                return base.BaseGet(index) as Ignore;
            }
            set
            {
                if (base.BaseGet(index) != null)
                {
                    base.BaseRemoveAt(index);
                }
                this.BaseAdd(index, value);
            }
        }
        /// <summary>
        /// 创建新元素
        /// </summary>
        /// <returns></returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new Ignore();
        }
        /// <summary>
        /// 获取键
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((Ignore)element).Url;
        }
    }
}
