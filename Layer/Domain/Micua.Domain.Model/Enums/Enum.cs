namespace Micua.Domain.Model
{
    #region 站点状态
    /// <summary>
    /// 站点状态
    /// </summary>
    /// <remarks>
    ///  2013-11-23 14:28 Created By iceStone
    /// </remarks>
    public enum SiteStatus : byte
    {
        /// <summary>
        /// 默认状态
        /// </summary>
        Default = 0,
        /// <summary>
        /// 开放
        /// </summary>
        Normal = 0,
        /// <summary>
        /// 关闭
        /// </summary>
        Closed = 1,
    }
    #endregion

    #region 博客状态
    /// <summary>
    /// 博客状态
    /// </summary>
    /// <remarks>
    ///  2013-11-23 14:28 Created By iceStone
    /// </remarks>
    public enum BlogStatus : byte
    {
        /// <summary>
        /// 默认状态
        /// </summary>
        Default = 0,
        /// <summary>
        /// 正常
        /// </summary>
        Normal = 0,
        /// <summary>
        /// 等待审核
        /// </summary>
        Awaiting = 1,
        /// <summary>
        /// 禁止的
        /// </summary>
        Closed = 2,
    }
    #endregion

    #region 显示类型
    /// <summary>
    /// 文章显示类型
    /// </summary>
    /// <remarks>
    ///  2013-11-23 14:28 Created By iceStone
    /// </remarks>
    public enum PostShowType : byte
    {
        /// <summary>
        /// 默认状态
        /// </summary>
        Default = 0,
        /// <summary>
        /// 摘要
        /// </summary>
        Summary = 0,
        /// <summary>
        /// 完全
        /// </summary>
        Full = 1,
    }
    /// <summary>
    /// RSS显示类型
    /// </summary>
    /// <remarks>
    ///  2013-11-23 14:28 Created By iceStone
    /// </remarks>
    public enum RssShowType : byte
    {
        /// <summary>
        /// 默认状态
        /// </summary>
        Default = 0,
        /// <summary>
        /// 摘要
        /// </summary>
        Summary = 0,
        /// <summary>
        /// 完全
        /// </summary>
        Full = 1,
    }
    #endregion

    #region 页面类型
    /// <summary>
    /// 页面类型
    /// </summary>
    /// <remarks>
    ///  2013-11-23 14:28 Created By iceStone
    /// </remarks>
    public enum PageType : byte
    {
        /// <summary>
        /// 默认页
        /// </summary>
        Default = 0,
        /// <summary>
        /// 文章页
        /// </summary>
        Post = 1,
        /// <summary>
        /// 类别页
        /// </summary>
        Category = 2,
        /// <summary>
        /// 标签页
        /// </summary>
        Tag = 3,
        /// <summary>
        /// 归档页
        /// </summary>
        Archive = 4,
        /// <summary>
        /// 分页
        /// </summary>
        Page = 5
    }
    #endregion

    #region 发表物类型
    /// <summary>
    /// 发表物类型
    /// </summary>
    /// <remarks>
    ///  2013-11-23 14:28 Created By iceStone
    /// </remarks>
    public enum PostType : byte
    {
        /// <summary>
        /// 默认状态
        /// </summary>
        Default = 0,
        /// <summary>
        /// 文章
        /// </summary>
        Article = 0,
        /// <summary>
        /// 页面
        /// </summary>
        Page = 1,
        /// <summary>
        /// 媒体
        /// </summary>
        Media = 2,
    }
    #endregion

    #region 发表物状态
    /// <summary>
    /// 发表物状态
    /// </summary>
    /// <remarks>
    ///  2013-11-23 14:28 Created By iceStone
    /// </remarks>
    public enum PostStatus : byte
    {
        /// <summary>
        /// 默认状态
        /// </summary>
        Default = 0,
        /// <summary>
        /// 公开发布
        /// </summary>
        Public = 0,
        /// <summary>
        /// 加密
        /// </summary>
        Encrypt = 1,
        /// <summary>
        /// 私有
        /// </summary>
        Private = 2,
        /// <summary>
        /// 继承的副本
        /// </summary>
        Revision = 3,
        /// <summary>
        /// 回收站
        /// </summary>
        Recycle = 4,
        /// <summary>
        /// 自动草稿
        /// </summary>
        AutoDraft = 5,
        /// <summary>
        /// 草稿
        /// </summary>
        Draft = 6,
    }
    #endregion

    #region 发表物评论状态
    /// <summary>
    /// 发表物评论状态
    /// </summary>
    /// <remarks>
    ///  2013-11-23 14:28 Created By iceStone
    /// </remarks>
    public enum PostCommentStatus : byte
    {
        /// <summary>
        /// 默认状态
        /// </summary>
        Default = 0,
        /// <summary>
        /// 所有人可评论
        /// </summary>
        Open = 0,
        /// <summary>
        /// 只有注册用户可评论
        /// </summary>
        JustRegistered = 1,
        /// <summary>
        /// 不允许评论
        /// </summary>
        Close = 2
    }
    #endregion

    #region 发表物Ping状态
    /// <summary>
    /// 发表物Ping状态
    /// </summary>
    /// <remarks>
    ///  2013-11-23 14:28 Created By iceStone
    /// </remarks>
    public enum PostPingStatus : byte
    {
        /// <summary>
        /// 默认状态
        /// </summary>
        Default = 0,
        /// <summary>
        /// 所有人Ping
        /// </summary>
        Open = 0,
        /// <summary>
        /// 不允许Ping
        /// </summary>
        Close = 1
    }
    #endregion

    #region 用户状态
    /// <summary>
    /// 用户状态
    /// </summary>
    /// <remarks>
    ///  2013-11-23 14:28 Created By iceStone
    /// </remarks>
    public enum UserStatus : byte
    {
        /// <summary>
        /// 默认状态
        /// </summary>
        Default = 0,
        /// <summary>
        /// 正常
        /// </summary>
        Normal = 0,
        /// <summary>
        /// 等待审核
        /// </summary>
        Awaiting = 1,
        /// <summary>
        /// 禁止的
        /// </summary>
        Ban = 2,
    }
    #endregion

    #region 评论状态
    /// <summary>
    /// 评论状态
    /// </summary>
    /// <remarks>
    ///  2013-11-23 14:28 Created By iceStone
    /// </remarks>
    public enum CommentStatus : byte
    {
        /// <summary>
        /// 默认状态
        /// </summary>
        Default = 0,
        /// <summary>
        /// 等待审核
        /// </summary>
        Awaiting = 0,
        /// <summary>
        /// 审核通过
        /// </summary>
        Approved = 1,
        /// <summary>
        /// 回收站
        /// </summary>
        Recycle = 2,
        /// <summary>
        /// 垃圾评论
        /// </summary>
        Spam = 3,
    }
    #endregion

    #region 链接类型
    /// <summary>
    /// 链接类型
    /// </summary>
    /// <remarks>
    ///  2013-11-23 14:28 Created By iceStone
    /// </remarks>
    public enum LinkType : byte
    {
        /// <summary>
        /// 默认状态
        /// </summary>
        Default = 0,
        /// <summary>
        /// 友情链接
        /// </summary>
        Blogroll = 0,
        /// <summary>
        /// 导航菜单
        /// </summary>
        Navigation = 1,
        /// <summary>
        /// 脚注链接
        /// </summary>
        Footer = 2
    }
    #endregion

    #region 标签类型
    /// <summary>
    /// 标签类型
    /// </summary>
    /// <remarks>
    ///  2013-11-23 14:28 Created By iceStone
    /// </remarks>
    public enum TermType : byte
    {
        /// <summary>
        /// 默认状态
        /// </summary>
        Default = 2,
        /// <summary>
        /// 博客标签
        /// </summary>
        BlogTag = 0,
        /// <summary>
        /// 博客分类
        /// </summary>
        BlogCategory = 1,
        /// <summary>
        /// 文章Tag
        /// </summary>
        PostTag = 2,
        /// <summary>
        /// 文章类别
        /// </summary>
        PostCategory = 3,
        /// <summary>
        /// 用户标签
        /// </summary>
        UserTag = 4,
        /// <summary>
        /// 用户分类
        /// </summary>
        UserCategory = 5,
        /// <summary>
        /// 链接标签
        /// </summary>
        LinkTag = 6,
        /// <summary>
        /// 链接类别
        /// </summary>
        LinkCategory = 7,
        /// <summary>
        /// 评论标签
        /// </summary>
        CommentTag = 8,
        /// <summary>
        /// 评论分类
        /// </summary>
        CommentCategory = 9,
    }
    #endregion

    #region 标签关系类型
    public enum TermRelationType : byte
    {

        /// <summary>
        /// 默认状态
        /// </summary>
        Default = 1,
        /// <summary>
        /// 博客
        /// </summary>
        Blog = 0,
        /// <summary>
        /// 文章
        /// </summary>
        Post = 1,
        /// <summary>
        /// 用户
        /// </summary>
        User = 2,
        /// <summary>
        /// 链接
        /// </summary>
        Link = 3,
        /// <summary>
        /// 评论
        /// </summary>
        Comment = 4,
    }
    #endregion

    #region 用户角色权限
    /// <summary>
    /// 用户角色
    /// </summary>
    /// <remarks>
    ///  2013-11-23 14:28 Created By iceStone
    /// </remarks>
    public enum UserRole : byte
    {
        /// <summary>
        /// 默认状态
        /// </summary>
        Default = 2,
        /// <summary>
        /// 超级管理员
        /// </summary>
        Administrator = 0,
        /// <summary>
        /// 撰写人
        /// </summary>
        Writer = 1,
        /// <summary>
        /// 订阅者
        /// </summary>
        Subscriber = 2,
        /// <summary>
        /// 第三方登陆用户
        /// </summary>
        OAuthUser = 3,
    }
    #endregion

    #region 登录结果
    /// <summary>
    /// 登录结果
    /// </summary>
    /// <remarks>
    ///  2013-11-23 14:28 Created By iceStone
    /// </remarks>
    public enum LoginResult : byte
    {
        /// <summary>
        /// 用户名不存在
        /// </summary>
        NonExistent,
        /// <summary>
        /// 密码错误
        /// </summary>
        PasswordError,
        /// <summary>
        /// 要求用户输入验证码
        /// </summary>
        NeedValidateCode,
        /// <summary>
        /// 用户等待审核
        /// </summary>
        Awaiting,
        /// <summary>
        /// 登录成功
        /// </summary>
        Success,
        /// <summary>
        /// 第三方登陆成功
        /// </summary>
        OAuthSuccess,
        /// <summary>
        /// 第三方登陆失败
        /// </summary>
        OAuthFail
    }
    #endregion

    #region 错误等级
    /// <summary>
    /// 错误等级
    /// </summary>
    /// <remarks>
    ///  2013-11-23 14:28 Created By iceStone
    /// </remarks>
    public enum ErrorLevel : byte
    {
        /// <summary>
        /// 调试错误
        /// </summary>
        Debug,
        /// <summary>
        /// 一般错误
        /// </summary>
        Error,
        /// <summary>
        /// 致命错误
        /// </summary>
        Fatal,
        /// <summary>
        /// 消息级错误
        /// </summary>
        Info,
        /// <summary>
        /// 警告
        /// </summary>
        Warn,
    }
    #endregion
}
