// ***********************************************************************
// Project          : Micua
// Assembly         : Micua.Core.Routing
// Author           : iceStone
// Created          : 2014-01-01 10:17 PM
// 
// Last Modified By : iceStone
// Last Modified On : 2014-01-01 10:17 PM
// ***********************************************************************
// <copyright file="Namespace.cs" company="Wedn.Net">
//     Copyright © 2014 Wedn.Net. All Rights Reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Micua.Core.Routing.Configuration
{
    using System.Configuration;

    /// <summary>
    /// 命名空间
    /// </summary>
    public class Namespace : ConfigurationElement
    {
        /// <summary>
        /// 参数名
        /// </summary>
        [ConfigurationProperty("value", IsRequired = true, IsKey = true)]
        public string Value
        {
            get { return this["value"].ToString(); }
            set { this["value"] = value; }
        }
    }
}