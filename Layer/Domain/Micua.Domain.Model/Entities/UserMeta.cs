// +----------------------------------------------------------------------
// | Micua [ DO IT BATTER ! ]
// +----------------------------------------------------------------------
// | Copyright © 2014 Wedn.Net. All Rights Reserved.
// +----------------------------------------------------------------------
// | Summary : UserMeta（实体类）
// +----------------------------------------------------------------------
// | Author  : iceStone <ice@wedn.net>
// +----------------------------------------------------------------------

namespace Micua.Domain.Model
{
    /// <summary>
    /// UserMeta（实体类）
    /// </summary>
    public partial class UserMeta : EntityBase<int>
    {

        #region Constructors
        /// <summary>
        /// 创建一个UserMeta实例对象
        /// </summary>
        public UserMeta()
        {

            this.Key = string.Empty;

            this.Value = string.Empty;

        }
        #endregion

        #region Simple Properties

        public override int Id { get; set; }

        public int UserId { get; set; }

        public string Key { get; set; }

        public string Value { get; set; }

        #endregion

        #region Navigation Properties

        public virtual User User { get; set; }

        #endregion

    }
}
