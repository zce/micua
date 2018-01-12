// +----------------------------------------------------------------------
// | Micua [ DO IT BATTER ! ]
// +----------------------------------------------------------------------
// | Copyright © 2014 Wedn.Net. All Rights Reserved.
// +----------------------------------------------------------------------
// | Summary : Comment（实体类）
// +----------------------------------------------------------------------
// | Author  : iceStone <ice@wedn.net>
// +----------------------------------------------------------------------

namespace Micua.Domain.Model
{
    using System.Collections.Generic;

    /// <summary>
    /// Comment（实体类）
    /// </summary>
    public partial class Comment : EntityBase<int>
    {

        #region Constructors
        /// <summary>
        /// 创建一个Comment实例对象
        /// </summary>
        public Comment()
        {

            this.Author = string.Empty;

            this.Email = string.Empty;

            this.Link = string.Empty;

            this.Commented = System.DateTime.Now;

            this.Content = string.Empty;

            this.Children = new List<Comment>();

            this.CommentMetas = new List<CommentMeta>();

        }
        #endregion

        #region Simple Properties

        public override int Id { get; set; }

        public string Author { get; set; }

        public string Email { get; set; }

        public string Link { get; set; }

        public System.DateTime Commented { get; set; }

        public string Content { get; set; }

        public CommentStatus Status { get; set; }

        public int UserId { get; set; }

        public int BlogId { get; set; }

        public int PostId { get; set; }

        public int? ParentId { get; set; }

        #endregion

        #region Navigation Properties

        public virtual Blog Blog { get; set; }

        public virtual ICollection<Comment> Children { get; set; }

        public virtual Comment Parent { get; set; }

        public virtual Post Post { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<CommentMeta> CommentMetas { get; set; }

        #endregion

    }
}
