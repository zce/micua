using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Micua.Core.Plugin;

namespace Micua.Plugins.Demo
{
    public class DemoPlugin : PluginBase
    {
        public override string Name
        {
            get { return "Demo"; }
        }

        public override string DefaultController
        {
            get { return "Default"; }
        }

        public override string DefaultAction
        {
            get { return "Index"; }
        }

        public override void Install(PluginContext context)
        {
            base.Install(context);
        }
    }
}