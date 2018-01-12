using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Micua.Domain.Model;

namespace Micua.Domain.Service
{
    /// <summary>
    /// User管理类接口 IUserManager
    /// </summary>
    /// <remarks>
    ///  2013-11-23 22:07 Created By iceStone
    /// </remarks>
    public partial interface IUserService
    {
        /// <summary>
        /// OAuth用户登录
        /// </summary>
        /// <remarks>
        ///  2013-11-23 22:07 Created By iceStone
        /// </remarks>
        /// <param name="uid">用户登录名</param>
        /// <param name="user">当前用户</param>
        /// <returns>登录结果</returns>
        LoginResult OAuthLogin(string uid, out User user);
        /// <summary>
        /// 校验用户登录
        /// </summary>
        /// <remarks>
        ///  2013-11-23 22:07 Created By iceStone
        /// </remarks>
        /// <param name="uid">用户登录名</param>
        /// <param name="pwd">用户密码</param>
        /// <param name="remember">是否记住登录</param>
        /// <param name="user">当前用户</param>
        /// <returns>登录结果</returns>
        LoginResult Login(string uid, string pwd, bool remember, out User user);
        /// <summary>
        /// 校验用户登录
        /// </summary>
        /// <remarks>
        ///  2013-11-23 22:07 Created By iceStone
        /// </remarks>
        /// <param name="uid">用户登录名</param>
        /// <param name="pwd">用户密码</param>
        /// <param name="remember">是否记住登录</param>
        /// <param name="user">当前用户</param>
        /// <param name="isFromCookie">是否来自Cookie登录</param>
        /// <returns>登录结果</returns>
        LoginResult Login(string uid, string pwd, bool remember, out User user, bool isFromCookie);
        /// <summary>
        /// 获取一个用户的加密信息字符串
        /// </summary>
        /// <remarks>
        ///  2013-11-23 22:07 Created By iceStone
        /// </remarks>
        /// <param name="user">用户实体</param>
        /// <returns>加密后的字符串</returns>
        string GetEncryptUserInfo(User user);
        /// <summary>
        /// 根据加密后的cookie解密出用户实体部分信息, 返回用户实体
        /// </summary>
        /// <remarks>
        ///  2013-11-23 22:07 Created By iceStone
        /// </remarks>
        /// <param name="cookie">要解密的cookie</param>
        /// <returns>返回用户实体</returns>
        User GetDecryptUserInfo(string cookie);
        /// <summary>
        /// 校验用户是否已经登录
        /// </summary>
        /// <remarks>
        ///  2013-11-23 22:07 Created By iceStone
        /// </remarks>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool CheckLogin();
        /// <summary>
        /// 校验cookie登录
        /// </summary>
        /// <remarks>
        ///  2013-11-23 22:07 Created By iceStone
        /// </remarks>
        /// <returns>是否登陆成功</returns>
        bool CookieLogin();
        /// <summary>
        /// 退出登录
        /// </summary>
        void Logout();
    }
}
