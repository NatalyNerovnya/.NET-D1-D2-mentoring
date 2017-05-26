using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ORM.Tests
{
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;

    using Dapper;
    using Dapper.FluentMap;

    using ORM.Mappers;

    using ORM.Entities;
    using ORM.Mappers;

    [TestClass]
    public class UnitTest1
    {

        private readonly string connectionString = "Server=EPBYMINW0898;Database=Northwind;Trusted_Connection=True;";

        [TestMethod]
        public void ListOfProductsWithCategoryAndSupplier()
        {
            FluentMapper.Initialize(config =>
            {
                config.AddMap(new SupplierMap());
                config.AddMap(new ProductMap());
                config.AddMap(new CategoryMap());
            });

            List<Product> products;
            using (var db = new SqlConnection(this.connectionString))
            {
                products = db.Query<Product>("SELECT * FROM Products").ToList();
                foreach (var product in products)
                {
                    product.Supplier =
                        db.Query<Supplier>("SELECT * FROM Suppliers WHERE SupplierID = " + product.SupplierId).First();
                    product.Category = db.Query<Category>("SELECT * FROM Categories WHERE CategoryID = " + product.CategoryId).First();
                }
                
            }

            var actual = products.FirstOrDefault(p => p.Id == 1);

            var expected = new Product()
                               {
                                   Id = 1,
                                   Category = new Category()
                                                  {
                                                      Id = 1,
                                                      CategoryName = "Beverages",
                                                      Description = "Soft drinks, coffees, teas, beers, and ales"
                                   },
                                   Supplier = new Supplier()
                                                  {
                                                      Id = 1,
                                                      ContactName = "Charlotte Cooper",
                                                      ContactTitle = "Purchasing Manager",
                                                      City = "London", 
                                                      CompanyName = "Exotic Liquids"
                                   },
                                   Discontinued = false,
                                   ProductName = "Chai",
                                   QuantityPerUnit = "10 boxes x 20 bags"
            };

            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Category.Id, actual.Category.Id);
            Assert.AreEqual(expected.Category.CategoryName, actual.Category.CategoryName);
            Assert.AreEqual(expected.Category.Description, actual.Category.Description);
            Assert.AreEqual(expected.Discontinued, actual.Discontinued);
            Assert.AreEqual(expected.ProductName, actual.ProductName);
            Assert.AreEqual(expected.Supplier.Id, actual.Supplier.Id);
            Assert.AreEqual(expected.Supplier.City, actual.Supplier.City);
            Assert.AreEqual(expected.Supplier.ContactTitle, actual.Supplier.ContactTitle);
            Assert.AreEqual(expected.Supplier.CompanyName, actual.Supplier.CompanyName);
            Assert.AreEqual(expected.QuantityPerUnit, actual.QuantityPerUnit);
        }
    }
}
