namespace Task
{
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Runtime.Serialization;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Task.DB;
    using Task.TestHelpers;

    [TestClass]
    public class SerializationSolutions
    {
        Northwind dbContext;

        [TestInitialize]
        public void Initialize()
        {
            dbContext = new Northwind();
        }

        [TestMethod]
        public void SerializationCallbacks()
        {
            dbContext.Configuration.ProxyCreationEnabled = false;

            var tester = new XmlDataContractSerializerTester<IEnumerable<Category>>(new NetDataContractSerializer(), true);
            var categories = dbContext.Categories.ToList();
            
            tester.SerializeAndDeserialize(categories);
        }

        [TestMethod]
        public void ISerializable()
        {
            dbContext.Configuration.ProxyCreationEnabled = false;

            var tester = new XmlDataContractSerializerTester<IEnumerable<Product>>(new NetDataContractSerializer(), true);
            var products = dbContext.Products.ToList();

            var objContext = (dbContext as IObjectContextAdapter).ObjectContext;
            foreach (var product in products)
            {
                objContext.LoadProperty(product, f => f.Category);
                objContext.LoadProperty(product, f => f.Supplier);
            }

            tester.SerializeAndDeserialize(products);
        }


        [TestMethod]
        public void ISerializationSurrogate()
        {
            dbContext.Configuration.ProxyCreationEnabled = false;

            var tester = new XmlDataContractSerializerTester<IEnumerable<Order_Detail>>(new NetDataContractSerializer(), true);
            var orderDetails = dbContext.Order_Details.ToList();

            var objContext = (dbContext as IObjectContextAdapter).ObjectContext;
            foreach (var detail in orderDetails)
            {
                objContext.LoadProperty(detail, f => f.Order);
                objContext.LoadProperty(detail, f => f.Product);
            }

            tester.SerializeAndDeserialize(orderDetails);
        }

        [TestMethod]
        public void IDataContractSurrogate()
        {
            dbContext.Configuration.ProxyCreationEnabled = false;
            dbContext.Configuration.LazyLoadingEnabled = false;

            var tester = new XmlDataContractSerializerTester<IEnumerable<Order>>(new DataContractSerializer(typeof(IEnumerable<Order>)), true);

            var orders = dbContext.Orders.AsNoTracking().ToList();

            tester.SerializeAndDeserialize(orders);
        }
    }
}
