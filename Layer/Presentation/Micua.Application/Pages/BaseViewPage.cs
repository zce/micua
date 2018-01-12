// ***********************************************************************
// Project			: Micua CMS
// Assembly         : Micua.Core
// Author           : iceStone
// Created          : 2013-11-23 23:09
//
// Last Modified By : iceStone
// Last Modified On : 2013-11-23 23:11
// ***********************************************************************
// <copyright file="BaseViewPage.cs" company="Wedn.Net">
//     Copyright © 2014 Wedn.Net. All Rights Reserved.
// </copyright>
// <summary>全部视图基类</summary>
// ***********************************************************************

namespace Micua.Application.Pages
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using System.Web.Mvc.Html;

    using Micua.Core.Localization;
    using Micua.Core.Session;
    using Micua.Domain.Model;
    using Micua.Infrastructure.Utility;

    /// <summary>
    /// 全部视图基类
    /// </summary>
    /// <remarks>
    ///  2013-11-23 23:11 Created By iceStone
    /// </remarks>
    /// <typeparam name="TModel">模型类型</typeparam>
    public abstract class BaseViewPage<TModel> : System.Web.Mvc.WebViewPage<TModel>
    {

        #region 站点Url(根据请求信息得到) +string SiteUrl
        /// <summary>
        /// 站点Url(根据请求信息得到)
        /// </summary>
        /// <value>The site URL.</value>
        public string SiteUrl
        {
            get
            {
                return Url.SiteUrl();
            }
        }
        #endregion

        #region 驱动机 +string Generator
        /// <summary>
        /// 驱动机
        /// </summary>
        public string Generator
        {
            get { return Core.MicuaInfo.Generator; }
        }
        #endregion

        #region 版本 +string Version
        /// <summary>
        /// 版本
        /// </summary>
        public string Version
        {
            get { return Core.MicuaInfo.Version.ToString(); }
        }
        #endregion

        #region 原动力 +string Powered
        /// <summary>
        /// 原动力
        /// </summary>
        public MvcHtmlString Powered
        {
            get { return MvcHtmlString.Create(Core.MicuaInfo.Powered); }
        }
        #endregion

        #region 站点作者 +string Author
        /// <summary>
        /// 站点作者
        /// </summary>
        public string Author
        {
            get { return Setting.GetString("meta_author"); }
        }
        #endregion

        #region 版权声明 +MvcHtmlString Copyright
        /// <summary>
        /// 版权声明
        /// </summary>
        public MvcHtmlString Copyright
        {
            get { return MvcHtmlString.Create(Setting.GetString("site_copyright")); }
        }
        #endregion

        #region 设置信息字典 +IDictionary<string,string> SettingBag
        /// <summary>
        /// 设置信息字典
        /// </summary>
        /// <remarks>
        ///  2013-11-23 22:16 Created By iceStone
        /// </remarks>
        /// <returns>值</returns>
        public IDictionary<string, string> SettingBag
        {
            get { return Setting.Settings; }
        }
        #endregion

        #region CurrentUser

        private User _currentUser;
        /// <summary>
        /// 当前用户对象实体
        /// </summary>
        /// <value>The current user.</value>
        protected User CurrentUser
        {
            get { return _currentUser ?? (_currentUser = SessionHelper.Get<User>(SessionKeys.CurrentUser)); }
        }

        #endregion

        #region 获取网站设置信息 +string SiteInfo(string name)
        /// <summary>
        /// 获取网站设置信息
        /// </summary>
        /// <remarks>
        ///  2013-11-24 11:15 Created By iceStone
        /// </remarks>
        /// <param name="name">The name.</param>
        /// <returns>System.String.</returns>
        protected string SiteInfo(string name) { return Setting.GetString(name); }
        #endregion

        #region 多语言化转意
        /// <summary>
        /// 转义语言
        /// </summary>
        /// <remarks>
        ///  2013-11-23 23:11 Created By iceStone
        /// </remarks>
        /// <param name="key">原文键</param>
        /// <returns>语言化字符串</returns>
        public string P(string key)
        {
            return this.Parse(key);
        }

        /// <summary>
        /// 转义语言
        /// </summary>
        /// <remarks>
        ///  2013-11-23 23:11 Created By iceStone
        /// </remarks>
        /// <param name="key">原文键</param>
        /// <returns>语言化字符串</returns>
        public string Parse(string key)
        {
            return key.Parse(); // key.GetCultureString(Setting.GetString("site_culture"));
        }

        /// <summary>
        /// 转义语言
        /// </summary>
        /// <remarks>
        ///  2013-11-23 23:11 Created By iceStone
        /// </remarks>
        /// <param name="key">原文键</param>
        /// <param name="args">一个对象数组，其中包含零个或多个要设置格式的对象</param>
        /// <returns>语言化字符串</returns>
        public string Parse(string key, params object[] args)
        {
            return key.Parse(args);
        }
        #endregion

        #region 引入以HTML编码字符串形式呈现的分部视图 +MvcHtmlString Include(string viewName)
        /// <summary>
        /// 引入以HTML编码字符串的形式呈现指定的分部视图。
        /// </summary>
        /// <remarks>
        ///  2013-11-23 23:12 Created By iceStone
        /// </remarks>
        /// <param name="viewName">要呈现的分部视图的名称</param>
        /// <returns>以HTML编码字符串形式呈现的分部视图</returns>
        protected MvcHtmlString Include(string viewName)
        {
            return Html.Partial(viewName);
        }
        #endregion

        #region 引入以HTML编码字符串形式呈现的分部视图 +MvcHtmlString Include(string viewName, object model)

        /// <summary>
        /// 引入以HTML编码字符串的形式呈现指定的分部视图。
        /// </summary>
        /// <remarks>
        ///  2013-11-23 23:12 Created By iceStone
        /// </remarks>
        /// <param name="viewName">要呈现的分部视图的名称</param>
        /// <param name="model">视图模型</param>
        /// <returns>以HTML编码字符串形式呈现的分部视图</returns>
        protected MvcHtmlString Include(string viewName, object model)
        {
            return Html.Partial(viewName, model);
        }
        #endregion
    }
}


