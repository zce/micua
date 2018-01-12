using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Micua.Domain.Model;
using Micua.Domain.Service;
using Micua.Infrastructure.Utility;
using Micua.Application.Models;

namespace Micua.Application.Controllers.bak
{
    public class PostController : Controller
    {
        /// <summary>
        /// 发表物服务对象
        /// </summary>
        [Dependency]
        public IPostService PostService { get; set; }

        [Dependency]
        public ITermService TermService { get; set; }
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
                    .Select(p => new PostExcerpt { Id = p.Id, Title = p.Title, Excerpt = p.Excerpt });
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
                posts = list.OrderByDescending(p => p.Published).Skip(size * (page - 1)).Take(size)
                    .Select(p => new PostExcerpt { Id = p.Id, Title = p.Title, Excerpt = p.Excerpt });
            }
            return PartialView(posts);
        }

        public ActionResult Index()
        {
            var posts = PostService.Query().OrderByDescending(p => p.Id).Take(24);
            return Json(posts.Select(p => new { p.Id, p.Excerpt, p.Author }), JsonRequestBehavior.AllowGet);
        }
        public ActionResult Detail(int item)
        {
            var post = PostService.QuerySingle(p => p.Id.Equals(item));
            return Json(new
            {
                post.Id,
                post.Title,
                post.Excerpt,
                post.Content,
                Comments = post.Comments.Select(c => new { c.Id, c.Content, c.Author, Commented = c.Commented.ToString() })
            }, JsonRequestBehavior.AllowGet);
        }
    }
}