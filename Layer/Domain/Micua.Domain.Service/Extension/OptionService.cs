using Micua.Domain.Model;
using Micua.Infrastructure.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Micua.Domain.Service
{
    public static class OptionServiceExtensions
    {
        /// <summary>
        /// 获取区域下的Widget列表
        /// </summary>
        /// <param name="optionService"></param>
        /// <param name="containerName">区域标识</param>
        /// <returns></returns>
        public static IList<Widget> GetWidgets(this IOptionService optionService, string container)
        {
            var widgetStr = optionService.QuerySingle(o => o.Name.Equals("widget_" + container));
            if (widgetStr == null) return new List<Widget>();
            return JsonHelper.Deserialize<IList<Widget>>(widgetStr.Value);
            //return null;
        }
    }
}
