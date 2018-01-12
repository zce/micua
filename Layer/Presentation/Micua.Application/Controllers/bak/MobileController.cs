using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Micua.Domain.Model;
using Micua.Domain.Service;
using Micua.Infrastructure.Utility;

namespace Micua.Application.Controllers.bak
{
    public class MobileController : Controller
    {
        // GET: Mobile
        [Dependency]
        public IPostService PostService { get; set; }
        [Dependency]
        public ITermService TermService { get; set; }

        /// <summary>
        /// 前30条照片
        /// </summary>
        /// <returns></returns>
        public ActionResult TopPhotos(int page = 1)
        {
            //Thread.Sleep(3000);
            int total;
            var posts = PostService.QueryPage(page, 30, out total, p => true, p => p.Published, true);
            return Json(posts.Select(p => new { p.Id, p.Excerpt, p.Author }), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 相册
        /// </summary>
        /// <returns></returns>
        public ActionResult Albums()
        {
            var terms = TermService.Query();
            return Json(terms.Select(t => new { t.Id, t.Name, t.Description }), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 照片详细
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public ActionResult Detail(int item)
        {
            var post = PostService.QuerySingle(p => p.Id.Equals(item));
            return Json(new
            {
                post.Id,
                post.Title,
                post.Excerpt,
                post.Content,
                Comments = post.Comments.Select(c => new { c.Id, c.Content, c.Author, c.Commented })
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 相册详细页面
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public ActionResult Photos(int item)
        {
            var term = TermService.QuerySingle(t => t.Id.Equals(item));
            var rel = term.Relations.ToList();
            var posts = from p in PostService.Query()
                        join r in rel.Select(r => r.ObjectId)
                        on p.Id equals r
                        select new
                        {
                            p.Id,
                            p.Author,
                            p.Title,
                            p.Excerpt,
                            p.Content,
                            Comments = p.Comments.Select(c => new { c.Id, c.Content, c.Author, Commented = c.Commented.ToString() })
                        };
            return Json(new { album = new { term.Id, term.Name }, posts }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Upload(string title, int termId, HttpPostedFileBase file)
        {
            var path = "/contents/images/" + file.FileName;
            var post = new Post
            {
                Title = title,
                Slug = Guid.NewGuid().ToString("N"),
                Published = DateTime.Now,
                Modified = DateTime.Now,
                Excerpt = Config.SiteUrl + path,
                BlogId = 1,
                UserId = 1,
                
            };
            PostService.Insert(post, true);
            if (post.Id > 0)
            {
                file.SaveAs(Server.MapPath("~" + path));
                return null;
            }
            else
            {
                return Content("fail shit");
            }
        }
    }
}