// ***********************************************************************
// Project			: Micua
// Assembly         : Micua.Infrastructure.Utility
// Author           : iceStone
// Created          : 2013-11-18 14:30
//
// Last Modified By : iceStone
// Last Modified On : 2013-11-18 14:34
// ***********************************************************************
// <copyright file="JsonHelper.cs" company="Wedn.Net">
//     Copyright © 2014 Wedn.Net. All Rights Reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Micua.Infrastructure.Utility
{
    using System.IO;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// JSON操作助手类
    /// </summary>
    /// <remarks>
    ///  2013-11-18 18:56 Created By iceStone
    /// </remarks>
    public static class JsonHelper
    {
        /// <summary>
        /// The json serializer
        /// </summary>
        private static readonly JsonSerializer JsonSerializer = new JsonSerializer();
        /// <summary>
        /// 将一个对象序列化JSON字符串
        /// </summary>
        /// <remarks>
        ///  2013-11-18 18:56 Created By iceStone
        /// </remarks>
        /// <param name="obj">待序列化的对象</param>
        /// <returns>JSON字符串</returns>
        public static string Serialize(object obj)
        {
            var sw = new StringWriter();
            JsonSerializer.Serialize(new JsonTextWriter(sw), obj);
            return sw.GetStringBuilder().ToString();
        }
        /// <summary>
        /// 将JSON字符串反序列化为一个Object对象
        /// </summary>
        /// <remarks>
        ///  2013-11-18 18:56 Created By iceStone
        /// </remarks>
        /// <param name="json">JSON字符串</param>
        /// <returns>Object对象</returns>
        public static object Deserialize(string json)
        {
            var sr = new StringReader(json);
            return JsonSerializer.Deserialize(new JsonTextReader(sr));
        }
        /// <summary>
        /// 将JSON字符串反序列化为一个指定类型对象
        /// </summary>
        /// <remarks>
        ///  2013-11-18 18:56 Created By iceStone
        /// </remarks>
        /// <typeparam name="TObj">对象类型</typeparam>
        /// <param name="json">JSON字符串</param>
        /// <returns>指定类型对象</returns>
        public static TObj Deserialize<TObj>(string json) where TObj : class
        {
            var sr = new StringReader(json);
            return JsonSerializer.Deserialize(new JsonTextReader(sr), typeof(TObj)) as TObj;
        }
        /// <summary>
        /// 将JSON字符串反序列化为一个JObject对象
        /// </summary>
        /// <remarks>
        ///  2013-11-18 18:56 Created By iceStone
        /// </remarks>
        /// <param name="json">JSON字符串</param>
        /// <returns>JObject对象</returns>
        public static JObject DeserializeObject(string json)
        {
            return JsonConvert.DeserializeObject(json) as JObject;
        }
        /// <summary>
        /// 将JSON字符串反序列化为一个JArray数组
        /// </summary>
        /// <remarks>
        ///  2013-11-18 18:56 Created By iceStone
        /// </remarks>
        /// <param name="json">JSON字符串</param>
        /// <returns>JArray对象</returns>
        public static JArray DeserializeArray(string json)
        {
            return JsonConvert.DeserializeObject(json) as JArray;
        }
    }
}
