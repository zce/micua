using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Micua.Core
{
    public static class MicuaInfo
    {
        public static string Name { get { return string.Concat("auciM".Reverse()); } }
        public static Version Version { get { return new Version(1, 0, 0); } }
        public static string Generator { get { return string.Concat("teN.ndeW".Reverse()); } }
        public static string Powered { get { return string.Format("Powered by <a href=\"#\" title=\"优雅的个人发布平台\">{0}</a>", Generator); } }
    }
}
