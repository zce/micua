namespace Micua.Plugins.Blog.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Microsoft.Practices.Unity;
    using Micua.Application.Controllers;
    using Micua.Domain.Model;
    using Micua.Domain.Service;
    using Micua.Infrastructure.Utility;
    using Micua.Application.Models;
    /// <summary>
    /// PostController
    /// </summary>
    /// <remarks>
    /// 2014-08-19 12:16:15 Created by iceStone
    /// </remarks>
    public class PostController : MicuaController
    {
        #region Fields

        #endregion

        #region Properties
        [Dependency]
        public IPostService PostService { get; set; }

        [Dependency]
        public ITermService TermService { get; set; }
        #endregion

        #region Constructors

        #endregion

        #region Action
        /// <summary>
        /// 详细页
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public ActionResult Detail(string item = "")
        {
            if (string.IsNullOrEmpty(item))
            {
                return HttpNotFound();
            }
            var post = PostService.QuerySingle(p => p.Slug.Equals(item));
            return Json(new
            {
                post.Id,
                post.Title,
                post.Excerpt,
                post.Content,
                Comments = post.Comments.Select(c => new { c.Id, c.Content, c.Author, Commented = c.Commented.ToString() })
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region ChildAction
        /// <summary>
        /// 首页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="cat"></param>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult List(int page = 1, int cat = 0)
        {
            var size = Setting.GetInt32("post_page_size", 20);
            IEnumerable<PostExcerpt> posts;
            int total;
            if (cat == 0)
            {
                posts = PostService.QueryPage(page, size, out total, p => p.Status == PostStatus.Public, p => p.Published, true)
                    .Select(p => new PostExcerpt { Id = p.Id, Slug = p.Slug, Title = p.Title, Excerpt = p.Excerpt });
            }
            else
            {
                var term = TermService.QuerySingle(t => t.Id.Equals(cat));
                if (term == null)
                {
                    return HttpNotFound();
                }
                var list = from p in PostService.Query()
                           join r in term.Relations
                           on p.Id equals r.ObjectId
                           where p.Status == PostStatus.Public
                           select p;
                total = list.Count();
                posts = list.OrderByDescending(p => p.Published).Skip(size * (page - 1)).Take(size)
                    .Select(p => new PostExcerpt { Id = p.Id, Slug = p.Slug, Title = p.Title, Excerpt = p.Excerpt });
            }
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Floor(total / (double)size);
            return PartialView(posts);
        }
        #endregion

        #region Utilities

        #endregion
    }
}