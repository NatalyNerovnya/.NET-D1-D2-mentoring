namespace CachingSolutionsSamples.Order
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;

    using NorthwindLibrary;

    public class OrderManager
    {
        private IOrderCache cache;

        public OrderManager(IOrderCache cache)
        {
            this.cache = cache;
        }

        public IEnumerable<Order> GetOrders()
        {
            Console.WriteLine("Get Orders");

            var user = Thread.CurrentPrincipal.Identity.Name;
            var orders = cache.Get(user);

            if (orders == null)
            {
                Console.WriteLine("From DB");

                using (var dbContext = new Northwind())
                {
                    dbContext.Configuration.LazyLoadingEnabled = false;
                    dbContext.Configuration.ProxyCreationEnabled = false;
                    orders = dbContext.Orders.ToList();
                    cache.Set(user, orders);
                }
            }

            return orders;
        }
    }
}
