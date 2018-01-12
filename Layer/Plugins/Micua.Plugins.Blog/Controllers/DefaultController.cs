namespace Micua.Plugins.Blog.Controllers
{
    using System.Web.Mvc;
    using Micua.Application.Controllers;
    /// <summary>
    /// DefaultController
    /// </summary>
    /// <remarks>
    /// 2014-08-19 11:40:10 Created by iceStone
    /// </remarks>
    public class DefaultController : MicuaController
    {
        #region Fields



        #endregion

        #region Properties



        #endregion

        #region Constructors



        #endregion

        #region Actions

        /// <summary>
        /// 博客首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        #endregion

        #region ChildActions



        #endregion

        #region Utilities



        #endregion
    }
}