// ***********************************************************************
// Project			: Micua.Infrastructure
// Assembly         : Micua.Utility
// Author           : iceStone
// Created          : 2013-11-18 14:30
//
// Last Modified By : iceStone
// Last Modified On : 2013-11-18 14:48
// ***********************************************************************
// <copyright file="LogHelper.cs" company="Wedn.Net">
//     Copyright © 2014 Wedn.Net. All Rights Reserved.
// </copyright>
// <summary>日志操作助手类</summary>
// ***********************************************************************

namespace Micua.Core.Tracing
{
    using System;
    using System.IO;

    using log4net;
    using log4net.Config;

    using Micua.Infrastructure.Utility;

    /// <summary>
    /// 日志操作助手类
    /// </summary>
    /// <remarks>
    ///  2013-11-18 18:56 Created By iceStone
    /// </remarks>
    public static class LogHelper
    {
        /// <summary>
        /// Gets the log.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <value>
        /// The log.
        /// </value>
        private static ILog Logger(string name)
        {
            // get
            // {
            // if (_logger != null) return _logger;
            //////如果使用log4net，应用程序一开始的时候，就要进行初始化配置。
            ////log4net.Config.XmlConfigurator.Configure();
            // string logConfig = MachineHelper.MapPath(Config.GetString("LogConfigPath"));
            // XmlConfigurator.Configure(new FileInfo(logConfig));
            ////如果后面传来的字符串是一样的话，那么返回的log对象就是相同等
            // _logger = 
            return LogManager.GetLogger(name);
 
            // }
        }

        static LogHelper()
        {
            ////如果使用log4net，应用程序一开始的时候，就要进行初始化配置。
            // log4net.Config.XmlConfigurator.Configure();
            string logConfig = Config.GetConfigPath(Config.GetString("trace_config_file", "trace.config"));
            XmlConfigurator.Configure(new FileInfo(logConfig));
        }

        /// <summary>
        /// 错误等级日志
        /// </summary>
        /// <remarks>
        ///  2013-11-18 18:56 Created By iceStone
        /// </remarks>
        /// <param name="loggerName">Logger对象名称</param>
        /// <param name="msg">日志内容</param>
        /// <param name="ex">异常信息</param>
        public static void WriteErrorLog(string loggerName = "Default", string msg = "错误", Exception ex = null)
        {
            var logger = Logger(loggerName);
            if (logger.IsErrorEnabled)
                logger.Error(msg, ex);
        }

        /// <summary>
        /// 调错等级日志
        /// </summary>
        /// <remarks>
        ///  2013-11-18 18:56 Created By iceStone
        /// </remarks>
        /// <param name="loggerName">Logger对象名称</param>
        /// <param name="msg">日志内容</param>
        /// <param name="ex">异常信息</param>
        public static void WriteDebugLog(string loggerName = "Default", string msg = "调错", Exception ex = null)
        {
            var logger = Logger(loggerName);
            if (logger.IsErrorEnabled)
                logger.Debug(msg, ex);
        }

        /// <summary>
        /// 致命等级日志
        /// </summary>
        /// <remarks>
        ///  2013-11-18 18:56 Created By iceStone
        /// </remarks>
        /// <param name="loggerName">Logger对象名称</param>
        /// <param name="msg">日志内容</param>
        /// <param name="ex">异常信息</param>
        public static void WriteFatalLog(string loggerName = "Default", string msg = "致命", Exception ex = null)
        {
            var logger = Logger(loggerName);
            if (logger.IsErrorEnabled)
                logger.Fatal(msg, ex);
        }

        /// <summary>
        /// 消息等级日志
        /// </summary>
        /// <remarks>
        ///  2013-11-18 18:56 Created By iceStone
        /// </remarks>
        /// <param name="loggerName">Logger对象名称</param>
        /// <param name="msg">日志内容</param>
        /// <param name="ex">异常信息</param>
        public static void WriteInfoLog(string loggerName = "Default", string msg = "消息", Exception ex = null)
        {
            var logger = Logger(loggerName);
            if (logger.IsErrorEnabled)
                logger.Info(msg, ex);
        }

        /// <summary>
        /// 警告等级日志
        /// </summary>
        /// <remarks>
        ///  2013-11-18 18:56 Created By iceStone
        /// </remarks>
        /// <param name="loggerName">Logger对象名称</param>
        /// <param name="msg">日志内容</param>
        /// <param name="ex">异常信息</param>
        public static void WriteWarnLog(string loggerName = "Default", string msg = "警告", Exception ex = null)
        {
            var logger = Logger(loggerName);
            if (logger.IsErrorEnabled)
                logger.Warn(msg, ex);
        }
    }
}
