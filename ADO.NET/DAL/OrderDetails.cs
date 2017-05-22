namespace DAL
{
    public class OrderDetails
    {
        public string CustomerId { get; set; }

        public int EmployeeId { get; set; }

        public int ProductId { get; set; }

        public decimal UnitPrice { get; set; }

        public short Quantity { get; set; }
    }
}
