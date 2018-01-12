// ***********************************************************************
// Project          : Micua CMS
// Assembly         : Micua.Core
// Author           : iceStone
// Created          : 2013-12-29 6:13 PM
// 
// Last Modified By : iceStone
// Last Modified On : 2013-12-29 6:13 PM
// ***********************************************************************
// <copyright file="ViewEngineConfig.cs" company="Wedn.Net">
//     Copyright © 2014 Wedn.Net. All Rights Reserved.
// </copyright>
// <summary>视图引擎配置类</summary>
// ***********************************************************************

namespace Micua.Core.ViewEngine
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Web.Mvc;

    /// <summary>
    /// 视图引擎配置类
    /// </summary>
    public static class ViewEngineConfig
    {
        /// <summary>
        /// 视图引擎集合
        /// </summary>
        public static IEnumerable<IViewEngine> ViewEngines
        {
            get
            {
                yield return new MicuaViewEngine();
                //yield return new NVelocityViewEngine();
            }
        }

        #region 注册视图引擎 +static void RegisterViewEngines()
        /// <summary>
        /// 注册视图引擎
        /// </summary>
        public static void RegisterViewEngines(ViewEngineCollection engines)
        {
            engines.Clear();
            //engines.Add(new NVelocityViewEngine());
            //engines.Add(new MicuaRazorViewEngine());
            foreach (var item in ViewEngines)
            {
                engines.Insert(0,item);
            }
        }
        #endregion
    }
}