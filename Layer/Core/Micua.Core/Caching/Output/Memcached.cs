//using System;
//using System.Collections;
//using System.Configuration;
//using Memcached.ClientLibrary;

//namespace Micua.Core.Caching
//{
//    public static class Memcached
//    {
//        /// <summary>
//        /// 实例
//        /// </summary>
//        private static readonly MemcachedClient CacheManager;
//        private static readonly object Olock = new object();

//        /// <summary>
//        /// 获取Memcached实例
//        /// </summary>
//        /// <returns></returns>
//        static Memcached()
//        {
//            string[] server = ConfigurationManager.AppSettings["cache_server_list"].Split(',');
//            SockIOPool pool = SockIOPool.GetInstance("OutputCache");
//            pool.SetServers(server);
//            pool.InitConnections = 1;//初始连接数
//            pool.MinConnections = 1;//最小连接数
//            pool.MaxConnections = 500;// 最大连接数500
//            pool.MaxIdle = (1000 * 60 * 60 * 6);//最大处理时间？？

//            pool.SocketConnectTimeout = 1000;
//            pool.SocketTimeout = 3000;

//            pool.MaintenanceSleep = 30;//主线程的睡眠时间
//            pool.Failover = true;

//            pool.Nagle = false;
//            pool.Initialize();
//            //
//            if (CacheManager != null) return;
//            lock (Olock)
//            {
//                if (CacheManager == null)
//                    CacheManager = new MemcachedClient { PoolName = "OutputCache" };
//            }
//        }

//        /// <summary>
//        /// 设置一个对象到Memcached中
//        /// </summary>
//        /// <param name="key">缓存key</param>
//        /// <param name="value">设置到缓存中的对象</param>
//        /// <returns></returns>
//        public static void Set(string key, object value)
//        {
//            CacheManager.Set(key, value);
//        }
//        ///// <summary>
//        ///// 设置添加缓存
//        ///// </summary>
//        ///// <param name="key"></param>
//        ///// <param name="value"></param>
//        ///// <param name="utcExpiry"></param>
//        //public static void Set(string key, object value, DateTime utcExpiry)
//        //{
//        //    CacheManager.Set(key, value, utcExpiry + (DateTime.Now - DateTime.UtcNow));
//        //}

//        /// <summary>
//        /// 设置添加缓存
//        /// </summary>
//        /// <param name="key">主键</param>
//        /// <param name="value">值</param>
//        /// <param name="expired">有效时间</param>
//        public static void Set(string key, object value, TimeSpan expired)
//        {
//            CacheManager.Set(key, value, DateTime.Now.Add(expired));
//        }
//        /// <summary>
//        /// 删除指定key的缓存
//        /// </summary>
//        /// <param name="key"></param>
//        /// <returns></returns>
//        public static bool Delete(string key)
//        {
//            return CacheManager.Delete(key);
//        }

//        /// <summary>
//        /// 获取指定key的缓存
//        /// </summary>
//        /// <param name="key"></param>
//        /// <returns></returns>
//        public static object Get(string key)
//        {
//            return CacheManager.Get(key);
//        }
//        /// <summary>
//        /// 获取指定key的缓存
//        /// </summary>
//        /// <typeparam name="TEntity">实体类型</typeparam>
//        /// <param name="key"></param>
//        /// <returns></returns>
//        public static TEntity Get<TEntity>(string key) where TEntity : class
//        {
//            return CacheManager.Get(key) as TEntity;
//        }
//        /// <summary>
//        /// 获取缓存状态
//        /// </summary>
//        /// <returns></returns>
//        public static Hashtable Status()
//        {
//            return CacheManager.Stats();
//        }
//        /// <summary>
//        /// 判断该键是否有效
//        /// </summary>
//        /// <param name="ekey"></param>
//        /// <returns></returns>
//        public static bool IsKeyInvalid(string ekey)
//        {
//            object obj = CacheManager.Get(ekey);
//            return obj != null;
//        }
//    }
//}
