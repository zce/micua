using System.Text;
using System.Web.Mvc.Html;
using Micua.Infrastructure.Utility;
namespace System.Web.Mvc
{
    public static class HtmlExtensions
    {
        /// <summary>
        /// 挂件
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        /// <param name="containerName">区域标识</param>
        /// <returns></returns>
        public static MvcHtmlString Widget(this HtmlHelper helper, string containerName)
        {
            return helper.Action("Index", "Widget", new { containerName, area = string.Empty });
        }

        #region 生成分页HTML代码 +static MvcHtmlString Pagination(this HtmlHelper html, string action, string controller, int totalPages, int current = 1, int showCount = 9, string ulContainerClass = "pagination", string activeLiClass = "active")
        /// <summary>
        /// 生成分页HTML代码
        /// </summary>
        /// <param name="html">HtmlHelper</param>
        /// <param name="action">操作名</param>
        /// <param name="controller">控制器名</param>
        /// <param name="totalPages">总页数</param>
        /// <param name="current">当前页码</param>
        /// <param name="showCount">显示几个页码</param>
        /// <param name="ulContainerClass">分页HTML中外层ul容器的class</param>
        /// <param name="activeLiClass">当前激活状态的li的class</param>
        /// <example>
        /// <code>
        ///     <div>@Html.Pagination("List", "Post", 10 , 1)%></div>
        /// </code>
        /// </example>
        /// <returns>分页HTML代码</returns>
        public static MvcHtmlString Pagination(this HtmlHelper html, string action, string controller, int totalPages, int current = 1, int showCount = 9, string ulContainerClass = "pagination", string activeLiClass = "active")
        {
            var topContext = html.ViewContext;
            while (topContext.IsChildAction)
            {
                topContext = topContext.ParentActionViewContext;
            }
            var url = new UrlHelper(topContext.RequestContext);
            var routeValues = topContext.RouteData.Values;
            if (routeValues.ContainsKey("page"))
            {
                routeValues.Remove("page");
            }
            routeValues.Add("page", '*');
            var format = url.Action(action, controller, routeValues).Replace("%2A", "*");

            return new MvcHtmlString(PaginationHelper.GetPagination(format, totalPages, current, showCount, ulContainerClass, activeLiClass, '*'));
            //var totalPages = Math.Max((totalCount + pageSize - 1) / pageSize, 1); //总页数
            //var region = (int)Math.Floor(showCount / 2.0);
            //var beginNum = current - region <= 0 ? 1 : current - region;
            //var endNum = beginNum + showCount;
            //if (endNum > totalPages)
            //{
            //    endNum = totalPages + 1;
            //    beginNum = endNum - showCount;
            //    beginNum = beginNum < 1 ? 1 : beginNum;
            //}
            //var pagination = new StringBuilder("<ul class=\"pagination\">\r\n");
            //if (current != 1)
            //    pagination.AppendFormat("\t<li><a href='{0}'>{1}</a></li>\r\n", url.Action(action, controller, new { page = current - 1 }), "上一页");
            //if (beginNum != 1)
            //    pagination.Append("\t<li><span>&hellip;</span></li>\r\n");
            //for (var i = beginNum; i < endNum; i++)
            //{
            //    if (i != current)
            //        pagination.AppendFormat("\t<li><a href='{0}'>{1}</a></li>\r\n", url.Action(action, controller, new { page = i }), i);
            //    else
            //        pagination.AppendFormat("\t<li class=\"active\"><span>{0}</span></li>\r\n", current);
            //}
            //if (endNum != totalPages + 1)
            //    pagination.Append("\t<li><span>&hellip;</span></li>\r\n");
            //if (current != totalPages)
            //    pagination.AppendFormat("\t<li><a href='{0}'>{1}</a></li>\r\n", url.Action(action, controller, new { page = current + 1 }), "下一页");
            //pagination.Append("</ul>");
        }
        #endregion
    }
}
