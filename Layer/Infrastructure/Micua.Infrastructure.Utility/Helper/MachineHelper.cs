
using System;
using System.Web;
using System.Web.Hosting;
namespace Micua.Infrastructure.Utility
{
    public static class MachineHelper
    {
        #region 返回与虚拟路径相对应的物理路径 +static string MapPath(string virtualPath)
        /// <summary>
        /// 返回与虚拟路径相对应的物理路径
        /// </summary>
        /// <remarks>
        ///  2014年01月09日 11:57 Created By iceStone
        /// </remarks>
        /// <param name="virtualPath">虚拟路径</param>
        /// <returns>物理路径</returns>
        public static string MapPath(string virtualPath)
        {
            var context = HttpContext.Current;
            if (context != null)
            {
                return context.Server.MapPath(virtualPath);
            }
            var path = HostingEnvironment.MapPath(virtualPath);
            if (!string.IsNullOrEmpty(path))
                return path;
            return AppDomain.CurrentDomain.BaseDirectory + "\\" + virtualPath.TrimStart('~', '/').Replace('/', '\\');
        }
        #endregion



        private static AspNetHostingPermissionLevel? _trustLevel;
        /// <summary>
        /// Finds the trust level of the running application (http://blogs.msdn.com/dmitryr/archive/2007/01/23/finding-out-the-current-trust-level-in-asp-net.aspx)
        /// </summary>
        /// <returns>The current trust level.</returns>
        public static AspNetHostingPermissionLevel TrustLevel
        {
            get
            {
                if (!_trustLevel.HasValue)
                {
                    //set minimum
                    _trustLevel = AspNetHostingPermissionLevel.None;

                    //determine maximum
                    foreach (AspNetHostingPermissionLevel trustLevel in
                            new AspNetHostingPermissionLevel[] {
                                AspNetHostingPermissionLevel.Unrestricted,
                                AspNetHostingPermissionLevel.High,
                                AspNetHostingPermissionLevel.Medium,
                                AspNetHostingPermissionLevel.Low,
                                AspNetHostingPermissionLevel.Minimal 
                            })
                    {
                        try
                        {
                            new AspNetHostingPermission(trustLevel).Demand();
                            _trustLevel = trustLevel;
                            break; //we've set the highest permission we can
                        }
                        catch (System.Security.SecurityException)
                        {
                            continue;
                        }
                    }
                }
                return _trustLevel.Value;
            }
        }
    }
}
