// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ClearObjectMemberIntelliSence.cs" company="Wedn.Net">
//   Copyright © 2014 Wedn.Net. All Rights Reserved.
// </copyright>
// <summary>
//   清除由Object派生成员的IntelliSence
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Micua.Infrastructure.Utility
{
    using System;
    using System.ComponentModel;

    /// <summary>
    /// 清除由Object派生成员的IntelliSence
    /// </summary>
    public abstract class ClearObjectMemberIntelliSence
    {
        #region 重写从Object继承的成员为IntelliSence不可见
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override string ToString() { return null; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Equals(object obj) { return base.Equals(obj); }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode() { return base.GetHashCode(); }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public new Type GetType() { return base.GetType(); }
        #endregion
    }
}
