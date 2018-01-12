﻿// ***********************************************************************
// Project			: Micua
// Assembly         : Micua.BLL
// Author           : iceStone
// Created          : 2013-11-23 21:59
//
// Last Modified By : iceStone
// Last Modified On : 2013-11-23 22:06
// ***********************************************************************
// <copyright file="UserService.cs" company="Wedn.Net">
//     Copyright (c) Wedn.Net. All rights reserved.
// </copyright>
// <summary>用户 服务类拓展.</summary>
// ***********************************************************************

using System;
using System.Linq;
using System.Linq.Expressions;
using Micua.Core.Caching;
using Micua.Core.Session;
using Micua.Domain.Model;
using Micua.Infrastructure.Utility;

namespace Micua.Domain.Service
{
    /// <summary>
    /// 用户 服务类拓展.
    /// </summary>
    /// <remarks>
    ///  2013-11-23 22:11 Created By iceStone
    /// </remarks>
    public partial class UserService
    {
        #region User常用业务

        #region OAuth用户登录 +LoginResult OAuthLogin(string uid, out User user)
        /// <summary>
        /// OAuth用户登录
        /// </summary>
        /// <remarks>
        ///  2013-11-23 22:11 Created By iceStone
        /// </remarks>
        /// <param name="uid">用户登录名</param>
        /// <param name="user">当前用户</param>
        /// <returns>登录结果</returns>
        public LoginResult OAuthLogin(string uid, out User user)
        {
            user = QuerySingle(u => u.Login == uid.ToLower());
            return user == null ? LoginResult.OAuthFail : LoginResult.OAuthSuccess;
        }
        #endregion

        #region 校验用户登录 +LoginResult Login(string uid, string pwd, out User user)
        /// <summary>
        /// 校验用户登录
        /// </summary>
        /// <remarks>
        ///  2013-11-23 22:11 Created By iceStone
        /// </remarks>
        /// <param name="uid">用户登录名</param>
        /// <param name="pwd">用户密码</param>
        /// <param name="remember">是否记住登录</param>
        /// <param name="user">当前用户</param>
        /// <returns>登录结果</returns>
        public LoginResult Login(string uid, string pwd, bool remember, out User user)
        {
            return Login(uid, pwd, remember, out user, false);
        }
        #endregion

        #region 校验用户登录 +LoginResult Login(string uid, string pwd, out User user, bool isFromCookie)
        /// <summary>
        /// 校验用户登录
        /// </summary>
        /// <remarks>
        ///  2013-11-23 22:11 Created By iceStone
        /// </remarks>
        /// <param name="uid">用户登录名</param>
        /// <param name="pwd">用户密码</param>
        /// <param name="remember">是否记住登录</param>
        /// <param name="user">当前用户</param>
        /// <param name="isFromCookie">登陆信息时候来自Cookie</param>
        /// <returns>登录结果</returns>
        public LoginResult Login(string uid, string pwd, bool remember, out User user, bool isFromCookie)
        {
            //调用数据访问层查询方法根据用户登录名查询登录用户实体
            user = QuerySingle(u => u.Login == uid.ToLower());
            //判断用户是否存在
            if (user == null)
                //不存在则返回登录结果
                return LoginResult.NonExistent;

            //用户是否是Cookie登录
            if (!isFromCookie)
                //不是Cookie登录则需要对用户密码加密
                pwd = pwd.Encrypt(Config.GetString("EncryptKey"));

            //比对用户密码是否一致
            if (user.Password != pwd)
                //不一致则返回登录结果
                return LoginResult.PasswordError;

            //判断用户状态是否是待审核状态
            if (user.Status == UserStatus.Awaiting)
                //是则返回登录结果
                return LoginResult.Awaiting;

            //一切正常则返回登录成功 并且保存到Session
            SessionHelper.Set(SessionKeys.CurrentUser, user);
            if (remember)
            {
                CookieHelper.Set(CookieKeys.UserInfo, GetEncryptUserInfo(user), DateTime.Now.AddDays(7));
            }
            return LoginResult.Success;
        }
        #endregion

