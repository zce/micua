using System.Web;
using System.Web.Routing;

namespace Micua.Core.IoC.Unity
{
    /// <summary>
    /// 路由处理程序
    /// </summary>
    public class MicuaRouteHandler : IRouteHandler
    {
        #region 提供处理请求的对象 +IHttpHandler GetHttpHandler(RequestContext requestContext)
        /// <summary>
        /// 提供处理请求的对象。
        /// </summary>
        /// <param name="requestContext">一个对象，封装有关请求的信息。</param>
        /// <returns>一个处理请求的对象。</returns>
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return new MicuaHandler(requestContext);
        } 
        #endregion
    }
}