namespace CachingSolutionsSamples.Order
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.SqlClient;
    using System.Runtime.Caching;

    using NorthwindLibrary;

    public class OrdersMemoryCache : IOrderCache
    {
        private ObjectCache cache = MemoryCache.Default;
        private string prefix = "Cache_Orders";

        public IEnumerable<Order> Get(string forUser)
        {
            return (IEnumerable<Order>)this.cache.Get(this.prefix + forUser);
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

                this.cache.Set(this.prefix + forUser, orders, policy);
            }
        }
    }
}
