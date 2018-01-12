using System;

namespace Micua.Core.Caching.Output
{
    [Serializable]
    internal class CacheItem
    {
        public string Id { get; set; }
        public byte[] Data { get; set; }

        //public object Data { get; set; }
        //public DateTime Expiration { get; set; }
    }
}
