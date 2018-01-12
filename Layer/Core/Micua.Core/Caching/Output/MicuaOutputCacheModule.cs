using System.Web;
using System.Web.Caching;

namespace Micua.Core.Caching
{
    public class MicuaOutputCacheModule : IHttpModule
    {
        public const bool EnableOutputCache = true;

        public void Init(HttpApplication application)
        {
            if (EnableOutputCache)
            {
                application.ResolveRequestCache += application_ResolveRequestCache;
                application.UpdateRequestCache += application_UpdateRequestCache;
            }
        }

        /// <summary>
        /// 在 ASP.NET 完成授权事件以使缓存模块从缓存中为请求提供服务后发生，从而绕过事件处理程序（例如某个页或 XML Web services）的执行。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void application_ResolveRequestCache(object sender, System.EventArgs e)
        {
            var application = sender as HttpApplication;
            if (application == null) return;
            HttpResponse response = application.Response;

        }
        /// <summary>
        /// 当 ASP.NET 执行完事件处理程序以使缓存模块存储将用于从缓存为后续请求提供服务的响应时发生。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void application_UpdateRequestCache(object sender, System.EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}