// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ObjectExtension.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the ObjectExtension type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace System
{
    using System.Collections.Generic;
    using System.ComponentModel;

    public static class ObjectExtension
    {
        #region 匿名类转换成字典 +static IDictionary<string, object> ToDictionary(this object obj, char attrSeparator)
        /// <summary>
        /// 匿名类转换成字典
        /// </summary>
        /// <remarks>
        ///  2013-11-18 18:53 Created By iceStone
        /// </remarks>
        /// <param name="input">匿名类</param>
        /// <param name="attrSeparator">属性分隔符，类属性不能有其他字符，_代表字符</param>
        /// <example>
        /// new {id="example",data_something="something"} → {{"id","example"},{"data_something","something"}}
        /// </example>
        /// <returns>属性字典</returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static IDictionary<string, object> ToDictionary(this object input, char attrSeparator = '_')
        {
            var dictionary = new Dictionary<string, object>();
            if (input == null)
            {
                return dictionary;
            }

            foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(input))
            {
                dictionary.Add(
                    attrSeparator == '_' 
                        ? descriptor.Name 
                        : descriptor.Name.Replace('_', attrSeparator),
                    descriptor.GetValue(input));
            }

            return dictionary;
        }
        #endregion

        /// <summary>
        /// 判断一个对象是否为NULL
        /// </summary>
        /// <param name="input">判断对象</param>
        /// <returns>是否为NULL</returns>
        public static bool IsNull(this object input)
        {
            return input == null;
        }

        /// <summary>
        /// 判断一个对象是否不为NULL
        /// </summary>
        /// <param name="input">判断对象</param>
        /// <returns>是否不为NULL</returns>
        public static bool IsNotNull(this object input)
        {
            return input != null;
        }
    }
}
