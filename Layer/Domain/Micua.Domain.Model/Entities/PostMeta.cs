// +----------------------------------------------------------------------
// | Micua [ DO IT BATTER ! ]
// +----------------------------------------------------------------------
// | Copyright © 2014 Wedn.Net. All Rights Reserved.
// +----------------------------------------------------------------------
// | Summary : PostMeta（实体类）
// +----------------------------------------------------------------------
// | Author  : iceStone <ice@wedn.net>
// +----------------------------------------------------------------------

namespace Micua.Domain.Model
{
    /// <summary>
    /// PostMeta（实体类）
    /// </summary>
    public partial class PostMeta : EntityBase<int>
    {

        #region Constructors
        /// <summary>
        /// 创建一个PostMeta实例对象
        /// </summary>
        public PostMeta()
        {

            this.Key = string.Empty;

            this.Value = string.Empty;

        }
        #endregion

        #region Simple Properties

        public override int Id { get; set; }

        public int PostId { get; set; }

        public string Key { get; set; }

        public string Value { get; set; }

        #endregion

        #region Navigation Properties

        public virtual Post Post { get; set; }

        #endregion

    }
}
