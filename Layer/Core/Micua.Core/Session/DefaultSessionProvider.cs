// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultSessionProvider.cs" company="Wedn.Net">
//   Copyright © 2014 Wedn.Net. All Rights Reserved.
// </copyright>
// <summary>
//   默认的Session供给者
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Micua.Core.Session
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Web;

    using Micua.Infrastructure.Utility;

    /// <summary>
    /// 默认的Session供给者
    /// </summary>
    public class DefaultSessionProvider : ISessionProvider
    {
        /// <summary>
        /// Key前缀,防止冲突
        /// </summary>
        private readonly static string SessionKeyPrefix = Config.GetString("session_key_prefix");
        /// <summary>
        /// Session模块是否启用
        /// </summary>
        private readonly static bool SessionModuleEnabled = true;

        #region 构造函数 +DefaultSessionProvider()
        /// <summary>
        /// 构造函数
        /// </summary>
        static DefaultSessionProvider()
        {
            var context = Context;
            if (context == null) return;
            SessionModuleEnabled = context.ApplicationInstance.Modules.AllKeys.Contains("Session");
            if (SessionModuleEnabled)
            {
                context.Session.Timeout = Setting.GetInt32("session_expires", 20);
            }
        }
        #endregion

        #region 当前的HttpContext +HttpContext Context
        /// <summary>
        /// 当前的HttpContext
        /// </summary>
        /// <remarks>
        ///  2013-11-23 22:04 Created By iceStone
        /// </remarks>
        private static HttpContext Context
        {
            get
            {
                var context = HttpContext.Current;
                if (context == null)
                    throw new NullReferenceException("Unable to get the current HttpContext!");
                return context;
            }
        }
        #endregion

        #region 获取会话唯一标识符 +string SessionId
        /// <summary>
        /// 获取会话唯一标识符
        /// </summary>
        public string SessionId
        {
            get
            {
                if (!SessionModuleEnabled)
                    throw new NotSupportedException("The session module is not enabled!");
                return Context.Session.SessionID;
            }
        }
        #endregion

        #region 获取指定键的Session对象 +object Get(string key)
        /// <summary>
        /// 获取指定键的Session对象
        /// </summary>
        /// <remarks>
        ///  2013-11-23 22:04 Created By iceStone
        /// </remarks>
        /// <param name="key">Session键</param>
        /// <returns>Session对象</returns>
        public object Get(string key)
        {
            if (!SessionModuleEnabled)
                throw new NotSupportedException("The session module is not enabled!");
            key = SessionKeyPrefix + key;
            return Context.Session[key];
        }
        #endregion

        #region 获取指定类型的指定键的Session对象 +TObj Get<TObj>(string key)
        /// <summary>
        /// 获取指定类型的指定键的Session对象
        /// </summary>
        /// <remarks>
        ///  2013-11-23 22:04 Created By iceStone
        /// </remarks>
        /// <typeparam name="TObj">Session数据类型</typeparam>
        /// <param name="key">Session键</param>
        /// <returns>Session对象</returns>
        public TObj Get<TObj>(string key) where TObj : class
        {
            var session = Get(key);
            return session as TObj;
        }
        #endregion

        #region 设置一个Session +void Set(string key, object value)
        /// <summary>
        /// 设置一个Session
        /// </summary>
        /// <remarks>
        ///  2013-11-23 22:04 Created By iceStone
        /// </remarks>
        /// <param name="key">Session键</param>
        /// <param name="value">Session值</param>
        public void Set(string key, object value)
        {
            if (!SessionModuleEnabled)
                throw new NotSupportedException("The session module is not enabled!");
            key = SessionKeyPrefix + key;
            var session = Context.Session;
            session.Remove(key);
            session.Add(key, value);
        }
        #endregion

        #region 删除一个指定的Session +void Remove(string key)
        /// <summary>
        /// 删除一个指定的Session
        /// </summary>
        /// <remarks>
        ///  2013-11-23 22:04 Created By iceStone
        /// </remarks>
        /// <param name="key">Session键</param>
        public void Remove(string key)
        {
            if (!SessionModuleEnabled)
                throw new NotSupportedException("The session module is not enabled!");
            key = SessionKeyPrefix + key;
            Context.Session.Remove(key);
        }
        #endregion

        #region 清空Session +void Clear()
        /// <summary>
        /// 清空Session
        /// </summary>
        /// <remarks>
        ///  2013-11-23 22:04 Created By iceStone
        /// </remarks>
        public void Clear()
        {
            if (!SessionModuleEnabled)
                throw new NotSupportedException("The session module is not enabled!");
            var session = Context.Session;

            session.Clear();
        }
        #endregion

        #region 删除匹配键的Session +void Clear(string pattern)
        /// <summary>
        /// 删除匹配键的Session
        /// </summary>
        /// <remarks>
        ///  2013-11-23 22:04 Created By iceStone
        /// </remarks>
        /// <param name="pattern">匹配键</param>
        public void Clear(string pattern)
        {
            if (!SessionModuleEnabled)
                throw new NotSupportedException("The session module is not enabled!");
            var session = Context.Session;
            if (string.IsNullOrEmpty(pattern))
            {
                session.Clear();
                return;
            }
            var regex = new Regex(SessionKeyPrefix + pattern);
            for (int i = 0; i < session.Keys.Count; i++)
            {
                if (!regex.IsMatch(session.Keys[i])) continue;
                session.Remove(session.Keys[i]);
            }
            //foreach只读
            //foreach (var item in session.Keys.Cast<string>().Where(item => regex.IsMatch(item)))
            //{
            //    session.Remove(item);
            //}
        }
        #endregion

        #region 取消当前会话 +void Abandon()
        /// <summary>
        /// 取消当前会话
        /// </summary>
        /// <remarks>
        ///  2013-11-23 22:04 Created By iceStone
        /// </remarks>
        public void Abandon()
        {
            if (!SessionModuleEnabled)
                throw new NotSupportedException("The session module is not enabled!");
            Context.Session.Abandon();
            Clear();
        }
        #endregion
    }

    //public class MyClass:SessionStateStoreProviderBase 
    //{
    //    public override void Dispose()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override bool SetItemExpireCallback(SessionStateItemExpireCallback expireCallback)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override void InitializeRequest(HttpContext context)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override SessionStateStoreData GetItem(HttpContext context, string id, out bool locked, out TimeSpan lockAge, out object lockId,
    //        out SessionStateActions actions)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override SessionStateStoreData GetItemExclusive(HttpContext context, string id, out bool locked, out TimeSpan lockAge,
    //        out object lockId, out SessionStateActions actions)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override void ReleaseItemExclusive(HttpContext context, string id, object lockId)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override void SetAndReleaseItemExclusive(HttpContext context, string id, SessionStateStoreData item, object lockId, bool newItem)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override void RemoveItem(HttpContext context, string id, object lockId, SessionStateStoreData item)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override void ResetItemTimeout(HttpContext context, string id)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override SessionStateStoreData CreateNewStoreData(HttpContext context, int timeout)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override void CreateUninitializedItem(HttpContext context, string id, int timeout)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override void EndRequest(HttpContext context)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}