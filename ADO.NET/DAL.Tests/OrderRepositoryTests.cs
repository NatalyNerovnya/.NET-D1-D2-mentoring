namespace DAL.Tests
{
    using System;
    using System.Linq;

    using DAL;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class OrderRepositoryTests
    {
        private static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        [TestMethod]
        public void GetOrders_ConnectionString_Orders()
        {
            var repository = new OrderRepository(connectionString, "System.Data.SqlClient");

            var orders = repository.GetOrders();

            Assert.AreEqual(orders.Count(), 830);
        }

        [TestMethod]
        public void GetOrderWithDetails_OrderWithId10974_OrderWithDetails()
        {
            var repository = new OrderRepository(connectionString, "System.Data.SqlClient");

            var id = 10974;
            var order = repository.GetOrderWithDetails(id);

            Assert.AreEqual(order.Id, id);
            Assert.IsNotNull(order.OrderDetails);
            Assert.AreEqual(order.Status, OrderStatus.Completed);
        }

        [TestMethod]
        public void AddOrder_EmptyOrder_NewOrderInDB()
        {
            var repository = new OrderRepository(connectionString, "System.Data.SqlClient");
            var oldOrders = repository.GetOrders();
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

            repository.AddOrder(order);
            var orders = repository.GetOrders();

            Assert.AreEqual(orders.Count(), oldOrders.Count() + 1);
        }

        [TestMethod]
        public void DeleteOrder_LastCompletedOrderId_False()
        {
            var repository = new OrderRepository(connectionString, "System.Data.SqlClient");
            var oldOrders = repository.GetOrders();
            var lastId = oldOrders.Where(o => o.Status == OrderStatus.Completed).OrderByDescending(o => o.Id).FirstOrDefault().Id;

            var result = repository.DeleteOrder(lastId);
            var orders = repository.GetOrders();

            Assert.IsFalse(result);
            Assert.AreEqual(oldOrders.Count(), orders.Count());
            Assert.IsNotNull(orders.Where(o => o.Id == lastId));
        }

        [TestMethod]
        public void DeleteOrder_LastNewOrderId_True()
        {
            var repository = new OrderRepository(connectionString, "System.Data.SqlClient");
            var oldOrders = repository.GetOrders();
            var lastId = oldOrders.OrderByDescending(o => o.Id).FirstOrDefault().Id;

            var result = repository.DeleteOrder(lastId);
            var orders = repository.GetOrders();

            Assert.IsTrue(result);
            Assert.AreEqual(oldOrders.Count() - 1, orders.Count());
            Assert.AreEqual(orders.Count(o => o.Id == lastId), 0);
        }
    }
}
