
namespace FibbonaciClass.Redis
{
    using System;

    using StackExchange.Redis;

    public class RedisConnectorHelper
    {
        private static Lazy<ConnectionMultiplexer> lazyConnection;

        static RedisConnectorHelper()
        {
            RedisConnectorHelper.lazyConnection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect("localhost"));
        }

        public static ConnectionMultiplexer Connection => lazyConnection.Value;
    }
}
