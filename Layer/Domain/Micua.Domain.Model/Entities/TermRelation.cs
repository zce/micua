

// +----------------------------------------------------------------------
// | Micua [ DO IT BATTER ! ]
// +----------------------------------------------------------------------
// | Copyright © 2014 Wedn.Net. All Rights Reserved.
// +----------------------------------------------------------------------
// | Summary : TermRelation（实体类）
// +----------------------------------------------------------------------
// | Author  : iceStone <ice@wedn.net>
// +----------------------------------------------------------------------

namespace Micua.Domain.Model
{
    /// <summary>
    /// TermRelation（实体类）
    /// </summary>
    public partial class TermRelation : EntityBase<int>
    {

        #region Constructors
        /// <summary>
        /// 创建一个TermRelation实例对象
        /// </summary>
        public TermRelation()
        {

        }
        #endregion

        #region Simple Properties

        public override int Id { get; set; }

        public int ObjectId { get; set; }

        public TermRelationType Type { get; set; }

        public int TermId { get; set; }

        public short Sort { get; set; }

        #endregion

        #region Navigation Properties

        public virtual Term Term { get; set; }

        #endregion

    }
}
