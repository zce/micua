// +----------------------------------------------------------------------
// | Micua [ DO IT BATTER ! ]
// +----------------------------------------------------------------------
// | Copyright © 2014 Wedn.Net. All Rights Reserved.
// +----------------------------------------------------------------------
// | Summary : Blog（实体类）
// +----------------------------------------------------------------------
// | Author  : iceStone <ice@wedn.net>
// +----------------------------------------------------------------------

namespace Micua.Domain.Model
{
    using System.Collections.Generic;

    /// <summary>
    /// Blog（实体类）
    /// </summary>
    public partial class Blog : EntityBase<int>
    {

        #region Constructors
        /// <summary>
        /// 创建一个Blog实例对象
        /// </summary>
        public Blog()
        {

            this.Slug = string.Empty;

            this.Name = string.Empty;

            this.SubName = string.Empty;

            this.Created = System.DateTime.Now;

            this.BlogMetas = new List<BlogMeta>();

            this.Comments = new List<Comment>();

            this.Links = new List<Link>();

            this.Posts = new List<Post>();

        }
        #endregion

        #region Simple Properties

        public override int Id { get; set; }

        public string Slug { get; set; }

        public string Name { get; set; }

        public string SubName { get; set; }

        public System.DateTime Created { get; set; }

        public BlogStatus Status { get; set; }

        public int UserId { get; set; }

        #endregion

        #region Navigation Properties

        public virtual User User { get; set; }

        public virtual ICollection<BlogMeta> BlogMetas { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Link> Links { get; set; }

        public virtual ICollection<Post> Posts { get; set; }

        #endregion

    }
}
