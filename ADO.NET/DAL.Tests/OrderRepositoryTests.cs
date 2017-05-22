namespace DAL.Tests
{
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
    }
}
