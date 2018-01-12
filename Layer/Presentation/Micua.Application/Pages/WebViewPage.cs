// ***********************************************************************
// Project			: Micua CMS
// Assembly         : Micua.Core
// Author           : iceStone
// Created          : 2013-11-23 23:09
//
// Last Modified By : iceStone
// Last Modified On : 2013-11-23 23:15
// ***********************************************************************
// <copyright file="WebViewPage.cs" company="Wedn.Net">
//     Copyright © 2014 Wedn.Net. All Rights Reserved.
// </copyright>
// <summary>缺少注释</summary>
// ***********************************************************************

namespace Micua.Application.Pages
{
    using System.Web.Mvc;

    using Micua.Infrastructure.Utility;

    /// <summary>
    /// 主题视图基类
    /// </summary>
    /// <remarks>
    ///  2013-11-24 11:15 Created By iceStone
    /// </remarks>
    /// <typeparam name="TModel">The type of the t model.</typeparam>
    public abstract class WebViewPage<TModel> : BaseViewPage<TModel>
    {
        #region Properties

        public override string Layout
        {
            get
            {
                return base.Layout;
            }
            set
            {
                base.Layout = value;
            }
        }

        #region 统计代码 +MvcHtmlString AnalyticsCode
        /// <summary>
        /// 统计代码
        /// </summary>
        public MvcHtmlString AnalyticsCode
        {
            get { return MvcHtmlString.Create(Setting.GetString("site_analytics_code")); }
        }
        #endregion

        /// <summary>
        /// 主题所在URL
        /// </summary>
        /// <value>The theme URL.</value>
        protected string ThemeUrl
        {
            get
            {
                return Url.ThemeUrl();
            }
        }

        ///// <summary>
        ///// 页面执行数据库查询次数
        ///// </summary>
        ///// <value>The queries.</value>
        //protected int Queries { get { return Statistics.Instance.QueryCount; } }

        ///// <summary>
        ///// 页面执行数据库查询次数
        ///// </summary>
        ///// <value>The excute time.</value>
        //protected double ExcuteTime { get { return Statistics.Instance.ExcuteTime.TotalSeconds; } }
        #endregion

        #region 模版常用方法

        #region 获取主题设置信息 +string ThemeInfo(string name)
        /// <summary>
        /// 获取主题设置信息
        /// </summary>
        /// <remarks>
        ///  2013-11-24 11:15 Created By iceStone
        /// </remarks>
        /// <param name="name">The name.</param>
        /// <returns>System.String.</returns>
        protected string ThemeInfo(string name) { return Setting.GetString(name); }
        #endregion

        //#region 获取主题包资源文件URL +string GetThemeUrl(string relpath)
        ///// <summary>
        ///// 获取主题包资源文件URL
        ///// </summary>
        ///// <remarks>
        /////  2013-11-24 11:15 Created By iceStone
        ///// </remarks>
        ///// <param name="relpath">相对路径</param>
        ///// <returns>文件URL</returns>
        //protected string GetThemeUrl(string relpath)
        //{
        //    return Url.Theme(relpath.TrimStart('~').TrimStart('/'));
        //}
        //#endregion

        //#region 渲染头部(Header)部分系统标签 +MvcHtmlString RenderHeader()
        ///// <summary>
        ///// 渲染头部(Header)部分系统标签
        ///// </summary>
        ///// <remarks>
        /////  2013-11-24 11:15 Created By iceStone
        ///// </remarks>
        ///// <returns>MvcHtmlString.</returns>
        //public MvcHtmlString RenderHeader()
        //{
        //    var builder = new TagBuilder("meta");
        //    builder.Attributes.Add("name", "author");
        //    builder.Attributes.Add("content", Setting.GetString(OptionKeys.MetaAuthor));
        //    return MvcHtmlString.Create(builder.ToString(TagRenderMode.SelfClosing));
        //}
        //#endregion

        //#region 渲染菜单导航 +MvcHtmlString RenderMenuNav(string ulId = "nav", string ulClass = "nav", string subUlClass = "sub-menu", string itemClass = "menu-item")
        ///// <summary>
        ///// 渲染菜单导航
        ///// </summary>
        ///// <remarks>
        /////  2013-11-24 11:15 Created By iceStone
        ///// </remarks>
        ///// <param name="ulId">总UL ID</param>
        ///// <param name="ulClass">总UL 类名</param>
        ///// <param name="subUlClass">子UL 类名</param>
        ///// <param name="itemClass">单项类名</param>
        ///// <returns>菜单导航Html</returns>
        //public MvcHtmlString RenderMenuNav(string ulId = "nav", string ulClass = "nav", string subUlClass = "sub-nav", string itemClass = "menu-item")
        //{
        //    var menu = new MenuNav(Context.Request.RawUrl, ulId, ulClass, subUlClass, itemClass);
        //    return menu.Render();
        //}
        //#endregion

        #region 渲染分页代码 +MvcHtmlString RenderPagination(int totalCount, int current = 1, int pageSize = 10, int showCount = 9)
        /// <summary>
        /// 渲染分页代码
        /// </summary>
        /// <remarks>
        ///  2013-11-24 11:15 Created By iceStone
        /// </remarks>
        /// <param name="totalCount">The total count.</param>
        /// <param name="current">The current.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="showCount">The show count.</param>
        /// <returns>MvcHtmlString.</returns>
        public MvcHtmlString RenderPagination(string action, string controller, int totalPages, int current = 1, int showCount = 9, string ulContainerClass = "pagination", string activeLiClass = "active")
        {
            return Html.Pagination(action, controller, totalPages, current, showCount, ulContainerClass, activeLiClass);
        }
        #endregion

        //#region 渲染脚注(Footer)部分系统标签 +MvcHtmlString RenderFooter()
        ///// <summary>
        ///// 渲染脚注(Footer)部分系统标签
        ///// </summary>
        ///// <remarks>
        /////  2013-11-24 11:15 Created By iceStone
        ///// </remarks>
        ///// <returns>MvcHtmlString.</returns>
        //public MvcHtmlString RenderFooter()
        //{
        //    return MvcHtmlString.Empty;
        //}
        //#endregion

        #endregion
    }
}
