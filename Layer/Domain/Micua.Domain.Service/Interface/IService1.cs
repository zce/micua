// +----------------------------------------------------------------------
// | Micua [ DO IT BATTER ! ]
// +----------------------------------------------------------------------
// | Copyright © 2014 Wedn.Net. All Rights Reserved.
// +----------------------------------------------------------------------
// | Summary : 服务接口实现 <IService>
// +----------------------------------------------------------------------
// | Author  : iceStone <ice@wedn.net>
// +----------------------------------------------------------------------
namespace Micua.Domain.Service
{
    using Micua.Domain.Model;


    /// <summary>
    /// Blog表 操作类接口 IBlogRepository
    /// </summary>
    /// <remarks>
    ///  2013-11-18 19:06 Created By iceStone
    ///  2014-08-29 12:21 Modified By iceStone
    /// </remarks>
    public partial interface IBlogService : IService<int, Blog> { }


    /// <summary>
    /// BlogMeta表 操作类接口 IBlogMetaRepository
    /// </summary>
    /// <remarks>
    ///  2013-11-18 19:06 Created By iceStone
    ///  2014-08-29 12:21 Modified By iceStone
    /// </remarks>
    public partial interface IBlogMetaService : IService<int, BlogMeta> { }


    /// <summary>
    /// Comment表 操作类接口 ICommentRepository
    /// </summary>
    /// <remarks>
    ///  2013-11-18 19:06 Created By iceStone
    ///  2014-08-29 12:21 Modified By iceStone
    /// </remarks>
    public partial interface ICommentService : IService<int, Comment> { }


    /// <summary>
    /// CommentMeta表 操作类接口 ICommentMetaRepository
    /// </summary>
    /// <remarks>
    ///  2013-11-18 19:06 Created By iceStone
    ///  2014-08-29 12:21 Modified By iceStone
    /// </remarks>
    public partial interface ICommentMetaService : IService<int, CommentMeta> { }


    /// <summary>
    /// Link表 操作类接口 ILinkRepository
    /// </summary>
    /// <remarks>
    ///  2013-11-18 19:06 Created By iceStone
    ///  2014-08-29 12:21 Modified By iceStone
    /// </remarks>
    public partial interface ILinkService : IService<int, Link> { }


    /// <summary>
    /// Option表 操作类接口 IOptionRepository
    /// </summary>
    /// <remarks>
    ///  2013-11-18 19:06 Created By iceStone
    ///  2014-08-29 12:21 Modified By iceStone
    /// </remarks>
    public partial interface IOptionService : IService<int, Option> { }


    /// <summary>
    /// Post表 操作类接口 IPostRepository
    /// </summary>
    /// <remarks>
    ///  2013-11-18 19:06 Created By iceStone
    ///  2014-08-29 12:21 Modified By iceStone
    /// </remarks>
    public partial interface IPostService : IService<int, Post> { }


    /// <summary>
    /// PostMeta表 操作类接口 IPostMetaRepository
    /// </summary>
    /// <remarks>
    ///  2013-11-18 19:06 Created By iceStone
    ///  2014-08-29 12:21 Modified By iceStone
    /// </remarks>
    public partial interface IPostMetaService : IService<int, PostMeta> { }


    /// <summary>
    /// Term表 操作类接口 ITermRepository
    /// </summary>
    /// <remarks>
    ///  2013-11-18 19:06 Created By iceStone
    ///  2014-08-29 12:21 Modified By iceStone
    /// </remarks>
    public partial interface ITermService : IService<int, Term> { }


    /// <summary>
    /// TermRelation表 操作类接口 ITermRelationRepository
    /// </summary>
    /// <remarks>
    ///  2013-11-18 19:06 Created By iceStone
    ///  2014-08-29 12:21 Modified By iceStone
    /// </remarks>
    public partial interface ITermRelationService : IService<int, TermRelation> { }


    /// <summary>
    /// User表 操作类接口 IUserRepository
    /// </summary>
    /// <remarks>
    ///  2013-11-18 19:06 Created By iceStone
    ///  2014-08-29 12:21 Modified By iceStone
    /// </remarks>
    public partial interface IUserService : IService<int, User> { }


    /// <summary>
    /// UserMeta表 操作类接口 IUserMetaRepository
    /// </summary>
    /// <remarks>
    ///  2013-11-18 19:06 Created By iceStone
    ///  2014-08-29 12:21 Modified By iceStone
    /// </remarks>
    public partial interface IUserMetaService : IService<int, UserMeta> { }
}
