// +----------------------------------------------------------------------
// | Micua [ DO IT BATTER ! ]
// +----------------------------------------------------------------------
// | Copyright © 2014 Wedn.Net. All Rights Reserved.
// +----------------------------------------------------------------------
// | Summary : 基于EF的仓储实现 <Repository>
// +----------------------------------------------------------------------
// | Author  : iceStone <ice@wedn.net>
// +----------------------------------------------------------------------
namespace Micua.Domain.Repository
{
    using Micua.Domain.Model;

    public class BlogRepository : RepositoryBase<int, Blog>, IBlogRepository { internal BlogRepository() { } }

    public class BlogMetaRepository : RepositoryBase<int, BlogMeta>, IBlogMetaRepository { internal BlogMetaRepository() { } }

    public class CommentRepository : RepositoryBase<int, Comment>, ICommentRepository { internal CommentRepository() { } }

    public class CommentMetaRepository : RepositoryBase<int, CommentMeta>, ICommentMetaRepository { internal CommentMetaRepository() { } }

    public class LinkRepository : RepositoryBase<int, Link>, ILinkRepository { internal LinkRepository() { } }

    public class OptionRepository : RepositoryBase<int, Option>, IOptionRepository { internal OptionRepository() { } }

    public class PostRepository : RepositoryBase<int, Post>, IPostRepository { internal PostRepository() { } }

    public class PostMetaRepository : RepositoryBase<int, PostMeta>, IPostMetaRepository { internal PostMetaRepository() { } }

    public class TermRepository : RepositoryBase<int, Term>, ITermRepository { internal TermRepository() { } }

    public class TermRelationRepository : RepositoryBase<int, TermRelation>, ITermRelationRepository { internal TermRelationRepository() { } }

    public class UserRepository : RepositoryBase<int, User>, IUserRepository { internal UserRepository() { } }

    public class UserMetaRepository : RepositoryBase<int, UserMeta>, IUserMetaRepository { internal UserMetaRepository() { } }

}