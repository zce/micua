// +----------------------------------------------------------------------
// | Micua [ DO IT BATTER ! ]
// +----------------------------------------------------------------------
// | Copyright © 2014 Wedn.Net. All Rights Reserved.
// +----------------------------------------------------------------------
// | Summary : BlogMeta（实体类）
// +----------------------------------------------------------------------
// | Author  : iceStone <ice@wedn.net>
// +----------------------------------------------------------------------

namespace Micua.Domain.Model
{
    /// <summary>
    /// BlogMeta（实体类）
    /// </summary>
    public partial class BlogMeta : EntityBase<int>
    {

        #region Constructors
        /// <summary>
        /// 创建一个BlogMeta实例对象
        /// </summary>
        public BlogMeta()
        {

            this.Key = string.Empty;

            this.Value = string.Empty;

        }
        #endregion

        #region Simple Properties

        public override int Id { get; set; }

        public int BlogId { get; set; }

        public string Key { get; set; }

        public string Value { get; set; }

        #endregion

        #region Navigation Properties

        public virtual Blog Blog { get; set; }

        #endregion

    }
}
