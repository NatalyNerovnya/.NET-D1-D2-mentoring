namespace CachingSolutionsSamples.Employees
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Caching;

    using NorthwindLibrary;

    public class EmployeesMemoryCache : IEmployeeCache
    {
        ObjectCache cache = MemoryCache.Default;
        string prefix = "Cache_Employees";
        public IEnumerable<Employee> Get(string forUser)
        {
            return (IEnumerable<Employee>)cache.Get(prefix + forUser);
        }

        public void Set(string forUser, IEnumerable<Employee> employees)
        {
            cache.Set(prefix + forUser, employees, new CacheItemPolicy() { AbsoluteExpiration = new DateTimeOffset().UtcDateTime.AddHours(12) });
        }
    }
}
