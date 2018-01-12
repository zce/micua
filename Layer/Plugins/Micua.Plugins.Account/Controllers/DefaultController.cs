using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Micua.Application.Controllers;
using Micua.Domain.Model;
using Micua.Domain.Service;
using Micua.Plugins.Account.Account.Models;

namespace Micua.Plugins.Account.Controllers
{
    public class DefaultController : MicuaController
    {
        [Dependency]
        public IUserService UserService { get; set; }

        #region Index
        public ActionResult Index()
        {
            return View();
        }
        #endregion

        #region Login
        /// <summary>
        /// 登录首页
        /// </summary>
        /// <param name="redirect">跳转页</param>
        /// <returns></returns>
        public ActionResult Login(string redirect = "/")
        {
            if (UserService.CheckLogin())
            {
                return Redirect(redirect);
            }
            ViewBag.Redirect = redirect;
            return View();
        }

        /// <summary>
        /// 异步登录校验方法
        /// </summary>
        /// <param name="login">登录名</param>
        /// <param name="password">密码</param>
        /// <param name="remember">是否记住我</param>
        /// <param name="redirect">跳转地址</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(LoginModel loginer)//string login, string password, bool remember = false, string redirect = "/")
        {
            //System.Threading.Thread.Sleep(2000);
            User currentUser;
            var res = UserService.Login(loginer.Login, loginer.Password, loginer.Remember, out currentUser);
            var status = false;
            var message = "未知错误";
            switch (res)
            {
                case LoginResult.NonExistent:
                case LoginResult.PasswordError:
                    message = "登录名或密码错误";
                    break;
                case LoginResult.NeedValidateCode:
                    message = "需要验证码";
                    break;
                case LoginResult.Awaiting:
                    message = "等待审核";
                    break;
                case LoginResult.Success:
                case LoginResult.OAuthSuccess:
                    status = true;
                    message = "登录成功";
                    break;
                case LoginResult.OAuthFail:
                    message = "登录失败";
                    break;
            }
            if (this.Request.IsAjaxRequest())
            {
                // 异步请求
                return Json(new { status, message, redirect = loginer.Redirect ?? "/" });
            }
            if (status)
            {
                // 登录成功
                return Redirect(loginer.Redirect);
            }
            // 返回登录页
            ViewBag.Message = message;
            return View();
        }
        #endregion

        #region Logout
        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            UserService.Logout();
            ViewBag.Message = "您已退出";
            //ViewBag.Title = "登出";
            return View("Login");
        }
        #endregion

        #region Register

        public ActionResult Register()
        {
            return View();
        }

        #endregion
    }
}