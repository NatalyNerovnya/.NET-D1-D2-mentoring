namespace DAL
{
    public class OrderDetails
    {
        public string CustomerId { get; set; }

        public int EmployeeId { get; set; }

        public int ProductId { get; set; }

        public double UnitPrice { get; set; }

        public int Quantity { get; set; }
    }
}
