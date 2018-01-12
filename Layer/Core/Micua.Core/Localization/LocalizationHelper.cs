// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LocalizationHelper.cs" company="Wedn.Net">
//     Copyright © 2014 Wedn.Net. All Rights Reserved.
// </copyright>
// <summary>
//   Defines the LocalizationHelper type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Micua.Core.Localization
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading;

    public static class LocalizationHelper
    {
        #region 区域信息字典
        public static IDictionary<string, string> AllCultures = new Dictionary<string, string>
                                                                 {
                                                                    { string.Empty, "固定区域性" },
                                                                    { "af", "南非荷兰语" },
                                                                    { "af-ZA", "南非荷兰语 - 南非" },
                                                                    { "sq", "阿尔巴尼亚语" },
                                                                    { "sq-AL", "阿尔巴尼亚语 - 阿尔巴尼亚" },
                                                                    { "ar", "阿拉伯语" },
                                                                    { "ar-DZ", "阿拉伯语 - 阿尔及利亚" },
                                                                    { "ar-BH", "阿拉伯语 - 巴林" },
                                                                    { "ar-EG", "阿拉伯语 - 埃及" },
                                                                    { "ar-IQ", "阿拉伯语 - 伊拉克" },
                                                                    { "ar-JO", "阿拉伯语 - 约旦" },
                                                                    { "ar-KW", "阿拉伯语 - 科威特" },
                                                                    { "ar-LB", "阿拉伯语 - 黎巴嫩" },
                                                                    { "ar-LY", "阿拉伯语 - 利比亚" },
                                                                    { "ar-MA", "阿拉伯语 - 摩洛哥" },
                                                                    { "ar-OM", "阿拉伯语 - 阿曼" },
                                                                    { "ar-QA", "阿拉伯语 - 卡塔尔" },
                                                                    { "ar-SA", "阿拉伯语 - 沙特阿拉伯" },
                                                                    { "ar-SY", "阿拉伯语 - 叙利亚" },
                                                                    { "ar-TN", "阿拉伯语 - 突尼斯" },
                                                                    { "ar-AE", "阿拉伯语 - 阿拉伯联合酋长国" },
                                                                    { "ar-YE", "阿拉伯语 - 也门" },
                                                                    { "hy", "亚美尼亚语" },
                                                                    { "hy-AM", "亚美尼亚语 - 亚美尼亚" },
                                                                    { "az", "阿泽里语" },
                                                                    { "az-AZ-Cyrl", "阿泽里语（西里尔语）- 阿塞拜疆" },
                                                                    { "az-AZ-Latn", "阿泽里语（拉丁）- 阿塞拜疆" },
                                                                    { "eu", "巴斯克语" },
                                                                    { "eu-ES", "巴斯克语 - 巴斯克地区" },
                                                                    { "be", "白俄罗斯语" },
                                                                    { "be-BY", "白俄罗斯语 - 白俄罗斯" },
                                                                    { "bg", "保加利亚语" },
                                                                    { "bg-BG", "保加利亚语 - 保加利亚" },
                                                                    { "ca", "加泰罗尼亚语" },
                                                                    { "ca-ES", "加泰罗尼亚语 - 加泰罗尼亚地区" },
                                                                    { "zh-HK", "中文 - 香港特别行政区" },
                                                                    { "zh-MO", "中文 - 澳门特别行政区" },
                                                                    { "zh-CN", "中文 - 中国" },
                                                                    { "zh-CHS", "中文（简体）" },
                                                                    { "zh-SG", "中文 - 新加坡" },
                                                                    { "zh-TW", "中文 - 台湾" },
                                                                    { "zh-CHT", "中文（繁体）" },
                                                                    { "hr", "克罗地亚语" },
                                                                    { "hr-HR", "克罗地亚语 - 克罗地亚" },
                                                                    { "cs", "捷克语" },
                                                                    { "cs-CZ", "捷克语 - 捷克共和国" },
                                                                    { "da", "丹麦语" },
                                                                    { "da-DK", "丹麦语 - 丹麦" },
                                                                    { "div", "马尔代夫语" },
                                                                    { "div-MV", "马尔代夫语 - 马尔代夫" },
                                                                    { "nl", "荷兰语" },
                                                                    { "nl-BE", "荷兰语 - 比利时" },
                                                                    { "nl-NL", "荷兰语 - 荷兰" },
                                                                    { "en", "英语" },
                                                                    { "en-AU", "英语 - 澳大利亚" },
                                                                    { "en-BZ", "英语 - 伯利兹" },
                                                                    { "en-CA", "英语 - 加拿大" },
                                                                    { "en-CB", "英语 - 加勒比" },
                                                                    { "en-IE", "英语 - 爱尔兰" },
                                                                    { "en-JM", "英语 - 牙买加" },
                                                                    { "en-NZ", "英语 - 新西兰" },
                                                                    { "en-PH", "英语 - 菲律宾" },
                                                                    { "en-ZA", "英语 - 南非" },
                                                                    { "en-TT", "英语 - 特立尼达和多巴哥" },
                                                                    { "en-GB", "英语 - 英国" },
                                                                    { "en-US", "英语 - 美国" },
                                                                    { "en-ZW", "英语 - 津巴布韦" },
                                                                    { "et", "爱沙尼亚语" },
                                                                    { "et-EE", "爱沙尼亚语 - 爱沙尼亚" },
                                                                    { "fo", "法罗语" },
                                                                    { "fo-FO", "法罗语 - 法罗群岛" },
                                                                    { "fa", "波斯语" },
                                                                    { "fa-IR", "波斯语 - 伊朗" },
                                                                    { "fi", "芬兰语" },
                                                                    { "fi-FI", "芬兰语 - 芬兰" },
                                                                    { "fr", "法语" },
                                                                    { "fr-BE", "法语 - 比利时" },
                                                                    { "fr-CA", "法语 - 加拿大" },
                                                                    { "fr-FR", "法语 - 法国" },
                                                                    { "fr-LU", "法语 - 卢森堡" },
                                                                    { "fr-MC", "法语 - 摩纳哥" },
                                                                    { "fr-CH", "法语 - 瑞士" },
                                                                    { "gl", "加利西亚语" },
                                                                    { "gl-ES", "加利西亚语 - 加利西亚地区" },
                                                                    { "ka", "格鲁吉亚语" },
                                                                    { "ka-GE", "格鲁吉亚语 - 格鲁吉亚" },
                                                                    { "de", "德语" },
                                                                    { "de-AT", "德语 - 奥地利" },
                                                                    { "de-DE", "德语 - 德国" },
                                                                    { "de-LI", "德语 - 列支敦士登" },
                                                                    { "de-LU", "德语 - 卢森堡" },
                                                                    { "de-CH", "德语 - 瑞士" },
                                                                    { "el", "希腊语" },
                                                                    { "el-GR", "希腊语 - 希腊" },
                                                                    { "gu", "古吉拉特语" },
                                                                    { "gu-IN", "古吉拉特语 - 印度" },
                                                                    { "he", "希伯来语" },
                                                                    { "he-IL", "希伯来语 - 以色列" },
                                                                    { "hi", "印地语" },
                                                                    { "hi-IN", "印地语 - 印度" },
                                                                    { "hu", "匈牙利语" },
                                                                    { "hu-HU", "匈牙利语 - 匈牙利" },
                                                                    { "is", "冰岛语" },
                                                                    { "is-IS", "冰岛语 - 冰岛" },
                                                                    { "id", "印度尼西亚语" },
                                                                    { "id-ID", "印度尼西亚语 - 印度尼西亚" },
                                                                    { "it", "意大利语" },
                                                                    { "it-IT", "意大利语 - 意大利" },
                                                                    { "it-CH", "意大利语 - 瑞士" },
                                                                    { "ja", "日语" },
                                                                    { "ja-JP", "日语 - 日本" },
                                                                    { "kn", "卡纳达语" },
                                                                    { "kn-IN", "卡纳达语 - 印度" },
                                                                    { "kk", "哈萨克语" },
                                                                    { "kk-KZ", "哈萨克语 - 哈萨克斯坦" },
                                                                    { "kok", "贡根语" },
                                                                    { "kok-IN", "贡根语 - 印度" },
                                                                    { "ko", "朝鲜语" },
                                                                    { "ko-KR", "朝鲜语 - 韩国" },
                                                                    { "ky", "吉尔吉斯语" },
                                                                    { "ky-KG", "吉尔吉斯语 - 吉尔吉斯坦" },
                                                                    { "lv", "拉脱维亚语" },
                                                                    { "lv-LV", "拉脱维亚语 - 拉脱维亚" },
                                                                    { "lt", "立陶宛语" },
                                                                    { "lt-LT", "立陶宛语 - 立陶宛" },
                                                                    { "mk", "马其顿语" },
                                                                    { "mk-MK", "马其顿语 - 前南斯拉夫联盟马其顿共和国" },
                                                                    { "ms", "马来语" },
                                                                    { "ms-BN", "马来语 - 文莱" },
                                                                    { "ms-MY", "马来语 - 马来西亚" },
                                                                    { "mr", "马拉地语" },
                                                                    { "mr-IN", "马拉地语 - 印度" },
                                                                    { "mn", "蒙古语" },
                                                                    { "mn-MN", "蒙古语 - 蒙古" },
                                                                    { "no", "挪威语" },
                                                                    { "nb-NO", "挪威语（伯克梅尔）- 挪威" },
                                                                    { "nn-NO", "挪威语（尼诺斯克）- 挪威" },
                                                                    { "pl", "波兰语" },
                                                                    { "pl-PL", "波兰语 - 波兰" },
                                                                    { "pt", "葡萄牙语" },
                                                                    { "pt-BR", "葡萄牙语 - 巴西" },
                                                                    { "pt-PT", "葡萄牙语 - 葡萄牙" },
                                                                    { "pa", "旁遮普语" },
                                                                    { "pa-IN", "旁遮普语 - 印度" },
                                                                    { "ro", "罗马尼亚语" },
                                                                    { "ro-RO", "罗马尼亚语 - 罗马尼亚" },
                                                                    { "ru", "俄语" },
                                                                    { "ru-RU", "俄语 - 俄罗斯" },
                                                                    { "sa", "梵语" },
                                                                    { "sa-IN", "梵语 - 印度" },
                                                                    { "sr-SP-Cyrl", "塞尔维亚语（西里尔语）- 塞尔维亚" },
                                                                    { "sr-SP-Latn", "塞尔维亚语（拉丁）- 塞尔维亚" },
                                                                    { "sk", "斯洛伐克语" },
                                                                    { "sk-SK", "斯洛伐克语 - 斯洛伐克" },
                                                                    { "sl", "斯洛文尼亚语" },
                                                                    { "sl-SI", "斯洛文尼亚语 - 斯洛文尼亚" },
                                                                    { "es", "西班牙语" },
                                                                    { "es-AR", "西班牙语 - 阿根廷" },
                                                                    { "es-BO", "西班牙语 - 玻利维亚" },
                                                                    { "es-CL", "西班牙语 - 智利" },
                                                                    { "es-CO", "西班牙语 - 哥伦比亚" },
                                                                    { "es-CR", "西班牙语 - 哥斯达黎加" },
                                                                    { "es-DO", "西班牙语  - 多米尼加共和国" },
                                                                    { "es-EC", "西班牙语 - 厄瓜多尔" },
                                                                    { "es-SV", "西班牙语 - 萨尔瓦多" },
                                                                    { "es-GT", "西班牙语 - 危地马拉" },
                                                                    { "es-HN", "西班牙语 - 洪都拉斯" },
                                                                    { "es-MX", "西班牙语 - 墨西哥" },
                                                                    { "es-NI", "西班牙语 - 尼加拉瓜" },
                                                                    { "es-PA", "西班牙语 - 巴拿马" },
                                                                    { "es-PY", "西班牙语 - 巴拉圭" },
                                                                    { "es-PE", "西班牙 - 秘鲁" },
                                                                    { "es-PR", "西班牙语 - 波多黎各" },
                                                                    { "es-ES", "西班牙语 - 西班牙" },
                                                                    { "es-UY", "西班牙语 - 乌拉圭" },
                                                                    { "es-VE", "西班牙语 - 委内瑞拉" },
                                                                    { "sw", "斯瓦希里语" },
                                                                    { "sw-KE", "斯瓦希里语 - 肯尼亚" },
                                                                    { "sv", "瑞典语" },
                                                                    { "sv-FI", "瑞典语 - 芬兰" },
                                                                    { "sv-SE", "瑞典语 - 瑞典" },
                                                                    { "syr", "叙利亚语" },
                                                                    { "syr-SY", "叙利亚语 - 叙利亚" },
                                                                    { "ta", "泰米尔语" },
                                                                    { "ta-IN", "泰米尔语 - 印度" },
                                                                    { "tt", "鞑靼语" },
                                                                    { "tt-RU", "鞑靼语 - 俄罗斯" },
                                                                    { "te", "泰卢固语" },
                                                                    { "te-IN", "泰卢固语 - 印度" },
                                                                    { "th", "泰语" },
                                                                    { "th-TH", "泰语 - 泰国" },
                                                                    { "tr", "土耳其语" },
                                                                    { "tr-TR", "土耳其语 - 土耳其" },
                                                                    { "uk", "乌克兰语" },
                                                                    { "uk-UA", "乌克兰语 - 乌克兰" },
                                                                    { "ur", "乌尔都语" },
                                                                    { "ur-PK", "乌尔都语 - 巴基斯坦" },
                                                                    { "uz", "乌兹别克语" },
                                                                    { "uz-UZ-Cyrl", "乌兹别克语（西里尔语）- 乌兹别克斯坦" },
                                                                    { "uz-UZ-Latn", "乌兹别克语（拉丁）- 乌兹别克斯坦" },
                                                                    { "vi", "越南语" },
                                                                    { "vi-VN", "越南语 - 越南" },
                                                                 }; 
        #endregion

        /// <summary>
        /// Gets or sets the localization provider.
        /// </summary>
        private static IDictionary<string, ILocalizationProvider> Providers { get; set; }

        /// <summary>
        /// 设置或获取当前区域信息
        /// </summary>
        public static CultureInfo CurrentUICulture
        {
            get
            {
                return Thread.CurrentThread.CurrentUICulture;
            }

            set
            {
                Thread.CurrentThread.CurrentUICulture = value;
            }
        }

        /// <summary>
        /// Initializes static members of the <see cref="LocalizationHelper"/> class.
        /// </summary>
        static LocalizationHelper()
        {
            Providers = new Dictionary<string, ILocalizationProvider>
                            {
                                {
                                    CurrentUICulture.Name,
                                    new XmlLocalizationProvider(CurrentUICulture)
                                }
                            };
        }

        /// <summary>
        /// 转换字符串
        /// </summary>
        /// <param name="input">输入</param>
        /// <returns>输出</returns>
        public static string Parse(this string input)
        {
            if (Providers.ContainsKey(CurrentUICulture.Name))
            {
                return Providers[CurrentUICulture.Name].Parse(input);
            }

            var provider = new XmlLocalizationProvider(CurrentUICulture);
            Providers.Add(CurrentUICulture.Name, provider);
            return provider.Parse(input);
        }

        /// <summary>
        /// 转换格式化字符串
        /// </summary>
        /// <param name="input">输入</param>
        /// <param name="args">一个对象数组，其中包含零个或多个要设置格式的对象</param>
        /// <returns>输出</returns>
        public static string Parse(this string input, params object[] args)
        {
            return Parse(input).Format(args);
        }
    }
}