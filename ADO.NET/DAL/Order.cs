namespace DAL
{
    using System;

    public class Order
    {
        public int Id { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime ShippedDate { get; set; }

        public OrderStatus Status { get; set; }

        public OrderDetails OrderDetails { get; set; }
    }
}
