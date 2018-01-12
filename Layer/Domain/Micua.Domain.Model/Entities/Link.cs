// +----------------------------------------------------------------------
// | Micua [ DO IT BATTER ! ]
// +----------------------------------------------------------------------
// | Copyright © 2014 Wedn.Net. All Rights Reserved.
// +----------------------------------------------------------------------
// | Summary : Link（实体类）
// +----------------------------------------------------------------------
// | Author  : iceStone <ice@wedn.net>
// +----------------------------------------------------------------------

namespace Micua.Domain.Model
{
    using System.Collections.Generic;

    /// <summary>
    /// Link（实体类）
    /// </summary>
    public partial class Link : EntityBase<int>
    {

        #region Constructors
        /// <summary>
        /// 创建一个Link实例对象
        /// </summary>
        public Link()
        {

            this.Name = string.Empty;

            this.Url = string.Empty;

            this.Image = string.Empty;

            this.Title = string.Empty;

            this.Target = string.Empty;

            this.Relation = string.Empty;

            this.Modified = System.DateTime.Now;

            this.Children = new List<Link>();

        }
        #endregion

        #region Simple Properties

        public override int Id { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public string Image { get; set; }

        public string Title { get; set; }

        public string Target { get; set; }

        public string Relation { get; set; }

        public LinkType Type { get; set; }

        public bool Visible { get; set; }

        public System.DateTime Modified { get; set; }

        public short Sort { get; set; }

        public int BlogId { get; set; }

        public int ParentId { get; set; }

        #endregion

        #region Navigation Properties

        public virtual Blog Blog { get; set; }

        public virtual ICollection<Link> Children { get; set; }

        public virtual Link Parent { get; set; }

        #endregion

    }
}
