namespace ORM.Entities
{
    public class OrderDetail
    {
        public decimal UnitPrice { get; set; }

        public short Quantity { get; set; }

        public double Discount { get; set; }


        public Order Order { get; set; }

        public Product Product { get; set; }
    }
}
