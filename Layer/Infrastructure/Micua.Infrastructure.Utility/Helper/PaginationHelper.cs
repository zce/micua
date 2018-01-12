// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PagerHelper.cs" company="Wedn.Net">
//     Copyright © 2014 Wedn.Net. All Rights Reserved.
// </copyright>
// <summary>
//   分页助手类  0.10
//   Verion:0.10
//   Description:生成分页代码
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Micua.Infrastructure.Utility
{
    using System;
    using System.Text;
    /// <summary>
    /// 分页助手类
    /// </summary>
    public static class PaginationHelper
    {
        #region 生成分页HTML代码 +static string GetPagination(string format, int totalPages, int current = 1, int showCount = 9, string ulContainerClass = "pagination", string activeLiClass = "active", char separator = '@')
        /// <summary>
        /// 生成分页HTML代码
        /// </summary>
        /// <param name="format">当前URL格式其中分页位置用@代替</param>
        /// <param name="totalPages">总页数</param>
        /// <param name="current">当前页码</param>
        /// <param name="showCount">显示几个页码</param>
        /// <param name="ulContainerClass">分页HTML中外层ul容器的class</param>
        /// <param name="activeLiClass">当前激活状态的li的class</param>
        /// <param name="separator">模版中的页码占位符</param>
        /// <example>
        /// <code>
        ///     <div><%=PaginationHelper.GetPagination("http://www.wedn.net/cat=1&page=@&other=something",10,1,11)%></div>
        /// </code>
        /// </example>
        /// <returns>分页HTML代码</returns>
        public static string GetPagination(string format, int totalPages, int current = 1, int showCount = 9, string ulContainerClass = "pagination", string activeLiClass = "active", char separator = '@')
        {
            var tempFormats = format.Split(separator);
            // url 前缀
            var prefix = tempFormats[0];
            // url 后缀
            var suffix = tempFormats.Length > 1 ? tempFormats[1] : string.Empty;
            // var totalPages = Math.Max((totalCount + pageSize - 1) / pageSize, 1); //总页数
            // 左右区间
            var region = (int)Math.Floor(showCount / 2.0);
            // 开始页码数
            var beginNum = current - region <= 0 ? 1 : current - region;
            // 结束页码数
            var endNum = beginNum + showCount;
            if (endNum > totalPages)
            {
                endNum = totalPages + 1;
                beginNum = endNum - showCount;
                beginNum = beginNum < 1 ? 1 : beginNum;
            }
            var pager = new StringBuilder(string.Format("<ul class=\"{0}\">\r\n", ulContainerClass));
            if (current != 1)
            {
                pager.AppendFormat("\t<li><a href=\"{1}{0}{2}\">上一页</a></li>\r\n", current - 1, prefix, suffix);
            }
            if (beginNum != 1)
            {
                pager.Append("\t<li><span>&hellip;</span></li>\r\n");
            }
            for (var i = beginNum; i < endNum; i++)
            {
                if (i != current)
                {
                    pager.AppendFormat("\t<li><a href=\"{1}{0}{2}\">{0}</a></li>\r\n", i, prefix, suffix);
                }
                else
                {
                    pager.AppendFormat("\t<li class=\"active\"><span>{0}</span></li>\r\n", current);
                }
            }
            if (endNum != totalPages + 1)
            {
                pager.Append("\t<li><span>&hellip;</span></li>\r\n");
            }
            if (current != totalPages)
            {
                pager.AppendFormat("\t<li><a href=\"{1}{0}{2}\">下一页</a></li>\r\n", current + 1, prefix, suffix);
            }
            pager.Append("</ul>");
            return pager.ToString();
        }
        #endregion
    }
}
