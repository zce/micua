// ***********************************************************************
// Project			: Micua.Infrastructure
// Assembly         : Micua.Infrastructure.Utility
// Author           : iceStone
// Created          : 2014年01月10日 10:19
//
// Last Modified By : iceStone
// Last Modified On : 2014年01月10日 10:24
// ***********************************************************************
// <copyright file="Guard.cs" company="Wedn.Net">
//     Copyright © 2014 Wedn.Net. All Rights Reserved.
// </copyright>
// <summary>异常保护</summary>
// ***********************************************************************
using System;
using System.Globalization;

namespace Micua.Infrastructure.Utility
{
    /// <summary>
    /// 异常保护
    /// </summary>
    /// <remarks>
    ///  2014年01月10日 10:25 Created By iceStone
    /// </remarks>
    public static class GuardHelper
    {
        /// <summary>
        /// 参数不为Null,如果为Null则抛出异常
        /// </summary>
        /// <remarks>
        ///  2014年01月10日 10:25 Created By iceStone
        /// </remarks>
        /// <param name="argumentValue">参数值.</param>
        /// <param name="argumentName">参数名称.</param>
        /// <exception cref="System.ArgumentNullException">参数为Null</exception>
        public static void ArgumentNotNull(object argumentValue, string argumentName)
        {
            if (argumentValue == null)
            {
                throw new ArgumentNullException(argumentName);
            }
        }

        /// <summary>
        /// Arguments the not null or empty.
        /// </summary>
        /// <remarks>
        ///  2014年01月10日 10:25 Created By iceStone
        /// </remarks>
        /// <param name="argumentValue">参数值.</param>
        /// <param name="argumentName">参数名称.</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        public static void ArgumentNotNullOrEmpty(string argumentValue, string argumentName)
        {
            if (argumentValue == null)
            {
                throw new ArgumentNullException(argumentName);
            }
            if (argumentValue.Length == 0)
            {
                throw new ArgumentException("参数必须不能为空字符串", argumentName);
            }
        }

        /// <summary>
        /// Gets the name of the type.
        /// </summary>
        /// <remarks>
        ///  2014年01月10日 10:25 Created By iceStone
        /// </remarks>
        /// <param name="assignmentInstance">The assignment instance.</param>
        /// <returns>System.String.</returns>
        private static string GetTypeName(object assignmentInstance)
        {
            try
            {
                return assignmentInstance.GetType().FullName;
            }
            catch (Exception)
            {
                return "未知的类型";
            }
        }

        /// <summary>
        /// Instances the is assignable.
        /// </summary>
        /// <remarks>
        ///  2014年01月10日 10:25 Created By iceStone
        /// </remarks>
        /// <param name="assignmentTargetType">Type of the assignment target.</param>
        /// <param name="assignmentInstance">The assignment instance.</param>
        /// <param name="argumentName">Name of the argument.</param>
        /// <exception cref="System.ArgumentNullException">
        /// assignmentTargetType
        /// or
        /// assignmentInstance
        /// </exception>
        /// <exception cref="System.ArgumentException"></exception>
        public static void InstanceIsAssignable(Type assignmentTargetType, object assignmentInstance, string argumentName)
        {
            if (assignmentTargetType == null)
            {
                throw new ArgumentNullException("assignmentTargetType");
            }
            if (assignmentInstance == null)
            {
                throw new ArgumentNullException("assignmentInstance");
            }
            if (!assignmentTargetType.IsInstanceOfType(assignmentInstance))
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture,"类型不匹配", new object[] { assignmentTargetType, GetTypeName(assignmentInstance) }), argumentName);
            }
        }

        /// <summary>
        /// Types the is assignable.
        /// </summary>
        /// <remarks>
        ///  2014年01月10日 10:25 Created By iceStone
        /// </remarks>
        /// <param name="assignmentTargetType">Type of the assignment target.</param>
        /// <param name="assignmentValueType">Type of the assignment value.</param>
        /// <param name="argumentName">Name of the argument.</param>
        /// <exception cref="System.ArgumentNullException">
        /// assignmentTargetType
        /// or
        /// assignmentValueType
        /// </exception>
        /// <exception cref="System.ArgumentException"></exception>
        public static void TypeIsAssignable(Type assignmentTargetType, Type assignmentValueType, string argumentName)
        {
            if (assignmentTargetType == null)
            {
                throw new ArgumentNullException("assignmentTargetType");
            }
            if (assignmentValueType == null)
            {
                throw new ArgumentNullException("assignmentValueType");
            }
            if (!assignmentTargetType.IsAssignableFrom(assignmentValueType))
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "类型不匹配", new object[] { assignmentTargetType, assignmentValueType }), argumentName);
            }
        }
    }
}