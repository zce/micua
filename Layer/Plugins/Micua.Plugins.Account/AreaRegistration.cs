using System.Web.Mvc;

namespace Micua.Plugins.Account
{
    public class AccountAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Account";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            //context.MapRoute(
            //    "Account_default",
            //    "Account/{action}",
            //    new { controller = "Default", action = "Index" }
            //);
        }
    }
}