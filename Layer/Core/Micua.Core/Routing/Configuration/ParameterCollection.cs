using System.Configuration;

namespace Micua.Core.Routing.Configuration
{
    /// <summary>
    /// 参数集合
    /// </summary>
    public class ParameterCollection : ConfigurationElementCollection
    {
        /// <summary>
        /// 索引器
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Parameter this[int index]
        {
            get
            {
                return base.BaseGet(index) as Parameter;
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
            return new Parameter();
        }

        /// <summary>
        /// 获取键
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((Parameter)element).Name;
        }
    }
}
