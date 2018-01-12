// +----------------------------------------------------------------------
// | Micua [ DO IT BATTER ! ]
// +----------------------------------------------------------------------
// | Copyright © 2014 Wedn.Net. All Rights Reserved.
// +----------------------------------------------------------------------
// | Summary : User（实体类）
// +----------------------------------------------------------------------
// | Author  : iceStone <ice@wedn.net>
// +----------------------------------------------------------------------

namespace Micua.Domain.Model
{
    using System.Collections.Generic;

    /// <summary>
    /// User（实体类）
    /// </summary>
    public partial class User : EntityBase<int>
    {

        #region Constructors
        /// <summary>
        /// 创建一个User实例对象
        /// </summary>
        public User()
        {

            this.Login = string.Empty;

            this.Password = string.Empty;

            this.Nickname = string.Empty;

            this.Mobile = string.Empty;

            this.Email = string.Empty;

            this.Link = string.Empty;

            this.Introduce = string.Empty;

            this.Registered = System.DateTime.Now;

            this.Token = string.Empty;

            this.Blogs = new List<Blog>();

            this.Comments = new List<Comment>();

            this.Posts = new List<Post>();

            this.UserMetas = new List<UserMeta>();

        }
        #endregion

        #region Simple Properties

        public override int Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string Nickname { get; set; }

        public string Mobile { get; set; }

        public string Email { get; set; }

        public string Link { get; set; }

        public string Introduce { get; set; }

        public UserRole Role { get; set; }

        public UserStatus Status { get; set; }

        public System.DateTime Registered { get; set; }

        public string Token { get; set; }

        #endregion

        #region Navigation Properties

        public virtual ICollection<Blog> Blogs { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Post> Posts { get; set; }

        public virtual ICollection<UserMeta> UserMetas { get; set; }

        #endregion

    }
}
