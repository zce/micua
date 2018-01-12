// ***********************************************************************
// Project			: Micua.Infrastructure
// Assembly         : Micua.Infrastructure.Utility
// Author           : iceStone
// Created          : 2014年01月09日 11:45
//
// Last Modified By : iceStone
// Last Modified On : 2014年01月09日 11:46
// ***********************************************************************
// <copyright file="XmlHelper.cs" company="Wedn.Net">
//     Copyright © 2014 Wedn.Net. All Rights Reserved.
// </copyright>
// <summary>XML序列化操作助手类</summary>
// ***********************************************************************

namespace Micua.Infrastructure.Utility
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    /// <summary>
    /// XML序列化操作助手类
    /// </summary>
    /// <remarks>
    ///  2014年01月09日 11:47 Created By iceStone
    /// </remarks>
    public static class XmlHelper
    {
        #region XML序列化 static void Serialize(Stream stream, object obj, Encoding encoding)
        /// <summary>
        /// XML序列化
        /// </summary>
        /// <remarks>
        ///  2014年01月09日 11:47 Created By iceStone
        /// </remarks>
        /// <param name="stream">流</param>
        /// <param name="obj">对象</param>
        /// <param name="encoding">编码</param>
        /// <exception cref="System.ArgumentNullException">
        /// o
        /// or
        /// encoding
        /// </exception>
        private static void Serialize(Stream stream, object obj, Encoding encoding)
        {
            if (obj == null)
                throw new ArgumentNullException("obj");
            if (encoding == null)
                throw new ArgumentNullException("encoding");
            var serializer = new XmlSerializer(obj.GetType());
            var settings = new XmlWriterSettings
            {
                Indent = true,
                NewLineChars = "\r\n",
                Encoding = encoding,
                IndentChars = "    "
            };
            using (XmlWriter writer = XmlWriter.Create(stream, settings))
            {
                serializer.Serialize(writer, obj);
                writer.Close();
            }
        }
        #endregion

        #region 将一个对象序列化为XML字符串(UTF-8编码) +static string Serialize(object obj)
        /// <summary>
        /// 将一个对象序列化为XML字符串(UTF-8编码)
        /// </summary>
        /// <remarks>
        ///  2014年01月09日 11:47 Created By iceStone
        /// </remarks>
        /// <param name="obj">要序列化的对象</param>
        /// <returns>序列化产生的XML字符串(UTF-8编码)</returns>
        public static string Serialize(object obj)
        {
            return Serialize(obj, Encoding.UTF8);
        } 
        #endregion

        #region 将一个对象序列化为XML字符串 +static string Serialize(object obj, Encoding encoding)
        /// <summary>
        /// 将一个对象序列化为XML字符串
        /// </summary>
        /// <remarks>
        ///  2014年01月09日 11:47 Created By iceStone
        /// </remarks>
        /// <param name="obj">要序列化的对象</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>序列化产生的XML字符串</returns>
        public static string Serialize(object obj, Encoding encoding)
        {
            using (var stream = new MemoryStream())
            {
                Serialize(stream, obj, encoding);

                stream.Position = 0;
                using (var reader = new StreamReader(stream, encoding))
                {
                    return reader.ReadToEnd();
                }
            }
        }
        #endregion

        #region 将一个对象按XML序列化的方式写入到一个文件(虚拟路径)UTF-8 +static void SerializeToFile(object obj, string virtualPath)
        /// <summary>
        /// 将一个对象按XML序列化的方式写入到一个文件(虚拟路径)UTF-8
        /// </summary>
        /// <remarks>
        ///  2014年01月09日 11:47 Created By iceStone
        /// </remarks>
        /// <param name="obj">The object.</param>
        /// <param name="virtualPath">虚拟路径</param>
        public static void SerializeToFile(object obj, string virtualPath)
        {
            SerializeToFile(obj, virtualPath, Encoding.UTF8);
        }
        #endregion

        #region 将一个对象按XML序列化的方式写入到一个文件 +static void SerializeToFile(object obj, string virtualPath, Encoding encoding)
        /// <summary>
        /// 将一个对象按XML序列化的方式写入到一个文件
        /// </summary>
        /// <remarks>
        ///  2014年01月09日 11:47 Created By iceStone
        /// </remarks>
        /// <param name="obj">要序列化的对象</param>
        /// <param name="virtualPath">虚拟路径</param>
        /// <param name="encoding">编码方式</param>
        /// <exception cref="System.ArgumentNullException">path</exception>
        public static void SerializeToFile(object obj, string virtualPath, Encoding encoding)
        {
            if (string.IsNullOrEmpty(virtualPath))
            {
                throw new ArgumentNullException("virtualPath");
            }

            var path = MachineHelper.MapPath(virtualPath);
            using (var file = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                Serialize(file, obj, encoding);
            }
        }
        #endregion

        #region 从XML字符串(UTF-8编码)中反序列化对象 +static T Deserialize<T>(string str)
        /// <summary>
        /// 从XML字符串(UTF-8编码)中反序列化对象
        /// </summary>
        /// <remarks>
        ///  2014年01月09日 11:47 Created By iceStone
        /// </remarks>
        /// <typeparam name="T">结果对象类型</typeparam>
        /// <param name="str">包含对象的XML字符串</param>
        /// <returns>反序列化得到的对象</returns>
        public static T Deserialize<T>(string str)
        {
            return Deserialize<T>(str, Encoding.UTF8);
        } 
        #endregion

        #region 从XML字符串中反序列化对象 +static T Deserialize<T>(string str, Encoding encoding)
        /// <summary>
        /// 从XML字符串中反序列化对象
        /// </summary>
        /// <remarks>
        ///  2014年01月09日 11:47 Created By iceStone
        /// </remarks>
        /// <typeparam name="T">结果对象类型</typeparam>
        /// <param name="str">包含对象的XML字符串</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>反序列化得到的对象</returns>
        /// <exception cref="System.ArgumentNullException">
        /// s
        /// or
        /// encoding
        /// </exception>
        public static T Deserialize<T>(string str, Encoding encoding)
        {
            if (string.IsNullOrEmpty(str))
                throw new ArgumentNullException("str");
            if (encoding == null)
                throw new ArgumentNullException("encoding");

            var mySerializer = new XmlSerializer(typeof(T));
            using (var ms = new MemoryStream(encoding.GetBytes(str)))
            {
                using (var sr = new StreamReader(ms, encoding))
                {
                    return (T)mySerializer.Deserialize(sr);
                }
            }
        }
        #endregion

        #region 读入一个虚拟路径文件(UTF-8编码)，并按XML的方式反序列化对象。 +static T DeserializeFromFile<T>(string virtualPath)
        /// <summary>
        /// 读入一个虚拟路径文件(UTF-8编码)，并按XML的方式反序列化对象。
        /// </summary>
        /// <remarks>
        ///  2014年01月09日 11:47 Created By iceStone
        /// </remarks>
        /// <typeparam name="T">结果对象类型</typeparam>
        /// <param name="virtualPath">虚拟路径</param>
        /// <returns>反序列化得到的对象</returns>
        public static T DeserializeFromFile<T>(string virtualPath)
        {
            return DeserializeFromFile<T>(virtualPath, Encoding.UTF8);
        }
        #endregion

        #region 读入一个文件，并按XML的方式反序列化对象。 +static T DeserializeFromFile<T>(string virtualPath, Encoding encoding)
        /// <summary>
        /// 读入一个文件，并按XML的方式反序列化对象。
        /// </summary>
        /// <remarks>
        ///  2014年01月09日 11:47 Created By iceStone
        /// </remarks>
        /// <typeparam name="T">结果对象类型</typeparam>
        /// <param name="virtualPath">虚拟路径</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>反序列化得到的对象</returns>
        /// <exception cref="System.ArgumentNullException">
        /// path
        /// or
        /// encoding
        /// </exception>
        public static T DeserializeFromFile<T>(string virtualPath, Encoding encoding)
        {
            if (string.IsNullOrEmpty(virtualPath))
                throw new ArgumentNullException("virtualPath");
            if (encoding == null)
                throw new ArgumentNullException("encoding");

            var path = MachineHelper.MapPath(virtualPath);
            if (!File.Exists(path))
                return default(T);
            string xml = File.ReadAllText(path, encoding);
            return Deserialize<T>(xml, encoding);
        }
        #endregion
    }
}