        #region 获取一个用户的加密信息字符串 +string GetEncryptUserInfo(User user)
        /// <summary>
        /// 获取一个用户的加密信息字符串
        /// </summary>
        /// <remarks>
        ///  2013-11-23 22:11 Created By iceStone
        /// </remarks>
        /// <param name="user">用户实体</param>
        /// <returns>加密后的字符串</returns>
        public string GetEncryptUserInfo(User user)
        {
            var temp = string.Format("u={0}&l={1}&p={2}", user.Id, user.Login, user.Password);
            return temp.EncryptStr(Config.GetString("EncryptKey"));//StringHelper.EncryptCookie(temp);
        }
        #endregion

        #region 根据加密后的cookie解密出用户实体部分信息, 返回用户实体 +User GetDecryptUserInfo(string cookie)
        /// <summary>
        /// 根据加密后的cookie解密出用户实体部分信息, 返回用户实体
        /// </summary>
        /// <remarks>
        ///  2013-11-23 22:11 Created By iceStone
        /// </remarks>
        /// <param name="cookie">要解密的cookie</param>
        /// <returns>返回用户实体</returns>
        public User GetDecryptUserInfo(string cookie)
        {
            string temp;
            try
            {
                temp = cookie.DecryptStr(Config.GetString("EncryptKey"));//StringHelper.DecryptCookie(cookie);
            }
            catch
            {
                return null;
            }
            var infos = temp.Split('&');
            return new User
            {
                Id = int.Parse(infos[0].Split('=')[1]),
                Login = infos[1].Split('=')[1],
                Password = infos[2].Split('=')[1]
            };
        }
        #endregion

        #region 校验用户是否已经登录 +bool CheckLogin()
        /// <summary>
        /// 校验用户是否已经登录
        /// </summary>
        /// <remarks>
        ///  2013-11-23 22:11 Created By iceStone
        /// </remarks>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool CheckLogin()
        {
            var currentUser = SessionHelper.Get<User>(SessionKeys.CurrentUser);
            return currentUser != null || CookieLogin();
        }
        #endregion

        #region 校验cookie登录 +bool CookieLogin()
        /// <summary>
        /// 校验cookie登录
        /// </summary>
        /// <remarks>
        ///  2013-11-23 22:11 Created By iceStone
        /// </remarks>
        /// <returns>是否登陆成功</returns>
        public bool CookieLogin()
        {
            //HttpCookie cookie = context.Request.Cookies[Config.SitePrefix + CookieKeys.UserInfo];
            string cookie = CookieHelper.Get(CookieKeys.UserInfo);
            if (!string.IsNullOrEmpty(cookie))
            {
                //UserManager userManager = new UserManager();
                var cookieUser = GetDecryptUserInfo(cookie);
                if (cookieUser != null)
                {
                    User currentUser;
                    var res = Login(cookieUser.Login, cookieUser.Password, false, out currentUser, true);
                    if (res == LoginResult.Success)
                    {
                        //审核通过
                        //if (HttpContext.Current.Session["CurrentUser"] == null)
                        //    HttpContext.Current.Session["CurrentUser"] = currentUser;
                        SessionHelper.Set(SessionKeys.CurrentUser, currentUser);
                        //换Cache模拟Session
                        //CacheHelper.SetCache(
                        return true;
                    }
                }
                else
                {
                    //密码被修改了.清除cookie
                    //context.Response.Cookies[Config.SitePrefix + CookieKeys.UserInfo].Expires = DateTime.Now.AddDays(-1);
                    CookieHelper.Remove(CookieKeys.UserInfo);
                }
            }
            return false;
        }
        /// <summary>
        /// 退出登录
        /// </summary>
        public void Logout()
        {
            SessionHelper.Remove(SessionKeys.CurrentUser);
            CookieHelper.Remove(CookieKeys.UserInfo);
        }
        #endregion

        #endregion

        #region 缓存控制 重写通用方法
        /// <summary>
        /// Selects the specified where.
        /// </summary>
        /// <remarks>
        ///  2013-11-23 22:11 Created By iceStone
        /// </remarks>
        /// <param name="predicate">The where.</param>
        /// <returns>IQueryable{User}.</returns>
        public override IQueryable<User> Query(Expression<Func<User, bool>> predicate)
        {
            var res = CacheHelper.Get<IQueryable<User>>(CacheKeys.UserList);
            if (res != null) return res.Where(predicate);

            res = base.Query();
            CacheHelper.Set(CacheKeys.UserList, res, TimeSpan.FromDays(1));
            return res.Where(predicate);
        }
        #endregion
    }
}