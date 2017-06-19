namespace CachingSolutionsSamples.Order
{
    using System.Collections.Generic;
    using System.Configuration;
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
            string connStr = ConfigurationManager.ConnectionStrings["Northwind"].ConnectionString;
            SqlDependency.Start(connStr);
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand command = new SqlCommand("Select * From Orders", conn))
                {
                    command.Notification = null;

                    SqlDependency dep = new SqlDependency();

                    dep.AddCommandDependency(command);

                    conn.Open();

                    SqlChangeMonitor monitor = new SqlChangeMonitor(dep);

                    policy.ChangeMonitors.Add(monitor);
                }

                cache.Set(prefix + forUser, orders, policy);
            }
        }
    }
}
