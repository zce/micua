// +----------------------------------------------------------------------
// | Micua [ DO IT BATTER ! ]
// +----------------------------------------------------------------------
// | Copyright © 2014 Wedn.Net. All Rights Reserved.
// +----------------------------------------------------------------------
// | Summary : Option（实体类）
// +----------------------------------------------------------------------
// | Author  : iceStone <ice@wedn.net>
// +----------------------------------------------------------------------

namespace Micua.Domain.Model
{
    /// <summary>
    /// Option（实体类）
    /// </summary>
    public partial class Option : EntityBase<int>
    {

        #region Constructors
        /// <summary>
        /// 创建一个Option实例对象
        /// </summary>
        public Option()
        {

            this.Name = string.Empty;

            this.Value = string.Empty;

        }
        #endregion

        #region Simple Properties

        public override int Id { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }

        public bool Enabled { get; set; }

        #endregion

        #region Navigation Properties

        #endregion

    }
}
