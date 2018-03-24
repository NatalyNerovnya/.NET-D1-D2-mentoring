namespace CachingSolutionsSamples.Order
{
    using System.Collections.Generic;

    using NorthwindLibrary;

    public interface IOrderCache
    {
        IEnumerable<Order> Get(string forUser);
        void Set(string forUser, IEnumerable<Order> orders);
    }
}
