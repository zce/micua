//using System;
//using System.IO;
//using System.Runtime.Serialization.Formatters.Binary;
//using System.Security.Cryptography;
//using System.Text;
//using System.Web.Caching;

//namespace Micua.Core.Caching
//{
//    public class MicuaOutputCacheProvider : OutputCacheProvider, IDisposable
//    {
//        private const string KeyPrefix = "_outputcache_item_";

//        #region OutputCacheProvider 成员
//        public override object Add(string key, object entry, DateTime utcExpiry)
//        {
//            if (utcExpiry == DateTime.MaxValue)
//                utcExpiry = DateTime.UtcNow.AddHours(1);
//            var existingItem = Get(key);
//            if (existingItem != null)
//                return existingItem;
//            Set(key, entry, utcExpiry);
//            return entry;
//        }

//        public override object Get(string key)
//        {
//            key = KeyPrefix + Md5(key);
//            var exist = Memcached.Get<byte[]>(key);
//            return exist == null ? null : Deserialize(exist);
//        }

//        public override void Set(string key, object entry, DateTime utcExpiry)
//        {
//            if (utcExpiry == DateTime.MaxValue)
//                utcExpiry = DateTime.UtcNow.AddHours(1);
//            key = KeyPrefix + Md5(key);

//            TimeSpan timeSpan = utcExpiry - DateTime.UtcNow;
//            Memcached.Set(key, Serialize(entry), timeSpan);
//            //var exist = Memcached.Get<byte[]>(key);

//            //if (exist != null)
//            //{
//            //    Memcached.Delete(key);
//            //    exist = Serialize(entry);
//            //    Memcached.Set(key, exist, timeSpan);
//            //}
//            //else
//            //{
//            //    Memcached.Set(key, Serialize(entry), timeSpan);
//            //}
//        }

//        public override void Remove(string key)
//        {
//            key = KeyPrefix + Md5(key);
//            Memcached.Delete(key);
//        }
//        #endregion

//        #region 二进制序列化
//        private static byte[] Serialize(object entry)
//        {
//            var formatter = new BinaryFormatter();
//            var stream = new MemoryStream();
//            formatter.Serialize(stream, entry);

//            return stream.ToArray();
//        }

//        private static object Deserialize(byte[] serializedEntry)
//        {
//            var formatter = new BinaryFormatter();
//            var stream = new MemoryStream(serializedEntry);

//            return formatter.Deserialize(stream);
//        }
//        #endregion

//        private static string Md5(string value)
//        {
//            var cryptoServiceProvider = new MD5CryptoServiceProvider();
//            var bytes = Encoding.UTF8.GetBytes(value);
//            var builder = new StringBuilder();
//            bytes = cryptoServiceProvider.ComputeHash(bytes);
//            foreach (var b in bytes)
//                builder.Append(b.ToString("x2").ToLower());
//            return builder.ToString();
//        }

//        public void Dispose()
//        {

//        }
//    }
//}