using System.Web.Mvc;

namespace Micua.Plugins.Demo
{
    public class DemoAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Demo";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Demo_default",
                "Demo/{action}",
                new { controller = "Default", action = "Index" }
            );
        }
    }
}