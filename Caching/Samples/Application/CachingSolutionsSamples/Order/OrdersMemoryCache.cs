namespace CachingSolutionsSamples.Order
{
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Runtime.Caching;

    using NorthwindLibrary;

    public class OrdersMemoryCache : IOrderCache
    {
        ObjectCache cache = MemoryCache.Default;
        string prefix = "Cache_Orders";

        public IEnumerable<Order> Get(string forUser)
        {
            return (IEnumerable<Order>)cache.Get(prefix + forUser);
        }

        public void Set(string forUser, IEnumerable<Order> orders)
        {
            var policy = new CacheItemPolicy();
            policy.ChangeMonitors.Add(new SqlChangeMonitor(new SqlDependency(new SqlCommand("select * from Orders"))));
            cache.Set(prefix + forUser, orders, ObjectCache.InfiniteAbsoluteExpiration);
        }
    }
}
