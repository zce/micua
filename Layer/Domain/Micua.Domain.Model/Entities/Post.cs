// +----------------------------------------------------------------------
// | Micua [ DO IT BATTER ! ]
// +----------------------------------------------------------------------
// | Copyright © 2014 Wedn.Net. All Rights Reserved.
// +----------------------------------------------------------------------
// | Summary : Post（实体类）
// +----------------------------------------------------------------------
// | Author  : iceStone <ice@wedn.net>
// +----------------------------------------------------------------------

namespace Micua.Domain.Model
{
    using System.Collections.Generic;

    /// <summary>
    /// Post（实体类）
    /// </summary>
    public partial class Post : EntityBase<int>
    {

        #region Constructors
        /// <summary>
        /// 创建一个Post实例对象
        /// </summary>
        public Post()
        {

            this.Slug = string.Empty;

            this.Author = string.Empty;

            this.Title = string.Empty;

            this.Published = System.DateTime.Now;

            this.Modified = System.DateTime.Now;

            this.Content = string.Empty;

            this.Excerpt = string.Empty;

            this.Comments = new List<Comment>();

            this.Children = new List<Post>();

            this.PostMetas = new List<PostMeta>();

        }
        #endregion

        #region Simple Properties

        public override int Id { get; set; }

        public string Slug { get; set; }

        public string Author { get; set; }

        public string Title { get; set; }

        public System.DateTime Published { get; set; }

        public System.DateTime Modified { get; set; }

        public string Content { get; set; }

        public string Excerpt { get; set; }

        public PostType Type { get; set; }

        public PostStatus Status { get; set; }

        public PostCommentStatus CommentStatus { get; set; }

        public PostPingStatus PingStatus { get; set; }

        public short Sort { get; set; }

        public int ViewCount { get; set; }

        public int CommentCount { get; set; }

        public int UserId { get; set; }

        public int BlogId { get; set; }

        public int? ParentId { get; set; }

        #endregion

        #region Navigation Properties

        public virtual Blog Blog { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Post> Children { get; set; }

        public virtual Post Parent { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<PostMeta> PostMetas { get; set; }

        #endregion

    }
}
