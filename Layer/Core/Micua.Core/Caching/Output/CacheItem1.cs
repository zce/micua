using System;

namespace Micua.Core.Caching
{
    [Serializable]
    internal class CacheItem1
    {
        public string Id { get; set; }
        public byte[] Item { get; set; }
        public DateTime Expiration { get; set; }
    }
}
