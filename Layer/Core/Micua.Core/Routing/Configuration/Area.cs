// ***********************************************************************
// Project          : Micua CMS
// Assembly         : Micua.Core.Routing
// Author           : iceStone
// Created          : 2014-01-02 9:25 PM
// 
// Last Modified By : iceStone
// Last Modified On : 2014-01-02 9:25 PM
// ***********************************************************************
// <copyright file="Area.cs" company="Wedn.Net">
//     Copyright © 2014 Wedn.Net. All Rights Reserved.
// </copyright>
// <summary>区域项</summary>
// ***********************************************************************
using System.Configuration;
namespace Micua.Core.Routing.Configuration
{
    /// <summary>
    /// 区域项
    /// </summary>
    public class Area : ConfigurationElementCollection
    {
        public Area()
        {
            this.AddElementName = "route";
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new Map();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((Map)element).Name;
        }
        /// <summary>
        /// Area的名称
        /// </summary>
        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        public string Name
        {
            get { return this["name"].ToString(); }
            set { this["name"] = value; }
        }
    }
}