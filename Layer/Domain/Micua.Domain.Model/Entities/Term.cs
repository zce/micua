

// +----------------------------------------------------------------------
// | Micua [ DO IT BATTER ! ]
// +----------------------------------------------------------------------
// | Copyright © 2014 Wedn.Net. All Rights Reserved.
// +----------------------------------------------------------------------
// | Summary : Term（实体类）
// +----------------------------------------------------------------------
// | Author  : iceStone <ice@wedn.net>
// +----------------------------------------------------------------------

namespace Micua.Domain.Model
{
    using System.Collections.Generic;

    /// <summary>
    /// Term（实体类）
    /// </summary>
    public partial class Term : EntityBase<int>
    {

        #region Constructors
        /// <summary>
        /// 创建一个Term实例对象
        /// </summary>
        public Term()
        {

            this.Slug = string.Empty;

            this.Name = string.Empty;

            this.Description = string.Empty;

            this.Children = new List<Term>();

            this.Relations = new List<TermRelation>();

        }
        #endregion

        #region Simple Properties

        public override int Id { get; set; }

        public string Slug { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public TermType Type { get; set; }

        public short Sort { get; set; }

        public int Count { get; set; }

        public int? ParentId { get; set; }

        #endregion

        #region Navigation Properties

        public virtual ICollection<Term> Children { get; set; }

        public virtual Term Parent { get; set; }

        public virtual ICollection<TermRelation> Relations { get; set; }

        #endregion

    }
}
