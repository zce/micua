namespace Micua.Domain.Model
{
    using System.Collections.Generic;

    public class SampleData : System.Data.Entity.DropCreateDatabaseIfModelChanges<MicuaContext>
    {
        IEnumerable<Blog> blogs;
        IEnumerable<BlogMeta> blogmetas;
        IEnumerable<Comment> comments;
        IEnumerable<CommentMeta> commentmetas;
        IEnumerable<Link> links;
        IEnumerable<Option> options;
        IEnumerable<Post> posts;
        IEnumerable<PostMeta> postmetas;
        IEnumerable<Term> terms;
        IEnumerable<TermRelation> termrelations;
        IEnumerable<User> users;
        IEnumerable<UserMeta> usermetas;
        protected override void Seed(MicuaContext context)
        {
            // initialize Blog table
            InitBlogs(context);
            // initialize BlogMeta table
            InitBlogMetas(context);
            // initialize Comment table
            InitComments(context);
            // initialize CommentMeta table
            InitCommentMetas(context);
            // initialize Link table
            InitLinks(context);
            // initialize Option table
            InitOptions(context);
            // initialize Post table
            InitPosts(context);
            // initialize PostMeta table
            InitPostMetas(context);
            // initialize Term table
            InitTerms(context);
            // initialize TermRelation table
            InitTermRelations(context);
            // initialize User table
            InitUsers(context);
            // initialize UserMeta table
            InitUserMetas(context);

            context.SaveChanges();
        }

        /// <summary>
        /// initialize Blog table
        /// </summary>
        /// <param name="context">database context</param>
        private void InitBlogs(MicuaContext context)
        {
            blogs = new List<Blog> { };
            context.Blogs.AddRange(blogs);
        }
        /// <summary>
        /// initialize BlogMeta table
        /// </summary>
        /// <param name="context">database context</param>
        private void InitBlogMetas(MicuaContext context)
        {
            blogmetas = new List<BlogMeta> { };
            context.BlogMetas.AddRange(blogmetas);
        }
        /// <summary>
        /// initialize Comment table
        /// </summary>
        /// <param name="context">database context</param>
        private void InitComments(MicuaContext context)
        {
            comments = new List<Comment> { };
            context.Comments.AddRange(comments);
        }
        /// <summary>
        /// initialize CommentMeta table
        /// </summary>
        /// <param name="context">database context</param>
        private void InitCommentMetas(MicuaContext context)
        {
            commentmetas = new List<CommentMeta> { };
            context.CommentMetas.AddRange(commentmetas);
        }
        /// <summary>
        /// initialize Link table
        /// </summary>
        /// <param name="context">database context</param>
        private void InitLinks(MicuaContext context)
        {
            links = new List<Link> { };
            context.Links.AddRange(links);
        }
        /// <summary>
        /// initialize Option table
        /// </summary>
        /// <param name="context">database context</param>
        private void InitOptions(MicuaContext context)
        {
            options = new List<Option> { };
            context.Options.AddRange(options);
        }
        /// <summary>
        /// initialize Post table
        /// </summary>
        /// <param name="context">database context</param>
        private void InitPosts(MicuaContext context)
        {
            posts = new List<Post> { };
            context.Posts.AddRange(posts);
        }
        /// <summary>
        /// initialize PostMeta table
        /// </summary>
        /// <param name="context">database context</param>
        private void InitPostMetas(MicuaContext context)
        {
            postmetas = new List<PostMeta> { };
            context.PostMetas.AddRange(postmetas);
        }
        /// <summary>
        /// initialize Term table
        /// </summary>
        /// <param name="context">database context</param>
        private void InitTerms(MicuaContext context)
        {
            terms = new List<Term> { };
            context.Terms.AddRange(terms);
        }
        /// <summary>
        /// initialize TermRelation table
        /// </summary>
        /// <param name="context">database context</param>
        private void InitTermRelations(MicuaContext context)
        {
            termrelations = new List<TermRelation> { };
            context.TermRelations.AddRange(termrelations);
        }
        /// <summary>
        /// initialize User table
        /// </summary>
        /// <param name="context">database context</param>
        private void InitUsers(MicuaContext context)
        {
            users = new List<User> { };
            context.Users.AddRange(users);
        }
        /// <summary>
        /// initialize UserMeta table
        /// </summary>
        /// <param name="context">database context</param>
        private void InitUserMetas(MicuaContext context)
        {
            usermetas = new List<UserMeta> { };
            context.UserMetas.AddRange(usermetas);
        }

    }
}