// +----------------------------------------------------------------------
// | Micua [ DO IT BATTER ! ]
// +----------------------------------------------------------------------
// | Copyright © 2014 Wedn.Net. All Rights Reserved.
// +----------------------------------------------------------------------
// | Summary : 仓储接口实现 <IRepository>
// +----------------------------------------------------------------------
// | Author  : iceStone <ice@wedn.net>
// +----------------------------------------------------------------------
namespace Micua.Domain.Repository
{
    using Micua.Domain.Model;


    /// <summary>
    /// Blog表 操作类接口 IBlogRepository
    /// </summary>
    /// <remarks>
    ///  2013-11-18 19:06 Created By iceStone
    ///  2014-08-29 12:21 Modified By iceStone
    /// </remarks>
    public partial interface IBlogRepository : IRepository<int, Blog> { }


    /// <summary>
    /// BlogMeta表 操作类接口 IBlogMetaRepository
    /// </summary>
    /// <remarks>
    ///  2013-11-18 19:06 Created By iceStone
    ///  2014-08-29 12:21 Modified By iceStone
    /// </remarks>
    public partial interface IBlogMetaRepository : IRepository<int, BlogMeta> { }


    /// <summary>
    /// Comment表 操作类接口 ICommentRepository
    /// </summary>
    /// <remarks>
    ///  2013-11-18 19:06 Created By iceStone
    ///  2014-08-29 12:21 Modified By iceStone
    /// </remarks>
    public partial interface ICommentRepository : IRepository<int, Comment> { }


    /// <summary>
    /// CommentMeta表 操作类接口 ICommentMetaRepository
    /// </summary>
    /// <remarks>
    ///  2013-11-18 19:06 Created By iceStone
    ///  2014-08-29 12:21 Modified By iceStone
    /// </remarks>
    public partial interface ICommentMetaRepository : IRepository<int, CommentMeta> { }


    /// <summary>
    /// Link表 操作类接口 ILinkRepository
    /// </summary>
    /// <remarks>
    ///  2013-11-18 19:06 Created By iceStone
    ///  2014-08-29 12:21 Modified By iceStone
    /// </remarks>
    public partial interface ILinkRepository : IRepository<int, Link> { }


    /// <summary>
    /// Option表 操作类接口 IOptionRepository
    /// </summary>
    /// <remarks>
    ///  2013-11-18 19:06 Created By iceStone
    ///  2014-08-29 12:21 Modified By iceStone
    /// </remarks>
    public partial interface IOptionRepository : IRepository<int, Option> { }


    /// <summary>
    /// Post表 操作类接口 IPostRepository
    /// </summary>
    /// <remarks>
    ///  2013-11-18 19:06 Created By iceStone
    ///  2014-08-29 12:21 Modified By iceStone
    /// </remarks>
    public partial interface IPostRepository : IRepository<int, Post> { }


    /// <summary>
    /// PostMeta表 操作类接口 IPostMetaRepository
    /// </summary>
    /// <remarks>
    ///  2013-11-18 19:06 Created By iceStone
    ///  2014-08-29 12:21 Modified By iceStone
    /// </remarks>
    public partial interface IPostMetaRepository : IRepository<int, PostMeta> { }


    /// <summary>
    /// Term表 操作类接口 ITermRepository
    /// </summary>
    /// <remarks>
    ///  2013-11-18 19:06 Created By iceStone
    ///  2014-08-29 12:21 Modified By iceStone
    /// </remarks>
    public partial interface ITermRepository : IRepository<int, Term> { }


    /// <summary>
    /// TermRelation表 操作类接口 ITermRelationRepository
    /// </summary>
    /// <remarks>
    ///  2013-11-18 19:06 Created By iceStone
    ///  2014-08-29 12:21 Modified By iceStone
    /// </remarks>
    public partial interface ITermRelationRepository : IRepository<int, TermRelation> { }


    /// <summary>
    /// User表 操作类接口 IUserRepository
    /// </summary>
    /// <remarks>
    ///  2013-11-18 19:06 Created By iceStone
    ///  2014-08-29 12:21 Modified By iceStone
    /// </remarks>
    public partial interface IUserRepository : IRepository<int, User> { }


    /// <summary>
    /// UserMeta表 操作类接口 IUserMetaRepository
    /// </summary>
    /// <remarks>
    ///  2013-11-18 19:06 Created By iceStone
    ///  2014-08-29 12:21 Modified By iceStone
    /// </remarks>
    public partial interface IUserMetaRepository : IRepository<int, UserMeta> { }
}
