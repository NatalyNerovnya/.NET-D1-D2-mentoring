namespace DAL.Tests
{
    using System;
    using System.Linq;
    using DAL;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class OrderRepositoryTests
    {
        private readonly string connectionString;

        private readonly string provider;

        private readonly IOrderRepository repository;

        public OrderRepositoryTests()
        {
            this.connectionString =
                System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            this.provider = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ProviderName;
            this.repository = new OrderRepository(this.connectionString, this.provider);

        }

        [TestMethod]
        public void GetOrders_ConnectionString_Orders()
        {
            var orders = this.repository.GetOrders();

            Assert.AreEqual(orders.Count(), 830);
        }

        [TestMethod]
        public void GetOrderWithDetails_OrderWithId10974_OrderWithDetails()
        {
            var id = 10974;
            var order = this.repository.GetOrderWithDetails(id);

            Assert.AreEqual(order.Id, id);
            Assert.IsNotNull(order.OrderDetails);
            Assert.AreEqual(order.Status, OrderStatus.Completed);
        }

        [TestMethod]
        public void AddOrder_EmptyOrder_NewOrderInDB()
        {
            var oldOrders = this.repository.GetOrders();
            string customerId = "LILAS";
            var order = new Order()
                            {
                                OrderDate = DateTime.Now,
                                OrderDetails = new OrderDetails()
                                                   {
                                                       CustomerId = customerId,
                                                       EmployeeId = 1
                                                   },
                                ShippedDate = null
                            };

            this.repository.AddOrder(order);
            var orders = this.repository.GetOrders();

            Assert.AreEqual(orders.Count(), oldOrders.Count() + 1);
        }

        [TestMethod]
        public void DeleteOrder_LastCompletedOrderId_False()
        {
            var oldOrders = this.repository.GetOrders();
            var lastId = oldOrders.Where(o => o.Status == OrderStatus.Completed).OrderByDescending(o => o.Id).FirstOrDefault().Id;

            var result = this.repository.DeleteOrder(lastId);
            var orders = this.repository.GetOrders();

            Assert.IsFalse(result);
            Assert.AreEqual(oldOrders.Count(), orders.Count());
            Assert.IsNotNull(orders.Where(o => o.Id == lastId));
        }

        [TestMethod]
        public void DeleteOrder_LastNewOrderId_True()
        {
            var oldOrders = this.repository.GetOrders();
            var lastId = oldOrders.OrderByDescending(o => o.Id).FirstOrDefault().Id;

            var result = this.repository.DeleteOrder(lastId);
            var orders = this.repository.GetOrders();

            Assert.IsTrue(result);
            Assert.AreEqual(oldOrders.Count() - 1, orders.Count());
            Assert.AreEqual(orders.Count(o => o.Id == lastId), 0);
        }
    }
}
