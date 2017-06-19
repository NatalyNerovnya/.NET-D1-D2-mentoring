using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthwindLibrary;
using System.Linq;
using System.Threading;

namespace CachingSolutionsSamples
{
    using CachingSolutionsSamples.Employees;
    using CachingSolutionsSamples.Order;

    [TestClass]
	public class CacheTests
	{
		[TestMethod]
		public void MemoryCache()
		{
			var categoryManager = new CategoriesManager(new CategoriesMemoryCache());

			for (var i = 0; i < 10; i++)
			{
				Console.WriteLine(categoryManager.GetCategories().Count());
				Thread.Sleep(100);
			}

            var employeeManager = new EmployeesManager(new EmployeesMemoryCache());

            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine(employeeManager.GetEmployee().Count());
                Thread.Sleep(100);
            }

            var ordersManager = new OrderManager(new OrdersMemoryCache());

            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine(ordersManager.GetOrders().Count());
                Thread.Sleep(100);
            }
        }

		[TestMethod]
		public void RedisCache()
		{
			var categoryManager = new CategoriesManager(new CategoriesRedisCache("localhost"));

			for (var i = 0; i < 10; i++)
			{
				Console.WriteLine(categoryManager.GetCategories().Count());
				Thread.Sleep(100);
			}

            var employeeManager = new EmployeesManager(new EmployeesRedisCache("localhost"));

            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine(employeeManager.GetEmployee().Count());
                Thread.Sleep(100);
            }

            var ordersManager = new OrderManager(new OrdersRedisCache("localhost"));

            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine(ordersManager.GetOrders().Count());
                Thread.Sleep(100);
            }
        }
	}
}
