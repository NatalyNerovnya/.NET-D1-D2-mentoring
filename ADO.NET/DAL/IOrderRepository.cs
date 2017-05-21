namespace DAL
{
    using System.Collections.Generic;

    public interface IOrderRepository
    {
        IEnumerable<Order> GetOrders();

        Order GetOrderWithDetails(int id);
    }
}
