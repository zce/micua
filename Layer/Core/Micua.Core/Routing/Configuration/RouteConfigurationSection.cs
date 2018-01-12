//------------------------------------------------------------------------------
// ╭─────────────────────────────╮
// │ ╭─╮     ╭─╮              TM │   ╠═╬═╬═╬═╬═╬═╬═╬═╬═╬═╬═╬═╬═╬═╬═╬═╬═╬═╬═╬═╬═╣
// │ │ │     │ │                 │                                           ☺  
// │ │ ╰───╮ │ │ ╭─────╮ ╭─────╮ │     Name:Wedn.Net 路由配置节点解析     ♮ ♪ ♩
// │ │ ╭─╮ │ │ │ │ ╭─╮ │ │ ╭─╮ │ │                                    ♫ ♭      
// │ │ ╰─╯ │ │ │ │ ╰─╯ │ │ ╰─╯ │ │     Author:iceStone               ♬ ♪       
// │ └─────╯ └─╯ ╰─────╯ ╰───╮ │ │     Chinese:汪磊                              
// │                     ┌───╯ │ │                                              
// │                     ╰─────╯ │   ╠═╬═╬═╬═╬═╬═╬═╬═╬═╬═╬═╬═╬═╬═╬═╬═╬═╬═╬═╬═╬═╣
// ╰─────────────────────────────╯                                              
//------------------------------------------------------------------------------

using System.Configuration;

namespace Micua.Core.Routing.Configuration
{
    /// <summary>
    /// 路由配置节点解析
    /// </summary>
    public class RouteConfigurationSection : ConfigurationSection
    {
        /// <summary>
        /// 命名空间
        /// </summary>
        [ConfigurationProperty("xmlns", IsRequired = true)]
        public string Xmlns
        {
            get { return this["xmlns"].ToString(); }
            set { this["xmlns"] = value; }
        }
        /// <summary>
        /// 拓展名
        /// </summary>
        [ConfigurationProperty("extend", IsRequired = true)]
        public string Extend
        {
            get { return this["extend"].ToString(); }
            set { this["extend"] = value; }
        }

        /// <summary>
        /// 忽略项集合
        /// </summary>
        [ConfigurationProperty("ignores", IsRequired = false)]
        public IgnoreCollection Ignores
        {
            get { return this["ignores"] as IgnoreCollection; }
            set { this["ignores"] = value; }
        }

        /// <summary>
        /// 匹配项集合
        /// </summary>
        [ConfigurationProperty("maps", IsRequired = false)]
        public MapCollection Maps
        {
            get { return this["maps"] as MapCollection; }
            set { this["maps"] = value; }
        }

        /// <summary>
        /// 区域项集合
        /// </summary>
        [ConfigurationProperty("areas", IsRequired = false)]
        public AreaCollection Areas
        {
            get { return this["areas"] as AreaCollection; }
            set { this["areas"] = value; }
        }
    }
}
