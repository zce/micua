namespace Micua.Application.Controllers
{
    using System.Web.Mvc;
    using Micua.Application.Controllers;
    /// <summary>
    /// HomeController
    /// </summary>
    /// <remarks>
    /// 2014-08-19 11:40:10 Created by iceStone
    /// </remarks>
    public class HomeController : MicuaController
    {
        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Constructors

        #endregion

        #region Action
        /// <summary>
        /// 首页操作
        /// </summary>
        /// <returns>视图结果</returns>
        public ActionResult Index()
        {
            return Content("<h1>斯通摇滚吧！</h1>");
        }
        #endregion

        #region ChildAction

        #endregion

        #region Utilities

        #endregion
    }
}