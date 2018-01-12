// ***********************************************************************
// Project          : Micua
// Assembly         : Micua.Core.Routing
// Author           : iceStone
// Created          : 2014-01-02 9:24 PM
// 
// Last Modified By : iceStone
// Last Modified On : 2014-01-02 9:24 PM
// ***********************************************************************
// <copyright file="AreaCollection.cs" company="Wedn.Net">
//     Copyright © 2014 Wedn.Net. All Rights Reserved.
// </copyright>
// <summary>区域集合</summary>
// ***********************************************************************

namespace Micua.Core.Routing.Configuration
{
    using System;
    using System.Configuration;

    /// <summary>
    /// 区域集合
    /// </summary>
    public class AreaCollection : ConfigurationElementCollection
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public AreaCollection()
        {
            this.AddElementName = "area";
        }
        /// <summary>
        /// 索引器
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Area this[int index]
        {
            get { return base.BaseGet(index) as Area; }

            set
            {
                if (base.BaseGet(index) != null) base.BaseRemoveAt(index);
                this.BaseAdd(index, value);
            }
        }
        protected override ConfigurationElement CreateNewElement()
        {
            return new Area();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((Area)element).Name;
        }
        ///// <summary>
        ///// 默认值
        ///// </summary>
        //[ConfigurationProperty("default", IsRequired = true)]
        //public string Default
        //{
        //    get { return this["default"].ToString(); }
        //    set { this["default"] = value; }
        //}
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