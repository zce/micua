// +----------------------------------------------------------------------
// | Micua [ DO IT BATTER ! ]
// +----------------------------------------------------------------------
// | Copyright © 2014 Wedn.Net. All Rights Reserved.
// +----------------------------------------------------------------------
// | Summary : 服务实现 <Repository>
// +----------------------------------------------------------------------
// | Author  : iceStone <ice@wedn.net>
// +----------------------------------------------------------------------
namespace Micua.Domain.Service
{
    using Micua.Domain.Model;
    using Micua.Domain.Repository;

    public partial class BlogService : ServiceBase<int, Blog>, IBlogService
    {
        protected override IRepository<int, Blog> Repository
        {
            get { return DbSession.Instance.BlogRepository; }
        }
    }

    public partial class BlogMetaService : ServiceBase<int, BlogMeta>, IBlogMetaService
    {
        protected override IRepository<int, BlogMeta> Repository
        {
            get { return DbSession.Instance.BlogMetaRepository; }
        }
    }

    public partial class CommentService : ServiceBase<int, Comment>, ICommentService
    {
        protected override IRepository<int, Comment> Repository
        {
            get { return DbSession.Instance.CommentRepository; }
        }
    }

    public partial class CommentMetaService : ServiceBase<int, CommentMeta>, ICommentMetaService
    {
        protected override IRepository<int, CommentMeta> Repository
        {
            get { return DbSession.Instance.CommentMetaRepository; }
        }
    }

    public partial class LinkService : ServiceBase<int, Link>, ILinkService
    {
        protected override IRepository<int, Link> Repository
        {
            get { return DbSession.Instance.LinkRepository; }
        }
    }

    public partial class OptionService : ServiceBase<int, Option>, IOptionService
    {
        protected override IRepository<int, Option> Repository
        {
            get { return DbSession.Instance.OptionRepository; }
        }
    }

    public partial class PostService : ServiceBase<int, Post>, IPostService
    {
        protected override IRepository<int, Post> Repository
        {
            get { return DbSession.Instance.PostRepository; }
        }
    }

    public partial class PostMetaService : ServiceBase<int, PostMeta>, IPostMetaService
    {
        protected override IRepository<int, PostMeta> Repository
        {
            get { return DbSession.Instance.PostMetaRepository; }
        }
    }

    public partial class TermService : ServiceBase<int, Term>, ITermService
    {
        protected override IRepository<int, Term> Repository
        {
            get { return DbSession.Instance.TermRepository; }
        }
    }

    public partial class TermRelationService : ServiceBase<int, TermRelation>, ITermRelationService
    {
        protected override IRepository<int, TermRelation> Repository
        {
            get { return DbSession.Instance.TermRelationRepository; }
        }
    }

    public partial class UserService : ServiceBase<int, User>, IUserService
    {
        protected override IRepository<int, User> Repository
        {
            get { return DbSession.Instance.UserRepository; }
        }
    }

    public partial class UserMetaService : ServiceBase<int, UserMeta>, IUserMetaService
    {
        protected override IRepository<int, UserMeta> Repository
        {
            get { return DbSession.Instance.UserMetaRepository; }
        }
    }

}
