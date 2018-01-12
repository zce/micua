// ***********************************************************************
// Project          : Micua
// Assembly         : Micua.Core.Routing
// Author           : iceStone
// Created          : 2014-01-01 10:15 PM
// 
// Last Modified By : iceStone
// Last Modified On : 2014-01-01 10:15 PM
// ***********************************************************************
// <copyright file="NamespaceCollection.cs" company="Wedn.Net">
//     Copyright © 2014 Wedn.Net. All Rights Reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Micua.Core.Routing.Configuration
{
    using System.Configuration;
    using System.Linq;

    /// <summary>
    /// 默认命名空间集合
    /// </summary>
    public class NamespaceCollection : ConfigurationElementCollection
    {
        /// <summary>
        /// 索引器
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Namespace this[int index]
        {
            get
            {
                return base.BaseGet(index) as Namespace;
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
            return new Namespace();
        }

        /// <summary>
        /// 获取键
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((Namespace)element).Value;
        }

        /// <summary>
        /// 转换成字符串数组
        /// </summary>
        /// <returns>字符串数组</returns>
        public string[] ToArray()
        {
            return (from Namespace item in this select item.Value).ToArray();
        }
    }
}