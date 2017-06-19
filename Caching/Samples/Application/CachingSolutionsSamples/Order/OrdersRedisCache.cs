namespace CachingSolutionsSamples.Order
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.Serialization;

    using StackExchange.Redis;

    using Order = NorthwindLibrary.Order;

    public class OrdersRedisCache : IOrderCache
    {
        private ConnectionMultiplexer redisConnection;
        private string prefix = "Cache_Orders";
        private DataContractSerializer serializer = new DataContractSerializer(
            typeof(IEnumerable<Order>));

        public OrdersRedisCache(string hostName)
        {
            redisConnection = ConnectionMultiplexer.Connect(hostName);
        }

        public IEnumerable<Order> Get(string forUser)
        {
            var db = redisConnection.GetDatabase();
            byte[] s = db.StringGet(prefix + forUser);
            if (s == null)
                return null;

            return (IEnumerable<Order>)serializer
                .ReadObject(new MemoryStream(s));
        }

        public void Set(string forUser, IEnumerable<Order> orders)
        {
            var db = redisConnection.GetDatabase();
            var key = prefix + forUser;

            if (orders == null)
            {
                db.StringSet(key, RedisValue.Null, TimeSpan.FromHours(12));
            }
            else
            {
                var stream = new MemoryStream();
                serializer.WriteObject(stream, orders);
                db.StringSet(key, stream.ToArray());
            }
        }
    }
}
