// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UrlExtensions.cs" company="Wedn.Net">
//   Copyright © 2014 Wedn.Net. All Rights Reserved.
// </copyright>
// <summary>
//   UrlHelper拓展方法类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace System.Web.Mvc
{
    using System.Text;
    using System.Web.Routing;
    using Micua.Infrastructure.Utility;

    /// <summary>
    /// UrlHelper拓展方法类
    /// </summary>
    /// <remarks>
    ///  2013-11-23 23:49 Created By iceStone
    /// </remarks>
    public static class UrlExtensions
    {
        #region Front
        #region 获取搜索页面地址
        /// <summary>
        /// 获取搜索页面地址。
        /// </summary>
        /// <remarks>
        ///  2013-12-29 23:49 Created By iceStone
        /// </remarks>
        /// <param name="url">UrlHelper。</param>
        /// <param name="q">搜索关键词。</param>
        /// <returns>搜索页面完全限定 URL。</returns>
        public static string Search(this UrlHelper url, string q = "")
        {
            return Action(url, "Index", "Serch", q.Length == 0 ? null : new { q });
        }
        #endregion

        #region 获取类别页固定链接地址 +static string Category(this UrlHelper url, string item)
        /// <summary>
        /// 获取类别页固定链接地址。
        /// </summary>
        /// <remarks>
        ///  2013-11-23 23:49 Created By iceStone
        /// </remarks>
        /// <param name="url">UrlHelper</param>
        /// <param name="slug">类别项。</param>
        /// <returns>类别页链接完全限定 URL。</returns>
        public static string Category(this UrlHelper url, string slug)
        {
            return Action(url, "Category", "Blog", new { area = string.Empty, item = slug });
        }
        #endregion

        #region 标签页固定链接 +static string Tag(this UrlHelper url, string item)
        /// <summary>
        /// 标签页固定链接
        /// </summary>
        /// <remarks>
        ///  2013-11-23 23:49 Created By iceStone
        /// </remarks>
        /// <param name="url">UrlHelper</param>
        /// <param name="slug">标签项</param>
        /// <returns>标签页链接</returns>
        public static string Tag(this UrlHelper url, string slug)
        {
            return Action(url, "Tag", "Blog", new { area = string.Empty, item = slug });
        }
        #endregion

        #region 作者页固定链接 +static string Author(this UrlHelper url, string item)
        /// <summary>
        /// 作者页固定链接
        /// </summary>
        /// <remarks>
        ///  2013-11-23 23:49 Created By iceStone
        /// </remarks>
        /// <param name="url">UrlHelper</param>
        /// <param name="login">作者项</param>
        /// <returns>作者页链接</returns>
        public static string Author(this UrlHelper url, string login)
        {
            return Action(url, "Author", "Blog", new { area = string.Empty, item = login });
        }
        #endregion

        #region 文章详细页固定链接 +static string Article(this UrlHelper url, string item)
        /// <summary>
        /// 文章详细页固定链接
        /// </summary>
        /// <remarks>
        ///  2013-11-23 23:49 Created By iceStone
        /// </remarks>
        /// <param name="url">UrlHelper</param>
        /// <param name="item">文章项</param>
        /// <returns>文章详细页链接</returns>
        public static string Article(this UrlHelper url, string item)
        {
            return Action(url, "Article", "Blog", new { area = string.Empty, item });
        }
        #endregion

        #region 页面详细页固定链接 +static string Page(this UrlHelper url, string item)
        /// <summary>
        /// 页面详细页固定链接
        /// </summary>
        /// <remarks>
        ///  2013-11-23 23:49 Created By iceStone
        /// </remarks>
        /// <param name="url">UrlHelper</param>
        /// <param name="item">页面项</param>
        /// <returns>页面详细页链接</returns>
        public static string Page(this UrlHelper url, string item)
        {
            return Action(url, "Detail", "Page", new { area = string.Empty, item });
        }
        #endregion
        #endregion

        #region Admin

        #region 获取登录页链接 +static string Login(this UrlHelper url, string redirect = "")
        /// <summary>
        /// 获取登录页链接
        /// </summary>
        /// <param name="url">UrlHelper。</param>
        /// <param name="redirect">跳转地址。</param>
        /// <returns>登录页完全限定 URL。</returns>
        public static string Login(this UrlHelper url, string redirect = "")
        {
            return redirect.Length == 0
                ? Action(url, "Index", "Login", new { area = "Login", redirect })
                : Action(url, "Index", "Login", new { area = "Login" });
        }
        #endregion

        /// <summary>
        /// 管理后台仪表盘链接
        /// </summary>
        /// <param name="url">UrlHelper</param>
        /// <returns>仪表盘链接</returns>
        public static string Dashboard(this UrlHelper url)
        {
            return url.Action("Index", "Dashboard", new { area = "Admin" });
        }

        #region Article编辑页链接 +static string ArticleEdit(this UrlHelper url, int item)
        /// <summary>
        /// Article编辑页链接
        /// </summary>
        /// <remarks>
        ///  2013-11-23 23:49 Created By iceStone
        /// </remarks>
        /// <param name="url">UrlHelper</param>
        /// <param name="item">文章项</param>
        /// <returns>文章详细页链接</returns>
        public static string ArticleEdit(this UrlHelper url, int item)
        {
            return url.Action("Edit", "Post", new { item });
        }
        #endregion

        #endregion

        #region 主题相关
        /// <summary>
        ///  将虚拟（相对主题根目录）路径转换为应用程序绝对路径。
        /// </summary>
        /// <param name="url">UrlHelper</param>
        /// <param name="relativePath">内容的相对路径(不传则返回主题根Url)。</param>
        /// <returns>完全限定 URL。</returns>
        public static string ThemeUrl(this UrlHelper url, string relativePath = "")
        {
            return Config.ThemeUrl + "/" + relativePath.TrimStart(new[] { '~', '/' });
        }
        #endregion

        #region Base

        /// <summary>
        /// (自定义)使用指定的操作名称、控制器名称、路由值、要使用的协议和主机名生成操作方法的完全限定 URL。
        /// </summary>
        /// <param name="url">UrlHelper。</param>
        /// <param name="actionName">操作方法的名称。</param>
        /// <param name="controllerName">控制器的名称。</param>
        /// <param name="routeValues">一个包含路由参数的对象。</param>
        /// <returns>操作方法的完全限定 URL。</returns>
        public static string Action(this UrlHelper url, string actionName, string controllerName, object routeValues)
        {
            var rUrl = url.RequestContext.HttpContext.Request.Url;
            return rUrl != null ? url.Action(actionName, controllerName, new RouteValueDictionary(routeValues), rUrl.Scheme, rUrl.Authority) : string.Empty;
        }

        /// <summary>
        /// (自定义)使用指定的虚拟路径生成操作方法的完全限定 URL。
        /// </summary>
        /// <param name="url">UrlHelper。</param>
        /// <param name="virtualPath">虚拟路径。</param>
        /// <returns>操作方法的完全限定 URL。</returns>
        public static string Content(this UrlHelper url, string virtualPath)
        {
            return string.Format("{0}{1}", SiteUrl(url), url.Content(virtualPath));
        }

        #region 站点Url(根据请求信息得到) +string SiteUrl
        /// <summary>
        /// 站点Url(根据请求信息得到)
        /// </summary>
        /// <value>The site URL.</value>
        public static string SiteUrl(this UrlHelper url)
        {
            var u = url.RequestContext.HttpContext.Request.Url;
            if (u != null)
            {
                return string.Format("{0}://{1}{2}", u.Scheme, u.Authority, url.RequestContext.HttpContext.Request.ApplicationPath).TrimEnd('/').ToLower();
            }
            return Setting.GetString("site_url");
        }
        #endregion

        #endregion

        /// <summary>
        /// 使用指定的操作名称、控制器名称和路由值生成操作方法的完全限定 URL。
        /// </summary>
        /// <param name="urlHelper"> The url Helper. </param>
        /// <param name="actionName"> 操作方法的名称。 </param>
        /// <param name="controllerName"> 控制器的名称。 </param>
        /// <param name="hash"> 锚点 </param>
        /// <param name="routeValues"> 一个包含路由参数的对象。通过检查对象的属性，利用反射检索参数。该对象通常是使用对象初始值设定项语法创建的。 </param>
        /// <returns> 操作方法的完全限定 URL。 </returns>
        public static string Action(this UrlHelper urlHelper, string actionName, string controllerName, string hash, object routeValues)
        {
            if (string.IsNullOrEmpty(hash))
            {
                return urlHelper.Action(actionName, controllerName, routeValues);
            }

            return string.Format("{0}#{1}", urlHelper.Action(actionName, controllerName, routeValues), hash);
        }
    }
}
