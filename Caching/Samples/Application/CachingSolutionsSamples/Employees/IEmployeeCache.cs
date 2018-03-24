namespace CachingSolutionsSamples.Employees
{
    using System.Collections.Generic;

    using NorthwindLibrary;

    public interface IEmployeeCache
    {
        IEnumerable<Employee> Get(string forUser);
        void Set(string forUser, IEnumerable<Employee> employees);
    }
}
