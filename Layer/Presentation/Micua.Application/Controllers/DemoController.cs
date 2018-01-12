namespace Micua.Application.Controllers
{
    using System.Web.Mvc;
    using Micua.Application.Controllers;
    /// <summary>
    /// DemoController
    /// </summary>
    /// <remarks>
    /// 2014-08-20 02:02:46 Created by iceStone
    /// </remarks>
    public class Demo222Controller : MicuaController
    {
        #region Fields
        const string demoPathFormat = "~/Contents/demo/{0}/index.html";
        #endregion

        #region Properties

        #endregion

        #region Constructors

        #endregion

        #region Action
        //public ActionResult Index(string item)
        //{
        //    return File(Server.MapPath(string.Format(demoPathFormat, item)), "text/html");
        //}
        public ActionResult Index(string lang)
        {
            return Content(lang);
        }
        #endregion

        #region ChildAction

        #endregion

        #region Utilities

        #endregion
    }
}