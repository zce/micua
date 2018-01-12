using System.Configuration;
using System.Web.Routing;

namespace Micua.Core.Routing.Configuration
{
    /// <summary>
    /// 约束配置元素的集合
    /// </summary>
    public class ConstraintCollection : ConfigurationElementCollection
    {
        /// <summary>
        /// 索引器
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Constraint this[int index]
        {
            get
            {
                return base.BaseGet(index) as Constraint;
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
            return new Constraint();
        }
        /// <summary>
        /// 获取键
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((Constraint)element).Name;
        }

        /// <summary>
        /// 转换成路由值的键值对集合
        /// </summary>
        /// <returns>路由值的键值对集合</returns>
        public RouteValueDictionary ToRouteValueDictionary()
        {
            var constraints = new RouteValueDictionary();
            foreach (Constraint constraint in this)
                constraints.Add(constraint.Name, constraint.Value);
            return constraints;
        }
    }
}
