// +----------------------------------------------------------------------
// | Micua [ DO IT BATTER ! ]
// +----------------------------------------------------------------------
// | Copyright © 2014 Wedn.Net. All Rights Reserved.
// +----------------------------------------------------------------------
// | Summary : CommentMeta（实体类）
// +----------------------------------------------------------------------
// | Author  : iceStone <ice@wedn.net>
// +----------------------------------------------------------------------

namespace Micua.Domain.Model
{
    /// <summary>
    /// CommentMeta（实体类）
    /// </summary>
    public partial class CommentMeta : EntityBase<int>
    {

        #region Constructors
        /// <summary>
        /// 创建一个CommentMeta实例对象
        /// </summary>
        public CommentMeta()
        {

            this.Key = string.Empty;

            this.Value = string.Empty;

        }
        #endregion

        #region Simple Properties

        public override int Id { get; set; }

        public int CommentId { get; set; }

        public string Key { get; set; }

        public string Value { get; set; }

        #endregion

        #region Navigation Properties

        public virtual Comment Comment { get; set; }

        #endregion

    }
}
