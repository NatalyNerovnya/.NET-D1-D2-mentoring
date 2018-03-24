namespace CachingSolutionsSamples.Employees
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;

    using NorthwindLibrary;

    public class EmployeesManager
    {
        private IEmployeeCache cache;

        public EmployeesManager(IEmployeeCache cache)
        {
            this.cache = cache;
        }

        public IEnumerable<Employee> GetEmployee()
        {
            Console.WriteLine("Get Employees");

            var user = Thread.CurrentPrincipal.Identity.Name;
            var employees = cache.Get(user);

            if (employees == null)
            {
                Console.WriteLine("From DB");

                using (var dbContext = new Northwind())
                {
                    dbContext.Configuration.LazyLoadingEnabled = false;
                    dbContext.Configuration.ProxyCreationEnabled = false;
                    employees = dbContext.Employees.ToList();
                    cache.Set(user, employees);
                }
            }

            return employees;
        }
    }
}
