using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Micua.Application.Models
{
    public class PostExcerpt
    {
        public int Id { get; set; }
        public string Slug { get; set; }
        public string Title { get; set; }
        public string Excerpt { get; set; }
    }
}