// namespace System.Web.Mvc {
//    public static class LocalizationHelpers {
//        /// <summary>
//        /// 在外边的 Html 中直接使用
//        /// </summary>
//        /// <param name="htmlhelper"></param>
//        /// <param name="key"></param>
//        /// <returns></returns>
//        public static string Lang(this HtmlHelper htmlhelper, string key) {
//            string FilePath = htmlhelper.ViewContext.HttpContext.Server.MapPath("/") + "Resource\\";
//            return GetLangString(htmlhelper.ViewContext.HttpContext, key, FilePath);
//        }
//        /// <summary>
//        /// 在外边的 Html 中直接使用，对 JS 进行输出字符串
//        /// </summary>
//        /// <param name="htmlhelper"></param>
//        /// <param name="key"></param>
//        /// <returns></returns>
//        public static string LangOutJsVar(this HtmlHelper htmlhelper, string key) {
//            string FilePath = htmlhelper.ViewContext.HttpContext.Server.MapPath("/") + "Resource\\";
//            string langstr = GetLangString(htmlhelper.ViewContext.HttpContext, key, FilePath);
//            return string.Format("var {0} = '{1}'", key,langstr);
//        }
//        /// <summary>
//        /// 在 C# 中使用
//        /// </summary>
//        /// <param name="httpContext"></param>
//        /// <param name="key"></param>
//        /// <returns></returns>
//        public static string InnerLang(HttpContextBase httpContext, string key) {
//            string FilePath = httpContext.Server.MapPath("/") + "Resource\\";
//            return GetLangString(httpContext, key, FilePath);
//        }

//        private static string GetLangString(HttpContextBase httpContext, string key, string FilePath) {
//            LangType langtype = LangType.cn;
//            if (httpContext.Session["Lang"] != null) {
//                langtype = (LangType)httpContext.Session["Lang"];
//            }
//            return LangResourceFileProvider.GetLangString(key, langtype, FilePath);
//        }
//    }

//    public static class LangResourceFileProvider {
//        public static string GetLangString(string Key, LangType langtype, string FilePath) {
//            string filename;
//            switch (langtype) {
//                case LangType.cn: filename = "zh-cn.resources"; break;
//                case LangType.en: filename = "en-us.resources"; break;
//                default: filename = "zh-cn.resources"; break;
//            }

//            System.Resources.ResourceReader reader = new System.Resources.ResourceReader(FilePath + filename);

//            string resourcetype;
//            byte[] resourcedata;
//            string result = string.Empty;

//            try {
//                reader.GetResourceData(Key, out resourcetype, out resourcedata);
//                //去掉第一个字节，无用
//                byte[] arr = new byte[resourcedata.Length - 1];
//                for (int i = 0; i < arr.Length; i++) {
//                    arr[i] = resourcedata[i + 1];
//                }
//                result = System.Text.Encoding.UTF8.GetString(arr);

//            }
//            catch (Exception ex) {

//            }
//            finally {
//                reader.Close();
//            }

//            return result;
//        }
//    }

//    public enum LangType {
//            cn,
//            en
//        }
//}
