//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.IO;
//using System.Linq;
//using System.Runtime.Serialization.Formatters.Binary;
//using System.Security.Cryptography;
//using System.Text;
//using System.Web.Caching;
//using Micua.Utility;

//namespace Micua.Core.Caching
//{
//    public class MicuaOutputCacheProvider : OutputCacheProvider, IDisposable
//    {

//        readonly IList<CacheItem> _cacheItems;

//        public MicuaOutputCacheProvider()
//        {
//            if (File.Exists("z:\\cache.json"))
//            {
//                var json = File.ReadAllText("z:\\cache.json");
//                _cacheItems = JsonHelper.Deserialize<List<CacheItem>>(json) ?? new List<CacheItem>();
//            }
//            else
//            {
//                _cacheItems = new List<CacheItem>();
//            }
//        }
//        public override object Get(string key)
//        {
//            Debug.WriteLine(string.Format("Cache.Get({0})", key));

//            key = Md5(key);

//            var cacheItem = _cacheItems.FirstOrDefault(c => c.Id == key);

//            if (cacheItem != null)
//            {
//                if (cacheItem.Expiration.ToUniversalTime() <= DateTime.UtcNow)
//                {
//                    _cacheItems.Remove(cacheItem);
//                }
//                else
//                {
//                    return Deserialize(cacheItem.Item);
//                }
//            }

//            return null;
//        }

//        public override object Add(string key, object entry, DateTime utcExpiry)
//        {
//            Debug.WriteLine("Cache.Add({0}, {1}, {2})", key, entry, utcExpiry);

//            key = Md5(key);

//            if (utcExpiry == DateTime.MaxValue)
//                utcExpiry = DateTime.UtcNow.AddMinutes(5);

//            var item = _cacheItems.FirstOrDefault(c => c.Id == key);

//            if (item != null)
//            {
//                if (item.Expiration.ToUniversalTime() <= DateTime.UtcNow)
//                {
//                    _cacheItems.Remove(item);
//                }
//                else
//                {
//                    SaveChange();
//                    return Deserialize(item.Item);
//                }
//            }

//            _cacheItems.Add(new CacheItem
//            {
//                Id = key,
//                Item = Serialize(entry),
//                Expiration = utcExpiry
//            });
//            SaveChange();

//            return entry;
//        }

//        public override void Set(string key, object entry, DateTime utcExpiry)
//        {
//            Debug.WriteLine("Cache.Set({0}, {1}, {2})", key, entry, utcExpiry);

//            key = Md5(key);

//            var item = _cacheItems.FirstOrDefault(c => c.Id == key);

//            if (item != null)
//            {
//                _cacheItems.Remove(item);
//                item.Item = Serialize(entry);
//                item.Expiration = utcExpiry;
//                _cacheItems.Add(item);
//            }
//            else
//            {
//                _cacheItems.Add(new CacheItem
//                {
//                    Id = key,
//                    Item = Serialize(entry),
//                    Expiration = utcExpiry
//                });
//            }
//            SaveChange();
//        }

//        public override void Remove(string key)
//        {
//            Debug.WriteLine("Cache.Remove({0})", key);

//            key = Md5(key);

//            _cacheItems.Remove(_cacheItems.FirstOrDefault(c => c.Id == key));
//            SaveChange();

//        }

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

//        private void SaveChange()
//        {
//            var json = JsonHelper.Serialize(_cacheItems);
//            File.WriteAllText("z:\\cache.json", json);
//        }

//        public void Dispose()
//        {
//            var json = JsonHelper.Serialize(_cacheItems);
//            File.WriteAllText("z:\\cache.json", json);
//        }
//    }
//}