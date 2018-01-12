using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Micua.Domain.Service;

namespace Micua.Application.Controllers.bak
{
    public class TermController : Controller
    {
        // GET: Term
        [Microsoft.Practices.Unity.Dependency]
        public ITermService TermService { get; set; }
        [Microsoft.Practices.Unity.Dependency]
        public ITermRelationService TermRelationService { get; set; }
        [Microsoft.Practices.Unity.Dependency]
        public IPostService PostService { get; set; }
        public ActionResult Index()
        {
            var terms = TermService.Query();
            return Json(terms.Select(t => new { t.Id, t.Name, t.Description }), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Detail(int item)
        {
            //var rels = TermRelationService.Query(r => r.TermId.Equals(item));
            //var post = from p in PostService.Query()
            //           join r in rels
            //           on p.Id equals r.ObjectId
            //           select new
            //           {
            //               p.Id,
            //               p.Author,
            //               p.Title,
            //               p.Excerpt,
            //               p.Content,
            //               Comments = p.Comments.Select(c => new { c.Id, c.Content, c.Author, Commented = c.Commented.ToString() })
            //           };

            var rel = TermService.QuerySingle(t => t.Id.Equals(item)).Relations.ToList();
            var posts=from p in PostService.Query()
                      join r in rel.Select(r=>r.ObjectId) 
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
            return Json(posts, JsonRequestBehavior.AllowGet);
        }
    }
}