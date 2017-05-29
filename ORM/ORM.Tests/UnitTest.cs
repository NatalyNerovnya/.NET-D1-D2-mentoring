namespace ORM.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using Dapper;
    using Dapper.FluentMap;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ORM.Entities;
    using ORM.Mappers;

    [TestClass]
    public class UnitTest
    {

        private string connectionString;

        public UnitTest()
        {
            this.connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
        }

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
                        db.Query<Supplier>("SELECT * FROM Suppliers WHERE SupplierID = supplierId", new { supplierId = product.SupplierId }).First();
                    product.Category = db.Query<Category>("SELECT * FROM Categories WHERE CategoryID = categoryId", new { categoryId = product.CategoryId }).First();
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

        [TestMethod]
        public void ListOfEmployeesWithTeritories()
        {
            FluentMapper.Initialize(config =>
            {
                config.AddMap(new EmployeeMap());
            });

            List<Employee> employees;
            using (var db = new SqlConnection(this.connectionString))
            {
                employees = db.Query<Employee>("SELECT * FROM Employees").ToList();
                foreach (var employee in employees)
                {
                    var emplTerritories =
                        db.Query<EmployeeTerritory>(
                            "SELECT * FROM EmployeeTerritories WHERE EmployeeID = employeeId", new { employeeId = employee.Id });
                    employee.Territories = new List<Territory>();
                    foreach (var id in emplTerritories)
                    {
                        var t =
                            db.Query<Territory>("SELECT * FROM Territories WHERE TerritoryID = territoryId", new { territoryId = id.TerritoryID }).FirstOrDefault();
                        employee.Territories.Add(t);
                    }
                }
            }

            var actual = employees.FirstOrDefault(e => e.Id == 3);

            var expected = new Employee()
            {
                Id = 3,
                LastName = "Leverling",
                FirstName = "Janet",
                Territories = new List<Territory>()
                                                     {
                                                         new Territory()
                                                     }
            };

            Assert.IsNotNull(actual);
            Assert.IsNotNull(actual.Territories);
            Assert.AreEqual(4, actual.Territories.Count);
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.FirstName, actual.FirstName);
            Assert.AreEqual(expected.LastName, actual.LastName);
        }


        [TestMethod]
        public void ExecuteProcedure()
        {
            using (var db = new SqlConnection(this.connectionString))
            {
                var Beginning_Date = new DateTime(1997, 1, 1);
                var Ending_Date = new DateTime(1997, 12, 30);
                var result = db.Query<Statistic>(
                    "Employee Sales by Country",
                    new { Beginning_Date, Ending_Date },
                    commandType: CommandType.StoredProcedure);

                var actual = result.OrderByDescending(r => r.SaleAmount).First();
                var expected = new Statistic()
                {
                    LastName = "Peacock",
                    OrderID = 10417,
                    SaleAmount = 11188
                };

                Assert.IsNotNull(result);
                Assert.AreEqual(expected.LastName, actual.LastName);
                Assert.AreEqual(expected.SaleAmount, actual.SaleAmount);
                Assert.AreEqual(expected.OrderID, actual.OrderID);
            }
        }

        [TestMethod]
        public void EmployeeWithShipper()
        {
            using (var db = new SqlConnection(this.connectionString))
            {
                var orders = db.Query<Order>("SELECT * FROM Orders").ToList();
                var employeeShipper = new Dictionary<string, List<string>>();

                foreach (var order in orders)
                {
                    var employee = db.Query<Employee>(
                        "SELECT * FROM Employees WHERE EmployeeID = @id",
                        new { @id = order.EmployeeID }).First();
                    var shipper = db.Query<Shipper>(
                        "SELECT * FROM Shippers WHERE ShipperID = @id",
                        new { @id = order.ShipVia }).First();
                    if (employeeShipper.ContainsKey(employee.LastName))
                    {
                        if (!employeeShipper[employee.LastName].Contains(shipper.CompanyName))
                        {
                            employeeShipper[employee.LastName].Add(shipper.CompanyName);
                        }
                    }
                    else
                    {
                        employeeShipper.Add(employee.LastName, new List<string>() { shipper.CompanyName });
                    }
                }
                var expectedLastName = "Buchanan";
                Assert.IsTrue(employeeShipper.ContainsKey(expectedLastName));
                Assert.AreEqual(3, employeeShipper[expectedLastName].Count);
                Assert.IsTrue(employeeShipper[expectedLastName].Contains("Federal Shipping"));
            }
        }

        [TestMethod]
        public void AddEmployee()
        {
            var employee = new Employee()
            {
                City = "Minsk",
                FirstName = "Natasha",
                LastName = "Nerovnya",
                Territories = new List<Territory>()
                                  {
                                      new Territory()
                                          {
                                              RegionID = 1,
                                              TerritoryDescription = "Nata"
                                          }
                                  }
            };

            using (var connection = new SqlConnection(this.connectionString))
            {
                connection.Execute(
                    "INSERT INTO Employees (City, FirstName, LastName) VALUES (@city, @firstName, @lastName)",
                    new { @firstName = employee.FirstName, @lastName = employee.LastName, @city = employee.City });

                connection.Execute(
                    "INSERT INTO Territories(RegionID, TerritoryDescription, TerritoryID) VALUES (@regionId, @description, 11111)",
                    new
                        {
                            @regionId = employee.Territories.First().RegionID,
                            @description = employee.Territories.First().TerritoryDescription
                        });
                var addedEmployeeId = connection.Query<int>("SELECT MAX(EmployeeID) FROM Employees").First();
                connection.Execute(
                    "INSERT INTO EmployeeTerritories (EmployeeID, TerritoryID) VALUES (@employeeId, @territoryID)",
                    new { @employeeId = addedEmployeeId, @territoryId = 11111 });

                var newEmployee = connection.Query<Employee>(
                    "SELECT * FROM Employees WHERE EmployeeID = @id",
                    new { @id = addedEmployeeId }).First();

                Assert.AreEqual(employee.FirstName, newEmployee.FirstName);
                Assert.AreEqual(employee.LastName, newEmployee.LastName);
                Assert.AreEqual(employee.City, newEmployee.City);

                connection.Execute("DELETE FROM EmployeeTerritories WHERE TerritoryID = 11111");
                connection.Execute("DELETE FROM Territories WHERE TerritoryID = 11111");
                connection.Execute("DELETE FROM Employees WHERE EmployeeID = @id", new { @id = addedEmployeeId });
            }
        }
    }
}
