namespace FibbonaciClass
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Caching;

    using FibbonaciClass.Redis;

    using StackExchange.Redis;

    public static class FibbonaciNumbers
    {
        public static ICacher Cacher { private get; set; }


        public static int Generate(int quantity)
        {
            if (Cacher == null)
            {
                Cacher = new InMemmoryCache();
            }

            if (quantity <= 0)
            {
                return 0;
            }

            return quantity <= 2 ? 1 : Cacher.GetValue(quantity - 2) + Cacher.GetValue(quantity - 1);
        }
    }

    public class InRedisCache : ICacher
    {
        private IDatabase cacher;

        public InRedisCache()
        {
            this.cacher = RedisConnectorHelper.Connection.GetDatabase();
        }

        public int GetValue(int n)
        {
            if (!this.cacher.KeyExists(n.ToString()))
            {
                this.cacher.StringSet(n.ToString(), FibbonaciNumbers.Generate(n));
            }

            return (int)this.cacher.StringGet(n.ToString());
        }
    }

    public class InMemmoryCache : ICacher
    {
        private ObjectCache cache;

        public InMemmoryCache()
        {
            this.cache = MemoryCache.Default;
        }

        public int GetValue(int n)
        {
            if (!this.cache.Contains(n.ToString()))
            {
                var expirationDate = new DateTimeOffset(DateTime.Now + new TimeSpan(0, 2, 0));
                this.cache.Add(n.ToString(), FibbonaciNumbers.Generate(n), expirationDate);
            }

            return (int)this.cache.Get(n.ToString());
        }
    }
}